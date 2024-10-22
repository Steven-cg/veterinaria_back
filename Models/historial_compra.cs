using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class historial_compra
    {
        [Key]
        public int id_historial_compra { get; set; }
        public int id_usuario { get; set; }
        public decimal valorapagar { get; set; }
        public decimal valorpagado { get; set; }
        public decimal valordevuelto { get; set; }
        public DateTime? fecha { get; set; }
        public string estado { get; set; }
        public string ip { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_actualizacion { get; set; }

        public ICollection<detalle_compra> detalle_compra { get; set; }
        public ICollection<detalle_servicio> detalle_servicio { get; set; }
    }
}
