using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
        //GET: api/users - listar todos os usuarios
        [HttpGet("api/users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {

                return Ok(users);
            }

            return NoContent();


        }

        //GET: api/users/{id} - listar usuarios por id
        [Authorize(Roles = "admin")]
        [HttpGet("api/users/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                return Ok(user);
            }
            return BadRequest(StatusCodes.Status204NoContent);
        }
        //POST: api/users - Atualizar Usuário
        [Authorize(Roles = "admin")]
        [HttpPut("api/users/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegisterModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }

        //DELETE: api/users/{id} - Deletar usuário
        [Authorize(Roles = "admin")]
        [HttpDelete("api/users/{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }
    }
}
