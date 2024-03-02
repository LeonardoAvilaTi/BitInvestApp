using Bitinvest.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace Bitinvest.Domain.Entities
{
    public class Cliente : Entity
    {
        public Cliente(){}

        public Cliente(string nome, string email, Endereco endereco, Cpf cpf, Telefone telefone)
        {
            Nome = nome;
            Email = email;
            Endereco = endereco;
            Cpf = cpf;
            Telefone = telefone;
        }

        public string Nome { get; private set; }
        public Cpf Cpf { get; }
        public string Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public Telefone Telefone { get; private set; }
        public override ValidationResult Validar() => new ValidatorClienteValido().Validate(this);

        public ValidationResult Desativar()
        {
            var result = new ValidatorClientePodeDesativar().Validate(this);
            if (result.IsValid)
                AlterarStatus();
            return result;
        }

        public void AlterarEndereco(Endereco endereco) => Endereco = endereco;
        public void AlterarTelefone(Telefone telefone) => Telefone = telefone;

        public class ValidatorClienteValido : AbstractValidator<Cliente>
        {
            public ValidatorClienteValido()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(5, 250).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("{PropertyName} em formato inválido")
                    .MaximumLength(250).WithMessage("O campo {PropertyName} pode ter no máximo {MaxLength} caracteres");

                RuleFor(x => x.Cpf).Must(Cpf.ValidarCpf).WithMessage("{PropertyName} inválido!");
                RuleFor(x => x.Endereco).SetValidator(new ValidatorEnderecoValido());
                RuleFor(x => x.Telefone).SetValidator(new ValidatorTelefoneValido());
            }
        }

        public ValidationResult AtualizarNome(string nome)
        {
            if (nome.Length < 2 || nome.Length > 250)
            {
                AdicionarErro("Nome deve ter entre 2 e 250 caracteres");
                return ValidationResult;
            }

            Nome = nome;
            return ValidationResult;
        }

      
        public ValidationResult AtualizarEmail(string email)
        {
            Email = email;
            ValidationResult = new ValidarEmailCliente().Validate(this);
            return ValidationResult;
        }
    }

    public class ValidatorClientePodeDesativar : AbstractValidator<Cliente>
    {
        public ValidatorClientePodeDesativar()
        {
            RuleFor(x => x.Ativo).Equal(true)
                .WithMessage("Cliente já está desativado!");
        }
    }
    public class ValidarEmailCliente : AbstractValidator<Cliente>
    {
        public ValidarEmailCliente()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail em formato inválido");
        }
    }
}
