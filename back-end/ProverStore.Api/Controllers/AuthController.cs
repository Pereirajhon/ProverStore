
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Business.Notificacoes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProverStore.Api.Controllers
{
    
    [Route("/api/conta")]
    public class AuthController: MainController
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private readonly IClienteRepository _clienteRepository;

        public AuthController(UserManager<Cliente> userManager, SignInManager<Cliente> signInManager ,IClienteRepository clienteRepository , IOptions<JwtSettings> jwtSettings, INotificador notificador):base(notificador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _clienteRepository = clienteRepository;
        }

        [HttpPost("google")]
        public async Task<ActionResult> GoogleAuth([FromBody] string googleToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken);

                var user = new Cliente
                {
                    Email = payload.Email,
                    UserName = payload.Name,
                    EmailConfirmed = true
                };

                var i = await _userManager.FindByEmailAsync(user.Email);

                if(i == null)
                {
                   return CustomResponse();
                }

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {

                    var token = await GerarJwt(payload.Email);

                    CustomResponse(new { token });
                }

                foreach(var error in result.Errors)
                {
                    NotificarErro(error.Description);
                }

                return CustomResponse(payload);

            }
            catch (InvalidJwtException)
            {
                return BadRequest("Token inválido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

        }

        [HttpPost("cadastro")]
        public async Task<ActionResult> Cadastro(UserVM cadastro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new Cliente
            {
                Email = cadastro.Email,
                UserName = cadastro.Nome,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, cadastro.Senha);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(cadastro.Email));
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(cadastro);
            
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, false);
            
            if (result.Succeeded)
            {
                var u = await _userManager.FindByEmailAsync(login.Email);

                var roles = await _userManager.GetRolesAsync(u);

                if (u == null) return CustomResponse();

                var usuario = new ClienteVM
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = roles.FirstOrDefault(),
                };

                var token = await GerarJwt(login.Email);

                return CustomResponse(new {token, usuario});
            }

            if (result.IsNotAllowed)
            {
                NotificarErro("Acesso não permitido !");
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(login);
            }
            
            NotificarErro("Usuário ou Senha incorretos");

            return CustomResponse(login);

        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return CustomResponse(new { message = "Logout feito com sucesso" });
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }

    }
}
