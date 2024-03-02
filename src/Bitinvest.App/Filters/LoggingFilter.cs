using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Any;

namespace Bitinvest.App.Filters
{
    public class LoggingFilter : IActionFilter
    {

        private DateTime _inicio;

        private readonly ILogger<AnyType> _logger;
        public LoggingFilter(ILogger<AnyType> logger) 
        { 
            _logger = logger;
        }
       

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _inicio = DateTime.Now;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var duracao = DateTime.Now - _inicio;
            var acao = context.ActionDescriptor.DisplayName;
            var log = $"Requisição realizada em {acao}, com duração em {duracao.TotalSeconds} segundos";

            _logger.LogInformation(log);
        }
    }
}
