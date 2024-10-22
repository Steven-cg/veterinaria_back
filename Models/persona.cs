using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
	public class persona
	{
		[Key]
		public int id { get; set; }

		[Required]
		[MaxLength(100)]
		public string? nombre { get; set; }

		[Required]
		[MaxLength(100)]
		public string? apellido { get; set; }

		[Required]
		public DateTime fecha_nacimiento { get; set; }

		[MaxLength(50)]
		public string? estado_civil { get; set; }

		[Column("estatura(m)")] 
		public int estatura { get; set; }

		[MaxLength(50)]
		public string? estado { get; set; }

		[MaxLength(50)]
		public string? ip { get; set; }

		public DateTime fecha_creacion { get; set; }

		public DateTime fecha_modificacion { get; set; }
	}
}
