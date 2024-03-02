using Bitinvest.Domain.Entities;
using Bitinvest.Domain.ValueObjects;
using Bogus;
using Bogus.Extensions.Brazil;
using System.Text.RegularExpressions;

namespace BitInvest.Tests.Config
{

    [CollectionDefinition(nameof(BitInvestCollection))]
    public class BitInvestCollection : ICollectionFixture<ClientesFixture> { }
    public class ClientesFixture
    {
        public IEnumerable<Cliente> GerarClientesValidos(int quantidade)
        {
            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f =>
                    new Cliente(
                        f.Name.FirstName() + " " + f.Name.LastName(),
                        f.Internet.Email(),
                        GerarEnderecoValido(),
                        GerarCpfValido(),
                        GerarTelefoneValido()
                    ));
            return cliente.Generate(quantidade);
        }

        public Cliente GerarClienteValido() => GerarClientesValidos(1).FirstOrDefault();
        public Cliente GerarClienteInvalido() => new Cliente("", "", GerarEnderecoInvalido(), GerarCpfInvalido(), GerarTelefoneInvalido());

        private Cpf GerarCpfValido()
        {
            var faker = new Faker("pt_BR");
            var numeroCpf = faker.Person.Cpf();

            Regex regexObj = new Regex(@"[^\d]");
            numeroCpf = regexObj.Replace(numeroCpf, "");

            return new Cpf(numeroCpf);
        }
        private Cpf GerarCpfInvalido()
        {
            return new Cpf("12345678901");
        }

        public IEnumerable<Endereco> GerarEnderecosValidos(int quantidade)
        {
            var endereco = new Faker<Endereco>("pt_BR")
                .CustomInstantiator(f =>
                    new Endereco(
                        f.Address.ZipCode().Replace("-", ""),
                        f.Address.StreetAddress(),
                        f.Address.BuildingNumber(),
                        f.Address.StreetSuffix(),
                        f.Address.StreetName(),
                        f.Address.City(),
                        f.Address.State()
            ));
            return endereco.Generate(quantidade);
        }
        public Endereco GerarEnderecoValido() => GerarEnderecosValidos(1).FirstOrDefault();
        public Endereco GerarEnderecoInvalido() => new Endereco("", "", "", "", "", "", "");
        public Telefone GerarTelefoneValido() => new Telefone("18996779396");
        public Telefone GerarTelefoneInvalido() => new Telefone("1234567");
    }
}
