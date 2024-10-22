using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        public long cedula { get; set; }

        [Required, MaxLength(100)]
        public string? nombre { get; set; }

        [Required, MaxLength(100)]
        public string? apellido { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string? correo { get; set; }

        [Required, MaxLength(50)]
        public string? usuario_name { get; set; }

        [Required, DataType(DataType.Password), MaxLength(255)]
        public string? contrasena { get; set; }

        [Range(0.0, 999.99)]
        public decimal? estatura { get; set; }

        [MaxLength(20)]
        public string? estado_civil { get; set; }

        [DataType(DataType.Date)]
        public DateTime? fecha_nacimiento { get; set; }

        [MaxLength(50)]
        public string? estado { get; set; }

        [MaxLength(45)]
        public string? ip { get; set; }

        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        public DateTime fecha_actualizacion { get; set; } = DateTime.Now;
    }
}

