using Azure.Storage.Queues;
using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.Domain.Entities.ContaCripto;
using Bitinvest.Domain.Entities.ContaReais;
using Bitinvest.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using JsonException = Newtonsoft.Json.JsonException;

namespace Bitinvest.App.Services
{

    public interface IContaCriptoService
    {
        public Task Comprar(MovimentoContaCriptoDTO dto);
        public Task Vender(MovimentoContaCriptoDTO dto);
        public Task<decimal> ConsultarSaldo(Guid clienteId, string criptoMoeda);
    }
    public class ContaCriptoService : BaseService, IContaCriptoService
    {
        private readonly IRepositoryContaCripto _repositoryContaCripto;
        private readonly IRepositoryContaReais _repositoryContaReais;
        private readonly IConfiguration _configuration;

        public ContaCriptoService(INotificator notificator, IRepositoryContaCripto repositoryContaCripto,
            IRepositoryContaReais repositoryContaReais, IConfiguration configuration) : base(notificator)
        {
            _repositoryContaCripto = repositoryContaCripto;
            _repositoryContaReais = repositoryContaReais;
            _configuration = configuration; 
        }

        public async Task<decimal> ConsultarSaldo(Guid clienteId, string criptoMoeda)
        {
            var saldo = await _repositoryContaCripto.ObterSaldo(clienteId, criptoMoeda);
            return saldo;
        }

        public async Task Comprar(MovimentoContaCriptoDTO dto)
        {
            var saldoReais = await _repositoryContaReais.ObterSaldo(dto.ClienteId);
            var saldoCripto = await _repositoryContaCripto.ObterSaldo(dto.ClienteId, dto.CriptoMoeda.ToString());
            //var valorOperacao = await ConsultarValorOperacao(dto.CriptoMoeda, dto.Quantidade);
            var valorOperacao = await ConsultarValorOperacaoCoinGecko(dto.CriptoMoeda, dto.Quantidade);

            if (valorOperacao > saldoReais)
            {
                Notificar("O saldo não é suficiente para realizar a transação");
                return;
            }

            var movimentoContaReais = new ContaCorrenteReais(dto.ClienteId, dto.Data, dto.Descricao, TipoOperacao.Debito, valorOperacao, saldoReais - valorOperacao);
            var movimentoCripto = new ContaCripto(dto.ClienteId, dto.Data, dto.CriptoMoeda.ToString(), dto.Descricao, TipoOperacao.Credito, dto.Quantidade, saldoCripto + dto.Quantidade);


            if (!ExecutarValidacao(new ValidatorContaCripto(), movimentoCripto))
                return;

            if (!ExecutarValidacao(new ValidatorContaReais(), movimentoContaReais))
                return;

            await _repositoryContaCripto.Adicionar(movimentoCripto);
            await _repositoryContaReais.Adicionar(movimentoContaReais);

            var queueClient = new QueueClient(_configuration.GetConnectionString("AzureStorage"), "compras-cripto");
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                await queueClient.SendMessageAsync(Base64Encode(movimentoCripto.Id.ToString()));
            }
        }

        public async Task Vender(MovimentoContaCriptoDTO dto)
        {
            //var valorOperacao = await ConsultarValorOperacao(dto.CriptoMoeda, dto.Quantidade);
            var valorOperacao = await ConsultarValorOperacaoCoinGecko(dto.CriptoMoeda, dto.Quantidade);
            var saldoCripto = await _repositoryContaCripto.ObterSaldo(dto.ClienteId, dto.CriptoMoeda.ToString());
            var saldoReais = await _repositoryContaReais.ObterSaldo(dto.ClienteId);

            if (saldoCripto < dto.Quantidade)
            {
                Notificar("Quantidade insuficiente para venda");
                return;
            }

            var movimentoContaReais = new ContaCorrenteReais(dto.ClienteId, dto.Data, dto.Descricao, TipoOperacao.Credito, valorOperacao, saldoReais + valorOperacao);
            var movimentoCripto = new ContaCripto(dto.ClienteId, dto.Data, dto.CriptoMoeda.ToString(), dto.Descricao, TipoOperacao.Debito, dto.Quantidade, saldoCripto - dto.Quantidade);

            if (!ExecutarValidacao(new ValidatorContaCripto(), movimentoCripto))
                return;

            if (!ExecutarValidacao(new ValidatorContaReais(), movimentoContaReais))
                return;



            await _repositoryContaCripto.Adicionar(movimentoCripto);
            await _repositoryContaReais.Adicionar(movimentoContaReais);

        }

        private async Task<decimal> ConsultarValorOperacao(CriptoMoeda criptoMoeda, decimal quantidade)
        {
            var simbolos = new Dictionary<string, string>()
            {
                { "bitcoin", "BTC-BRL" },
                { "ethereum", "ETH-BRL" },
            };


            var endpoint = $"https://economia.awesomeapi.com.br/last/";
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint);

            var result = await httpClient.GetAsync(simbolos[criptoMoeda.ToString()]);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {


                var response = await result.Content.ReadAsStringAsync();
                var cotacao = JsonConvert.DeserializeObject<CotacaoDTO>(response);

                return quantidade * cotacao.BTCBRL.Ask;
            }

            Notificar("Ocorreu um erro ao pesquisar a cotação da moeda");
            return 0;
        }


        private async Task<decimal> ConsultarValorOperacaoCoinGecko(CriptoMoeda criptoMoeda, decimal quantidade)
        {
            var chaveAPI = "CG-hbxSBuQnHQV7H7uQsrBdzCoR";
            var httpClient = new HttpClient();
            var endpoint = "https://api.coingecko.com/api/v3/simple/price";

            try
            {
                var parametros = new Dictionary<string, string>
                 {
                    { "ids", criptoMoeda.ToString().ToLower() },
                    { "vs_currencies", "brl" },
                    { "api_key", chaveAPI }
                };

                // Construa a URL com os parâmetros
                var urlComParametros = $"{endpoint}?{string.Join("&", parametros.Select(p => $"{p.Key}={p.Value}"))}";

                var resposta = await httpClient.GetStringAsync(urlComParametros);

                var cotacaoResponse = JObject.Parse(resposta);
                var brlValue = cotacaoResponse[criptoMoeda.ToString().ToLower()]["brl"].Value<decimal>();

                return quantidade * brlValue;

            }
            catch (HttpRequestException e)
            {
                Notificar($"Erro na solicitação HTTP para CoinGecko: {e.Message}");
                return 0;
            }
            catch (JsonException e)
            {
                Notificar($"Erro na desserialização JSON para CoinGecko: {e.Message}");
                return 0;
            }
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
