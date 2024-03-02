using AutoMapper;
using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.Domain.Entities;
using Bitinvest.Domain.Repositories;
using Bitinvest.Domain.ValueObjects;
using static Bitinvest.Domain.Entities.Cliente;

namespace Bitinvest.App.Services
{
    public interface IClienteService
    {
        public Task Adicionar(ClienteDTO dto);
        public Task Atualizar(Guid id, ClienteAlteracaoDTO dto);
        public Task Remover(Guid id);
        public Task<ClienteDTO> ObterPorId(Guid id);
        public Task<ClienteDTO> ObterPorCpf(string numero);
        public Task<IEnumerable<ClienteDTO>> ObterTodos();
        public Task AlterarEndereco(Guid id, EnderecoDTO endereco);
        public Task AlterarTelefone(Guid id, TelefoneDTO telefone);

    }

    public class ClienteService : BaseService, IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryCliente _repository;

        public ClienteService(INotificator notificator, IMapper mapper, IRepositoryCliente repository) : base(notificator)
        {
            _mapper = mapper;  
            _repository = repository;
        }

        private async Task<Cliente> ValidarClienteExistente(Guid id)
        {
            var clienteExistente = await _repository.ObterPorId(id);
            if (clienteExistente is null)
            {
                Notificar("Cliente não cadastrado na base de Dados!");
            }

            return clienteExistente;
        }
        public async Task Adicionar(ClienteDTO dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);
            if(!ExecutarValidacao(new ValidatorClienteValido(), cliente)) 
                return; 

            await _repository.Adicionar(cliente);
        }

        public async Task AlterarEndereco(Guid id, EnderecoDTO endereco)
        {
            var cliente = await ValidarClienteExistente(id);

            var novoEndereco = _mapper.Map<Endereco>(endereco);
            if (!ExecutarValidacao(new ValidatorEnderecoValido(), novoEndereco))
                return;

            cliente.AlterarEndereco(novoEndereco);
            await _repository.Atualizar(id, cliente);

        }

        public async Task AlterarTelefone(Guid id, TelefoneDTO telefone)
        {
            var cliente = await ValidarClienteExistente(id);

            var novoTelefone = _mapper.Map<Telefone>(telefone);
            if (!ExecutarValidacao(new ValidatorTelefoneValido(), novoTelefone))
                return;

            cliente.AlterarTelefone(novoTelefone);
            await _repository.Atualizar(id, cliente);

        }

        public async Task Atualizar(Guid id, ClienteAlteracaoDTO dto)
        {
            var cliente = await ValidarClienteExistente(id);
            var resultNome = cliente.AtualizarNome(dto.Nome);
            var resultEmail = cliente.AtualizarEmail(dto.Email);

            if (!resultNome.IsValid || !resultEmail.IsValid) 
            {
                Notificar(resultNome.Errors.FirstOrDefault().ErrorMessage);
                Notificar(resultEmail.Errors.FirstOrDefault().ErrorMessage);
                return;
            }
            
            if (!ExecutarValidacao(new ValidatorClienteValido(), cliente))
                return;

            await _repository.Atualizar(id, cliente);
        }

        public async Task<ClienteDTO> ObterPorCpf(string numero)
        {
            var clienteExistente = await _repository.ObterPorCpf(numero);
            if (clienteExistente is null)
            {
                Notificar("Cliente não cadastrado na base de Dados!");
            }

            return _mapper.Map<ClienteDTO>(clienteExistente);
        }

        public async Task<ClienteDTO> ObterPorId(Guid id)
        {
            var clienteExistente = await ValidarClienteExistente(id);
            return _mapper.Map<ClienteDTO>(clienteExistente);
        }

        public async Task<IEnumerable<ClienteDTO>> ObterTodos()
        {
            var clientes = await _repository.ObterTodos();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);  
        }

        public async Task Remover(Guid id)
        {
            await ValidarClienteExistente(id);
            await _repository.Remover(id);
            
        }
    }
}
