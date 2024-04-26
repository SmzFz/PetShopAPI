using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagarsController : ControllerBase
    {
        private readonly MyContext _context;

        public PagarsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Pagars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagar>>> GetPagamentos()
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }
            return await _context.Pagamentos.ToListAsync();
        }

        // GET: api/Pagars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagar>> GetPagar(Guid id)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }
            var pagar = await _context.Pagamentos.FindAsync(id);

            if (pagar == null)
            {
                return NotFound();
            }

            return pagar;
        }

        // PUT: api/Pagars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagar(Guid id, Pagar pagar)
        {
            if (id != pagar.PagarId)
            {
                return BadRequest();
            }

            _context.Entry(pagar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagarExists(id))
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

        // POST: api/Pagars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagar>> PostPagar(Pagar pagar)
        {
            if (_context.Pagamentos == null)
            {
                return Problem("Entity set 'MyContext.Pagamentos'  is null.");
            }

            var venda = _context.Vendas.Where(v => v.VenderId == pagar.VenderId).FirstOrDefault();
            if (venda == null)
            {
                return BadRequest();
            }

            if (pagar.ValorPagar > venda.VendaValor)
            {
                pagar.TrocoPagar = pagar.ValorPagar - venda.VendaValor;
            }

            if (pagar.ValorPagar < venda.VendaValor)
            {
                return BadRequest("Pague o valor necessário para continuar");
            }



            _context.Pagamentos.Add(pagar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagar", new { id = pagar.PagarId }, pagar);
        }

        // DELETE: api/Pagars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagar(Guid id)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }
            var pagar = await _context.Pagamentos.FindAsync(id);
            if (pagar == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagarExists(Guid id)
        {
            return (_context.Pagamentos?.Any(e => e.PagarId == id)).GetValueOrDefault();
        }
    }
}
