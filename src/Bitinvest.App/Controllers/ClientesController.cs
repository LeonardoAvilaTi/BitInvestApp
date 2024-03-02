using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitinvest.App.Controllers
{
    [Authorize]
    [Route("api/clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteService _service;
        public ClientesController(INotificator notificador, IClienteService service) : base(notificador)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var clientes = await _service.ObterTodos();
            return CustomResponse(clientes);
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var cliente = await _service.ObterPorId(id);
            return CustomResponse(cliente);
        }

        [HttpGet("cpf:string")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
        {
            var cliente = await _service.ObterPorCpf(cpf);
            return CustomResponse(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteDTO dto)
        {
            await _service.Adicionar(dto);
            return CustomResponse();
        }

        [HttpPut("id:guid")]
        public async Task<IActionResult> AtualizarCliente(Guid id, ClienteAlteracaoDTO dto)
        {
            await _service.Atualizar(id, dto);
            return CustomResponse();
        }

        [HttpPut("endereco/id:guid")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoDTO dto)
        {
            await _service.AlterarEndereco(id, dto);
            return CustomResponse();
        }
        
        [HttpPut("telefone/id:guid")]
        public async Task<IActionResult> AtualizarTelefone(Guid id, TelefoneDTO dto)
        {
            await _service.AlterarTelefone(id, dto);
            return CustomResponse();
        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _service.Remover(id);
            return CustomResponse();
        }
    }
}
