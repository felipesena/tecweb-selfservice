using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Model;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComidasController : ControllerBase
    {
        private readonly AplicacaoDbContext _context;

        public ComidasController(AplicacaoDbContext context)
        {
            _context = context;
        }

        // GET: api/Comidas
        [HttpGet]
        public IEnumerable<Comida> GetComidas()
        {
            return _context.Comidas;
        }

        // GET: api/Comidas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comida = await _context.Comidas.FindAsync(id);

            if (comida == null)
            {
                return NotFound();
            }

            return Ok(comida);
        }

        // PUT: api/Comidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComida([FromRoute] int id, [FromBody] Comida comida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comida.ComidaId)
            {
                return BadRequest();
            }

            _context.Entry(comida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComidaExists(id))
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

        // POST: api/Comidas
        [HttpPost]
        public async Task<IActionResult> PostComida([FromBody] Comida comida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comidas.Add(comida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComida", new { id = comida.ComidaId }, comida);
        }

        // DELETE: api/Comidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comida = await _context.Comidas.FindAsync(id);
            if (comida == null)
            {
                return NotFound();
            }

            _context.Comidas.Remove(comida);
            await _context.SaveChangesAsync();

            return Ok(comida);
        }

        private bool ComidaExists(int id)
        {
            return _context.Comidas.Any(e => e.ComidaId == id);
        }
    }
}