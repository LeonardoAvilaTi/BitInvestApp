using Bitinvest.Domain.Entities;
using FluentValidation;

namespace Bitinvest.Domain.ValueObjects
{
    public class Telefone 
    {
        public Telefone(string numero)
        {
            Numero = numero;
        }
        public string Numero { get; }
        public virtual Cliente Cliente { get; set; }
    }

    public class ValidatorTelefoneValido : AbstractValidator<Telefone>
    {
        public ValidatorTelefoneValido()
        {
            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MinimumLength(10).WithMessage("O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres");
        }
    }
}
