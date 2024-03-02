using Bitinvest.App.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Bitinvest.App.Services
{
    public abstract class BaseService
    {
        protected readonly INotificator _notificator;
        protected BaseService(INotificator notificator) => _notificator = notificator;
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {
            _notificator.AddNotification(mensagem);
        }
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> 
        {
            var validator = validacao.Validate(entidade);
            if (validator.IsValid) return true;
            Notificar(validator);
            return false;
        }
        protected bool ExecutarValidacao(ValidationResult validator)
        {
            if (validator.IsValid) return true;
            Notificar(validator);
            return false;
        }
        protected bool ExecutarValidacao(bool condicao, string erro)
        {
            if (condicao)
                Notificar(erro);
            return !condicao;
        }
    }
}
