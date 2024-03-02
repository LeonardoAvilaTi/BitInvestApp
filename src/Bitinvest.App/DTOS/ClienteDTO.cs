namespace Bitinvest.App.DTOS
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CpfDTO Cpf { get; set; }
        public string Email { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public TelefoneDTO Telefone { get; set; }

    }

    public class ClienteAlteracaoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
