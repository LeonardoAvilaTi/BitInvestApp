using Bitinvest.App.Notifications;
using Bitinvest.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitinvest.App.Controllers
{
    [Route("api/usuarios")]
    [Authorize]
    public class AuthController : MainController
    {
        private readonly AuthService _authService;
        public AuthController(INotificator notificador, AuthService authService) : base(notificador)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Usuarios()
        {
            var users = await _authService.UserManager.Users.ToListAsync();
            return CustomResponse(users);
        }

        [HttpPost("nova-conta")]
        [AllowAnonymous]
        public async Task<ActionResult> Registrar(string email, string senha)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
            };


            var result = await _authService.UserManager.CreateAsync(user, senha);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    NotificarErro(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string email, string senha)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.SignInManager.PasswordSignInAsync(email, senha,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authService.GerarJwt(email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            NotificarErro("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                NotificarErro("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authService.ObterRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                NotificarErro("Refresh Token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authService.GerarJwt(token.Username));
        }

        [HttpPost("recuperar-senha")]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarSenha([FromBody] string email)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = await _authService.UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var recover = await _authService.UserManager.GeneratePasswordResetTokenAsync(user);

                if (recover != null)
                {
                    return CustomResponse(recover);
                }
                NotificarErro("Não foi possível realizar a recuperação da senha.");
                return CustomResponse();
            }
            NotificarErro("E-mail não cadastrado.");
            return CustomResponse();
        }

        [HttpPost("redefinir-senha")]
        [AllowAnonymous]
        public async Task<IActionResult> RedefinirSenha([FromBody] string email, string codigo, string novaSenha)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _authService.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                NotificarErro("Não foi possível redefinir a senha!");
                return CustomResponse();
            }

            var reset = await _authService.UserManager.ResetPasswordAsync(user, codigo, novaSenha);
            if (!reset.Succeeded)
                NotificarErro("Não foi possível redefinir a senha!");

            return CustomResponse();
        }
    }
}
