using backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class detalle_servicio
{
    [Key]
    public int id_detalle_servicio { get; set; }

    public int? id_historial_compra { get; set; }   

    public int id_servicio { get; set; } 

    public decimal valor { get; set; }
    public char operacion { get; set; }

    public DateTime? fecha_creacion { get; set; }
    public DateTime? fecha_actualizacion { get; set; }
}
