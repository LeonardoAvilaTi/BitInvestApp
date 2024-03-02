namespace Bitinvest.App.DTOS
{
    public class UsuarioRespostaLoginDTO
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioTokenDTO UsuarioToken { get; set; }
    }
}
