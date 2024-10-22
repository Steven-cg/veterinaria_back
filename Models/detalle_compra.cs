using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class detalle_compra
    {
        [Key]
        public int id_detalle_compra { get; set; }

        public int? id_historial_compra { get; set; } 

        public int id_producto { get; set; } 

        public int cantidad { get; set; }
        public decimal valor { get; set; }
        public decimal total { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
    }
}
