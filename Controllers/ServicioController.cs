using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.context;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<servicio>>> GetServicios()
        {
            return await _context.servicio.ToListAsync();
        }

        [HttpGet("{id_servicio}")]
        public async Task<ActionResult<servicio>> GetServicio(int id_servicio)
        {
            var servicio = await _context.servicio.FindAsync(id_servicio);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        [HttpPost]
        public async Task<ActionResult<servicio>> CreateServicio(servicio newServicio)
        {
            newServicio.fecha_creacion = DateTime.Now;
            newServicio.fecha_actualizacion = DateTime.Now;

            _context.servicio.Add(newServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServicio), new { id_servicio = newServicio.id_servicio }, newServicio);
        }

        [HttpPut("{id_servicio}")]
        public async Task<IActionResult> UpdateServicio(int id_servicio, servicio updatedServicio)
        {
            var existingServicio = await _context.servicio.FindAsync(id_servicio);

            if (existingServicio == null)
            {
                return NotFound();
            }

            existingServicio.tipo = updatedServicio.tipo;
            existingServicio.nombre = updatedServicio.nombre;
            existingServicio.concepto = updatedServicio.concepto;
            existingServicio.valor = updatedServicio.valor;
            existingServicio.otro = updatedServicio.otro;
            existingServicio.estado = updatedServicio.estado;
            existingServicio.ip = updatedServicio.ip;
            existingServicio.fecha_actualizacion = DateTime.Now;

            _context.Entry(existingServicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id_servicio}")]
        public async Task<IActionResult> DeleteServicio(int id_servicio)
        {
            var servicio = await _context.servicio.FindAsync(id_servicio);

            if (servicio == null)
            {
                return NotFound();
            }

            _context.servicio.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
