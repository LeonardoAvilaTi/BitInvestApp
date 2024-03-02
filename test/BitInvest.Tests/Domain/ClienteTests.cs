using BitInvest.Tests.Config;
using FluentAssertions;

namespace BitInvest.Tests.Domain
{
    [Collection(nameof(BitInvestCollection))]
    public class ClienteTests
    {
        private readonly ClientesFixture _fixtures;
        public ClienteTests(ClientesFixture fixtures) => _fixtures = fixtures;

        [Trait("Categoria", "Clientes")]
        [Fact(DisplayName = "Novo cliente pessoa física válido")]
        public void Cliente_Criar_PessoaFisica_Valido()
        {
            //Arrange
            var cliente = _fixtures.GerarClienteValido();
            //Act
            var result = cliente.Validar();
            //Assert
            result.IsValid.Should().BeTrue();
        }

        
        [Trait("Categoria", "Clientes")]
        [Fact(DisplayName = "Novo cliente pessoa física inválido")]
        public void Cliente_Criar_PessoaFisica_Invalido()
        {
            //Arrange
            var cliente = _fixtures.GerarClienteInvalido();
            //Act
            var result = cliente.Validar();
            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(17);
        }


        [Trait("Categoria", "Clientes")]
        [Fact(DisplayName = "Desativar cliente com sucesso")]
        public void Cliente_Desativar_Sucesso()
        {
            //Arrange
            var cliente = _fixtures.GerarClienteValido();

            //Act
            var result = cliente.Desativar();

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Trait("Categoria", "Clientes")]
        [Fact(DisplayName = "Desativar cliente inativo deve falhar")]
        public void Cliente_DesativarInativo_DeveFalhar()
        {
            var cliente = _fixtures.GerarClienteValido();

            //Act
            var result = cliente.Desativar();
            var resultErro = cliente.Desativar();

            //Assert
            resultErro.IsValid.Should().BeFalse();
            cliente.Ativo.Should().BeFalse();
        }
    }
}
