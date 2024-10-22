using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class producto
    {
        [Key]
        public int id_producto { get; set; }

        public string? nombre { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal valor { get; set; }

        public string? estado { get; set; }

        public string? ip { get; set; }

        public string? imagen { get; set; }

        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        public DateTime fecha_actualizacion { get; set; } = DateTime.Now;
    }
}
