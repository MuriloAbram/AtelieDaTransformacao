using AtelieDaTransformacao.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Encapsulamento pra gerenciar usuarios e operações de autenticação
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Injeção de dependencia
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registra um usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            // Valida a senha
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { message = "As senhas não coincidem." });

            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            //Cria o usuário usando o UserManager
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(erros => erros.Description);
                return BadRequest(new { message = "Erro ao registrar usuário.", errors });
            }
            return Ok(new { message = "Usuário registrado com sucesso." });
        }

        /// <summary>
        /// Usuario faz login
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Email ou senha invalidos" });
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            var roles = await _userManager.GetRolesAsync(user!);

            return Ok(new UserDto
            {
                Id = user!.Id,
                Email = dto.Email,
                Roles = roles
            });
        }

        /// <summary>
        /// Usuario faz logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout realizado!" });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized(new { message = "Uuario não encontrado" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }
    }
}
