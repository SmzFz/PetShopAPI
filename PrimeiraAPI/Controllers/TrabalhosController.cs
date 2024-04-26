using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabalhosController : ControllerBase
    {
        private readonly MyContext _context;

        public TrabalhosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Trabalhos
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trabalhos>>> GetTrabalhados()
        {
            if (_context.Trabalhados == null)
            {
                return NotFound();
            }
            return await _context.Trabalhados.ToListAsync();
        }

        // GET: api/Trabalhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trabalhos>> GetTrabalhos(Guid id)
        {
            if (_context.Trabalhados == null)
            {
                return NotFound();
            }
            var trabalhos = await _context.Trabalhados.FindAsync(id);

            if (trabalhos == null)
            {
                return NotFound();
            }

            return trabalhos;
        }

        // PUT: api/Trabalhos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabalhos(Guid id, Trabalhos trabalhos)
        {
            if (id != trabalhos.TrabalhosId)
            {
                return BadRequest();
            }

            _context.Entry(trabalhos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabalhosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trabalhos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Trabalhos>> PostTrabalhos(Trabalhos trabalhos)
        {
            if (_context.Trabalhados == null)
            {
                return Problem("Entity set 'MyContext.Trabalhados'  is null.");
            }
            _context.Trabalhados.Add(trabalhos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrabalhos", new { id = trabalhos.TrabalhosId }, trabalhos);
        }

        // DELETE: api/Trabalhos/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabalhos(Guid id)
        {
            if (_context.Trabalhados == null)
            {
                return NotFound();
            }
            var trabalhos = await _context.Trabalhados.FindAsync(id);
            if (trabalhos == null)
            {
                return NotFound();
            }

            _context.Trabalhados.Remove(trabalhos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrabalhosExists(Guid id)
        {
            return (_context.Trabalhados?.Any(e => e.TrabalhosId == id)).GetValueOrDefault();
        }
    }
}
