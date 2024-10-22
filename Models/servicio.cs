using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class servicio
    {
        [Key] 
        public int id_servicio { get; set; }

        public int tipo { get; set; }

        [StringLength(100)]
        public string? nombre { get; set; }

        [StringLength(255)]
        public string? concepto { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal valor { get; set; }

        [StringLength(255)]
        public string? otro { get; set; }

        [StringLength(50)]
        public string? estado { get; set; }

        [StringLength(45)]
        public string? ip { get; set; }

        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        public DateTime fecha_actualizacion { get; set; } = DateTime.Now;
    }
}
