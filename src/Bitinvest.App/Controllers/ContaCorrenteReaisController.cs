using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitinvest.App.Controllers
{
    //[Authorize]
    [Route("api/conta-corrente")]
    public class ContaCorrenteReaisController : MainController
    {
        private readonly IContaCorrenteReaisService _service;
        
        public ContaCorrenteReaisController(INotificator notificador, IContaCorrenteReaisService service) : base(notificador)
        {
            _service = service;
        }

        [HttpPost("depositar")]
        public async Task<IActionResult> Depositar(MovimentoContaCorrenteDTO dto)
        {
            await _service.Depositar(dto);
            return CustomResponse();
        }

        [HttpPost("sacar")]
        public async Task<IActionResult> Sacar(MovimentoContaCorrenteDTO dto)
        {
            await _service.Sacar(dto);
            return CustomResponse();
        }

        [HttpGet("obter-saldo/{id:guid}")]
        public async Task<IActionResult> ConsultarSaldo(Guid id)
        {
            return CustomResponse(await _service.ConsultarSaldo(id));
        }

    }
}
