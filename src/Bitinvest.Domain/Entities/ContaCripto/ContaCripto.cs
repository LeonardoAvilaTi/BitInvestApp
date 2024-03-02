using Bitinvest.Domain.Entities.ContaReais;
using FluentValidation;
using FluentValidation.Results;

namespace Bitinvest.Domain.Entities.ContaCripto
{
    public class ContaCripto : Entity
    {
        public ContaCripto() { }

        public ContaCripto(Guid clienteId, DateTime dataMovimento, string criptoMoeda, string descricaoOperacao, 
            TipoOperacao tipoOperacao, decimal quantidade, decimal saldo)
        {
            ClienteId = clienteId;
            DataMovimento = dataMovimento;
            CriptoMoeda = criptoMoeda;
            DescricaoOperacao = descricaoOperacao;
            TipoOperacao = tipoOperacao;
            Quantidade = quantidade;
            Saldo = saldo;
            Aprovado = false;
        }

        public Guid ClienteId { get; }
        public DateTime DataMovimento { get; }
        public string CriptoMoeda { get; }
        public string DescricaoOperacao { get; }
        public TipoOperacao TipoOperacao { get; }
        public decimal Quantidade { get; }
        public decimal Saldo { get; }
        public bool Aprovado { get; }

        public override ValidationResult Validar()
        {
            return new ValidatorContaCripto().Validate(this);
        }
    }

    public class ValidatorContaCripto : AbstractValidator<ContaCripto>
    {

        public ValidatorContaCripto()
        {

            RuleFor(c => c.ClienteId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");


            RuleFor(c => c.DataMovimento)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.CriptoMoeda)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.DescricaoOperacao)
                   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                   .Length(5, 250).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.TipoOperacao)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Quantidade)
               .GreaterThan(0).WithMessage("A quantidade é obrigatória");

        }

    }
}
