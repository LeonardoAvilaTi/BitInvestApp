using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.Domain.Entities;
using Bitinvest.Domain.Entities.ContaReais;
using Bitinvest.Domain.Repositories;

namespace Bitinvest.App.Services
{
    public interface IContaCorrenteReaisService
    {
        public Task Depositar(MovimentoContaCorrenteDTO dto);
        public Task Sacar(MovimentoContaCorrenteDTO dto);
        public Task<decimal> ConsultarSaldo(Guid clienteId);
    }

    public class ContaCorrenteReaisService : BaseService, IContaCorrenteReaisService
    {
        private readonly IRepositoryCliente _repoCliente;
        private readonly IRepositoryContaReais _repo;

        
        public ContaCorrenteReaisService(INotificator notificator,
            IRepositoryCliente repoCliente, IRepositoryContaReais repo) : base(notificator)
        {
            _repoCliente = repoCliente;
            _repo = repo;
        }

        public async Task<decimal> ConsultarSaldo(Guid clienteId)
        {
            var cliente = await ValidarClienteExistente(clienteId);
            if(cliente == null) { return  0; }

            var saldo = await _repo.ObterSaldo(clienteId);
            return saldo;
        }

        public async Task Depositar(MovimentoContaCorrenteDTO dto)
        {
            var cliente = await ValidarClienteExistente(dto.ClienteId);
            if (cliente == null) { return; }

            var saldo = await _repo.ObterSaldo(dto.ClienteId);
            saldo += dto.Valor;

            var deposito = new ContaCorrenteReais(dto.ClienteId, dto.Data, dto.Descricao, TipoOperacao.Credito, dto.Valor, saldo);

            if (!ExecutarValidacao(new ValidatorContaReais(), deposito))
                return;

            await _repo.Adicionar(deposito);

        }

        public async Task Sacar(MovimentoContaCorrenteDTO dto)
        {
            var cliente = await ValidarClienteExistente(dto.ClienteId);
            if(cliente == null) { return; }

            var saldo = await _repo.ObterSaldo(dto.ClienteId);
            saldo -= dto.Valor;

            var saque = new ContaCorrenteReais(dto.ClienteId, dto.Data, dto.Descricao, TipoOperacao.Debito, dto.Valor, saldo);

            if (!ExecutarValidacao(new ValidatorContaReais(), saque))
                return;

            await _repo.Adicionar(saque);
        }

        

        private async Task<Cliente> ValidarClienteExistente(Guid id)
        {
            var clienteExistente = await _repoCliente.ObterPorId(id);
            if (clienteExistente is null)
            {
                Notificar("Cliente não cadastrado na base de Dados!");
            }

            return clienteExistente;
        }
    }
}
