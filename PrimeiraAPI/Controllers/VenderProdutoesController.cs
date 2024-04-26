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
    public class VenderProdutoesController : ControllerBase
    {
        private readonly MyContext _context;

        public VenderProdutoesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/VenderProdutoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenderProduto>>> GetVenderProdutos()
        {
          if (_context.VenderProdutos == null)
          {
              return NotFound();
          }
            return await _context.VenderProdutos.ToListAsync();
        }

        // GET: api/VenderProdutoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VenderProduto>> GetVenderProduto(int id)
        {
          if (_context.VenderProdutos == null)
          {
              return NotFound();
          }
            var venderProduto = await _context.VenderProdutos.FindAsync(id);

            if (venderProduto == null)
            {
                return NotFound();
            }

            return venderProduto;
        }

        // PUT: api/VenderProdutoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenderProduto(int id, VenderProduto venderProduto)
        {
            if (id != venderProduto.VenderProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(venderProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenderProdutoExists(id))
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

        // POST: api/VenderProdutoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VenderProduto>> PostVenderProduto(VenderProduto venderProduto)
        {
          if (_context.VenderProdutos == null)
          {
              return Problem("Entity set 'MyContext.VenderProdutos'  is null.");
          }
            _context.VenderProdutos.Add(venderProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenderProduto", new { id = venderProduto.VenderProdutoId }, venderProduto);
        }

        // DELETE: api/VenderProdutoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenderProduto(int id)
        {
            if (_context.VenderProdutos == null)
            {
                return NotFound();
            }
            var venderProduto = await _context.VenderProdutos.FindAsync(id);
            if (venderProduto == null)
            {
                return NotFound();
            }

            _context.VenderProdutos.Remove(venderProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenderProdutoExists(int id)
        {
            return (_context.VenderProdutos?.Any(e => e.VenderProdutoId == id)).GetValueOrDefault();
        }
    }
}
