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
    public class PetsController : ControllerBase
    {
        private readonly MyContext _context;

        public PetsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Pets
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pets>>> GetPetsDono()
        {
            if (_context.PetsDono == null)
            {
                return NotFound();
            }
            return await _context.PetsDono.ToListAsync();
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pets>> GetPets(Guid id)
        {
            if (_context.PetsDono == null)
            {
                return NotFound();
            }
            var pets = await _context.PetsDono.FindAsync(id);

            if (pets == null)
            {
                return NotFound();
            }

            return pets;
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin,vendedor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPets(Guid id, Pets pets)
        {
            if (id != pets.PetsId)
            {
                return BadRequest();
            }

            _context.Entry(pets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetsExists(id))
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

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize(Roles = "admin,vendedor")]
        [HttpPost]
        public async Task<ActionResult<Pets>> PostPets(Pets pets)
        {
            if (_context.PetsDono == null)
            {
                return Problem("Entity set 'MyContext.PetsDono'  is null.");
            }
            _context.PetsDono.Add(pets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPets", new { id = pets.PetsId }, pets);
        }

        // DELETE: api/Pets/5
        [Authorize(Roles = "admin,vendedor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePets(Guid id)
        {
            if (_context.PetsDono == null)
            {
                return NotFound();
            }
            var pets = await _context.PetsDono.FindAsync(id);
            if (pets == null)
            {
                return NotFound();
            }

            _context.PetsDono.Remove(pets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetsExists(Guid id)
        {
            return (_context.PetsDono?.Any(e => e.PetsId == id)).GetValueOrDefault();
        }
    }
}
