namespace Bitinvest.App.DTOS
{
    public class MovimentoContaCriptoDTO
    {
        public Guid ClienteId { get; set; }
        public CriptoMoeda CriptoMoeda { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Quantidade { get; set; }
    }

    public enum CriptoMoeda
    {
        bitcoin,
        ethereum,
        monero
    }
}
