using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Services;

namespace PrimeiraAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost("api/login")]
        public IActionResult Login(string username, string password)
        {

            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var result = _signInManager.CheckPasswordSignInAsync(user, password, false).Result;
            if (result.Succeeded)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                var token = TokenService.GenerateToken(user, roles);
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("api/logout")]
        public IActionResult Logout()
        {
            var result = _signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(StatusCodes.Status503ServiceUnavailable);
        }
        //POST: Atualizar Senha
        [HttpPost("api/updatepassword")]

        public async Task<IActionResult> UpdatePassword(string userId, string oldPassword, string newPassoword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastro!");
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassoword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        //POST: Resetar Senha
        [HttpPost("api/resetpassword")]

        public async Task<IActionResult> ResetPassword(string userId, string oldPassword, string newPassoword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastro!");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassoword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
