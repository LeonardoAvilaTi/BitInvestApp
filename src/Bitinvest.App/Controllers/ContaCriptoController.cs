using Bitinvest.App.DTOS;
using Bitinvest.App.Notifications;
using Bitinvest.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bitinvest.App.Controllers
{
    //[Authorize]
    [Route("api/conta-cripto")]
    public class ContaCriptoController : MainController
    {
        private readonly IContaCriptoService _service;
        
        public ContaCriptoController(INotificator notificador, IContaCriptoService service) : base(notificador)
        {
            _service = service;
        }

        [HttpPost("comprar")]
        public async Task<IActionResult> Comprar(MovimentoContaCriptoDTO dto)
        {
            await _service.Comprar(dto);
            return CustomResponse();
        }

        [HttpPost("vender")]
        public async Task<IActionResult> Vender(MovimentoContaCriptoDTO dto)
        {
            await _service.Vender(dto);
            return CustomResponse();
        }

        [HttpGet("obter-saldo/{id:guid}")]
        public async Task<IActionResult> ConsultarSaldo(Guid id, CriptoMoeda cripto)
        {
            return CustomResponse(await _service.ConsultarSaldo(id, cripto.ToString()));
        }

    }
}
