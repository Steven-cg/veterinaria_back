using backend.context;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioMascotaController : ControllerBase
    {
        private readonly AppDbContext context;

        public FormularioMascotaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var mascotas = context.mascota.ToList();
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id_mascota}", Name = "GetMascota")]
        public ActionResult Get(int id_mascota)
        {
            try
            {
                var mascota = context.mascota.FirstOrDefault(m => m.id_mascota == id_mascota);
                if (mascota == null)
                {
                    return NotFound("Mascota no encontrada.");
                }
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] mascota mascota)
        {
            try
            {
                if (mascota == null)
                {
                    return BadRequest("El cuerpo de la solicitud está vacío.");
                }

                mascota.estado = mascota.estado ?? "Inactivo";
                mascota.ip = mascota.ip ?? "0.0.0.0";

                mascota.fecha_creacion = DateTime.UtcNow;
                mascota.fecha_actualizacion = DateTime.UtcNow;

                context.mascota.Add(mascota);
                context.SaveChanges();

                return CreatedAtRoute("GetMascota", new { id_mascota = mascota.id_mascota }, mascota);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar la mascota: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpPut("{id_mascota}")]
        public ActionResult Put(int id_mascota, [FromBody] mascota mascota)
        {
            try
            {
                if (mascota == null || mascota.id_mascota != id_mascota)
                {
                    return BadRequest("El cuerpo de la solicitud no es válido.");
                }

                context.Entry(mascota).State = EntityState.Modified;
                context.SaveChanges();

                return CreatedAtRoute("GetMascota", new { id_mascota = mascota.id_mascota }, mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id_mascota}")]
        public ActionResult Delete(int id_mascota)
        {
            try
            {
                var mascota = context.mascota.FirstOrDefault(m => m.id_mascota == id_mascota);
                if (mascota != null)
                {
                    context.mascota.Remove(mascota);
                    context.SaveChanges();
                    return Ok(id_mascota);
                }
                else
                {
                    return NotFound("El registro no se encontró.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
