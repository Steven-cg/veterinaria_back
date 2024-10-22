using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<producto>>> GetProductos()
        {
            return await _context.producto.ToListAsync();
        }

        [HttpGet("{id_producto}")]
        public async Task<ActionResult<producto>> GetProducto(int id_producto)
        {
            var producto = await _context.producto.FindAsync(id_producto);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<producto>> PostProducto(producto nuevoProducto)
        {
            _context.producto.Add(nuevoProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id_producto = nuevoProducto.id_producto }, nuevoProducto);
        }

        [HttpPut("{id_producto}")]
        public async Task<IActionResult> PutProducto(int id_producto, producto updatedProducto)
        {
            if (id_producto != updatedProducto.id_producto)
            {
                return BadRequest();
            }

            _context.Entry(updatedProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id_producto))
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

        [HttpDelete("{id_producto}")]
        public async Task<IActionResult> DeleteProducto(int id_producto)
        {
            var producto = await _context.producto.FindAsync(id_producto);
            if (producto == null)
            {
                return NotFound();
            }

            _context.producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id_producto)
        {
            return _context.producto.Any(e => e.id_producto == id_producto);
        }
    }
}
