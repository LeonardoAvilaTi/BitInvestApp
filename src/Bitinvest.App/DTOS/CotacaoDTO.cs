namespace Bitinvest.App.DTOS
{
    public class CotacaoDTO
    {
        public ResponseCotacao BTCBRL { get; set; }
    }

    public class ResponseCotacao
    {
        public decimal Ask { get; set; }
    }
}
