using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.context;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleServicioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDetalleServicios()
        {
            var detalleServicios = _context.detalle_servicio.ToList();
            return Ok(detalleServicios);
        }

        [HttpGet("{id_detalle_servicio}")]
        public IActionResult GetDetalleServicioById(int id_detalle_servicio)
        {
            var detalleServicio = _context.detalle_servicio.Find(id_detalle_servicio);
            if (detalleServicio == null)
                return NotFound();

            return Ok(detalleServicio);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDetalleServicio([FromBody] detalle_servicio newDetalleServicio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            newDetalleServicio.fecha_creacion = DateTime.UtcNow;
            newDetalleServicio.fecha_actualizacion = DateTime.UtcNow;

            _context.detalle_servicio.Add(newDetalleServicio);
            await _context.SaveChangesAsync();
            return Ok(newDetalleServicio);
        }


        [HttpPut("{id_detalle_servicio}")]
        public async Task<IActionResult> UpdateDetalleServicio(int id_detalle_servicio, [FromBody] detalle_servicio updatedDetalleServicio)
        {
            var detalleServicio = _context.detalle_servicio.Find(id_detalle_servicio);
            if (detalleServicio == null)
                return NotFound();

            _context.Entry(detalleServicio).CurrentValues.SetValues(updatedDetalleServicio);
            await _context.SaveChangesAsync();
            return Ok(updatedDetalleServicio);
        }

        [HttpDelete("{id_detalle_servicio}")]
        public async Task<IActionResult> DeleteDetalleServicio(int id_detalle_servicio)
        {
            var detalleServicio = _context.detalle_servicio.Find(id_detalle_servicio);
            if (detalleServicio == null)
                return NotFound();

            _context.detalle_servicio.Remove(detalleServicio);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
