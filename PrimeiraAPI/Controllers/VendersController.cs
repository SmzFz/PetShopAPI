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
    public class VendersController : ControllerBase
    {
        private readonly MyContext _context;

        public VendersController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Venders
        [Authorize(Roles = "admin,vendedor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vender>>> GetVendas()
        {
            if (_context.Vendas == null)
            {
                return NotFound();
            }
            return await _context.Vendas.ToListAsync();
        }

        // GET: api/Venders/5
        [Authorize(Roles = "admin,vendedor")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vender>> GetVender(Guid id)
        {
            if (_context.Vendas == null)
            {
                return NotFound();
            }
            var vender = await _context.Vendas.FindAsync(id);

            if (vender == null)
            {
                return NotFound();
            }

            return vender;
        }

        // PUT: api/Venders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin,vendedor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVender(Guid id, Vender vender)
        {
            if (id != vender.VenderId)
            {
                return BadRequest();
            }

            _context.Entry(vender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenderExists(id))
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

        // POST: api/Venders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin,vendedor")]
        [HttpPost]
        public async Task<ActionResult<Vender>> PostVender(Vender vender)
        {
            if (_context.Vendas == null)
            {
                return Problem("Entity set 'MyContext.Vendas'  is null.");
            }
            if (vender.VendaValor < 0)
            {
                return BadRequest("VendaValor não pode ser negativo");
            }

            var client = await _context.Clientes.FindAsync(vender.ClienteId);
            if (client == null)
            {
                return BadRequest("Cliente nao encontrado");

            }

            vender.Cliente = client;

            _context.Vendas.Add(vender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVender", new { id = vender.VenderId }, vender);
        }

        // DELETE: api/Venders/5
        [Authorize(Roles = "admin,vendedor")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVender(Guid id)
        {
            if (_context.Vendas == null)
            {
                return NotFound();
            }
            var vender = await _context.Vendas.FindAsync(id);
            if (vender == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(vender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenderExists(Guid id)
        {
            return (_context.Vendas?.Any(e => e.VenderId == id)).GetValueOrDefault();
        }
    }
}
