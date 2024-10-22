using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.context;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleCompraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDetalleCompras()
        {
            var detalleCompras = _context.detalle_compra.ToList();
            return Ok(detalleCompras);
        }

        [HttpGet("{id_detalle_compra}")]
        public IActionResult GetDetalleCompraById(int id_detalle_compra)
        {
            var detalleCompra = _context.detalle_compra.Find(id_detalle_compra);
            if (detalleCompra == null)
                return NotFound();

            return Ok(detalleCompra);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetalleCompra([FromBody] detalle_compra newDetalleCompra)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            newDetalleCompra.fecha_creacion = DateTime.UtcNow;
            newDetalleCompra.fecha_actualizacion = DateTime.UtcNow;

            _context.detalle_compra.Add(newDetalleCompra);
            await _context.SaveChangesAsync();
            return Ok(newDetalleCompra);
        }


        [HttpPut("{id_detalle_compra}")]
        public async Task<IActionResult> UpdateDetalleCompra(int id_detalle_compra, [FromBody] detalle_compra updatedDetalleCompra)
        {
            var detalleCompra = _context.detalle_compra.Find(id_detalle_compra);
            if (detalleCompra == null)
                return NotFound();

            _context.Entry(detalleCompra).CurrentValues.SetValues(updatedDetalleCompra);
            await _context.SaveChangesAsync();
            return Ok(updatedDetalleCompra);
        }

        [HttpDelete("{id_detalle_compra}")]
        public async Task<IActionResult> DeleteDetalleCompra(int id_detalle_compra)
        {
            var detalleCompra = _context.detalle_compra.Find(id_detalle_compra);
            if (detalleCompra == null)
                return NotFound();

            _context.detalle_compra.Remove(detalleCompra);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
