namespace Bitinvest.App.DTOS
{
    public class MovimentoContaCorrenteDTO
    {
        public Guid ClienteId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
    }
}
