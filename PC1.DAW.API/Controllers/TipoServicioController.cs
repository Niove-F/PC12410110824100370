using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Infrastructure.Data;

namespace PC1.DAW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : ControllerBase
    {
        private readonly TallerDbContext _context;

        public TipoServicioController(TallerDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoServicio
        [HttpGet]
        public async Task<IActionResult> GetTipoServicios()
        {
            var tipos = await _context.TipoServicio.ToListAsync();
            return Ok(tipos);
        }

        // GET: api/TipoServicio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoServicioById(int id)
        {
            var tipo = await _context.TipoServicio.FindAsync(id);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        // POST: api/TipoServicio
        [HttpPost]
        public async Task<IActionResult> CreateTipoServicio(TipoServicio tipoServicio)
        {
            if (tipoServicio == null) return BadRequest();

            _context.TipoServicio.Add(tipoServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoServicioById), new { id = tipoServicio.ID_TS }, tipoServicio);
        }

        // PUT: api/TipoServicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoServicio(int id, TipoServicio tipoServicio)
        {
            if (tipoServicio == null || id != tipoServicio.ID_TS) return BadRequest();

            // Mark entity as modified
            _context.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.TipoServicio.AnyAsync(e => e.ID_TS == id);
                if (!exists) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/TipoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServicio(int id)
        {
            var tipo = await _context.TipoServicio.FindAsync(id);
            if (tipo == null) return NotFound();

            _context.TipoServicio.Remove(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
