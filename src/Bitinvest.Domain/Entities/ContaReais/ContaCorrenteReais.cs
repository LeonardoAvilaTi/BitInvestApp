using FluentValidation;
using FluentValidation.Results;

namespace Bitinvest.Domain.Entities.ContaReais
{
    public class ContaCorrenteReais : Entity
    {

        public ContaCorrenteReais() {}
        public ContaCorrenteReais(Guid clienteId, DateTime dataMovimento, 
            string descricaoOperacao, TipoOperacao tipoOperacao, decimal valor, decimal saldo)
        {
            ClienteId = clienteId;
            DataMovimento = dataMovimento;
            DescricaoOperacao = descricaoOperacao;
            TipoOperacao = tipoOperacao;
            Valor = valor;
            Saldo = saldo;
        }

        public Guid ClienteId { get; }
        public DateTime DataMovimento { get; }
        public string DescricaoOperacao { get; }
        public TipoOperacao TipoOperacao { get; }
        public decimal Valor { get; }
        public decimal Saldo { get; }

        public override ValidationResult Validar()
        {
            return new ValidatorContaReais().Validate(this);
        }
    }


    public class ValidatorContaReais : AbstractValidator<ContaCorrenteReais>
    {

        public ValidatorContaReais()
        {


            RuleFor(c => c.ClienteId)
                .NotEmpty().WithMessage("O campo ClienteId precisa ser fornecido");

            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O campo Valor precisa ser fornecido");

            RuleFor(c => c.DataMovimento)
               .NotEmpty().WithMessage("O campo DataMovimento precisa ser fornecido");

            RuleFor(c => c.DescricaoOperacao)
                   .NotEmpty().WithMessage("O campo DescricaoOperacao precisa ser fornecido")
                   .Length(5, 250).WithMessage("O campo DescricaoOperacao precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.TipoOperacao)
               .NotEmpty().WithMessage("O campo TipoOperacao precisa ser fornecido");

            RuleFor(c => c.Valor)
               .GreaterThanOrEqualTo(100).WithMessage("Depósitos ou saques com valor mínimo de R$ 100,00");


            // regra para verificar débito maior que o saldo
             RuleFor(c => c)
                 .Must(c => c.TipoOperacao != TipoOperacao.Debito || c.Valor <= c.Saldo)
                 .WithMessage("Operação de débito não pode ser maior que o saldo disponível");


        }

    }
}
