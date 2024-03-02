
using Bitinvest.Domain.Entities.ContaReais;
using Bitinvest.Domain.Entities.ContaReais.Tests;
using BitInvest.Tests.Config;
using FluentAssertions;
using System.Runtime.ConstrainedExecution;

namespace BitInvest.Tests.Domain
{
     [Collection(nameof(BitInvestCollection2))]
    public class ContaRealTests
    {
        private readonly ContaReaisFixture _fixtures;

        public ContaRealTests(ContaReaisFixture fixtures) => _fixtures = fixtures;


        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Crédito Válido")]
        public void Conta_Corrente_Valor_Credito_Valido()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoCreditoValida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Débito Válido")]
        public void Conta_Corrente_Valor_Debito_Valido()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoDebitoValida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento Inválido por Falta de Preenchimento do campo Id")]
        public void Conta_Corrente_Valor_Credito_Invalido_Id_Vazio()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoSemIdInvalida();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "O campo ClienteId precisa ser fornecido");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento Inválido por Falta de Preenchimento do campo TipoOperação")]
        public void Conta_Corrente_Valor_Credito_Invalido_Operacao_Vazio()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoSemOperacaoInvalida();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "O campo TipoOperacao precisa ser fornecido");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento Inválido por Falta de Preenchimento do campo Descrição")]
        public void Conta_Corrente_Valor_Credito_Invalido_Descricao_Vazio()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoSemDescricaoInvalida();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "O campo DescricaoOperacao precisa ser fornecido");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento Inválido por Falta de Preenchimento do campo Data Movimento")]
        public void Conta_Corrente_Valor_Credito_Invalido_DataMov_Vazio()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoSemDataMovInvalida();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "O campo DataMovimento precisa ser fornecido");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Crédito Inválido por valor Negativo")]
        public void Conta_Corrente_Valor_Credito_Invalido_Valor_Negativo()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoCreditoNegativoInvalida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");
        }

  
        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Débito Inválido por valor Negativo")]
        public void Conta_Corrente_Valor_Debito_Invalido_Valor_Negativo()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoCreditoNegativoInvalida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Crédito Inválido por valor Zerado")]
        public void Conta_Corrente_Valor_Credito_Invalido_Valor_Zerado()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoCreditoZeradoInvalida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert0
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");

        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Débito Inválido por valor Zerado")]
        public void Conta_Corrente_Valor_Debito_Invalido_Valor_Zerado()
        {
            //Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoDebitoZeradoInvalida();
            //Act
            var result = contaCorrenteReais.Validar();
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Crédito Inválido por valor Menor que o Mínimo")]
        public void Deve_Falhar_Quando_Valor_Credito_Abaixo_Do_Minimo()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoCreditoAbaixoMinimoInvalido();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");
        }

        [Trait("Categoria", "ContaReais")]
        [Fact(DisplayName = "Lançamento de Débito Inválido por valor Menor que o Mínimo")]
        public void Deve_Falhar_Quando_Valor_Debito_Abaixo_Do_Minimo()
        {
            // Arrange
            var contaCorrenteReais = _fixtures.CriarContaCorrenteReaisMovimentoDebitoAbaixoMinimoInvalido();
            // Act
            var validationResult = contaCorrenteReais.Validar();
            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Depósitos ou saques com valor mínimo de R$ 100,00");
        }


        // Adicione mais casos de teste aqui conforme necessário
    }
}


