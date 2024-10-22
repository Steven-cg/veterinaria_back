using backend.context;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FormularioController : ControllerBase
	{

		private readonly AppDbContext context;

		public FormularioController(AppDbContext context) { 
			this.context = context;
		}

		[HttpGet]
		public ActionResult Get()
		{
			try
			{
				return Ok(context.persona.ToList());
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetGestor")]
		public ActionResult Get(int id)
		{
			try
			{
				var gestor = context.persona.FirstOrDefault(g=> g.id == id);

				return Ok(gestor);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public ActionResult Post([FromBody] persona gestor)
		{
			try
			{
				if (gestor == null)
				{
					return BadRequest("El cuerpo de la solicitud está vacío.");
				}

				gestor.estado_civil = gestor.estado_civil ?? "No especificado";
				gestor.estado = gestor.estado ?? "Inactivo";
				gestor.ip = gestor.ip ?? "192.168.1.1";

				
				if (gestor.fecha_creacion == default(DateTime))
				{
					gestor.fecha_creacion = DateTime.UtcNow;
				}

				if (gestor.fecha_modificacion == default(DateTime))
				{
					gestor.fecha_modificacion = DateTime.UtcNow;
				}

				context.persona.Add(gestor);
				context.SaveChanges();

				return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor);
			}
			catch (Exception ex)
			{
				return BadRequest($"Error al guardar la persona: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] persona gestor)
		{
			try
			{
				if (gestor == null || gestor.id != id)
				{
					return BadRequest("El cuerpo de la solicitud no es válido.");
				}

				context.Entry(gestor).State = EntityState.Modified;
				context.SaveChanges();
				return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			try
			{
				var gestor = context.persona.FirstOrDefault(g => g.id == id);
				if (gestor != null)
				{
					context.persona.Remove(gestor);
					context.SaveChanges(); 
					return Ok(id);
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
