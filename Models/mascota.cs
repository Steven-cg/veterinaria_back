using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class mascota
    {
        [Key]
        public int id_mascota { get; set; }  

        [Required]
        public int id_usuario { get; set; }  

        [Required]
        [MaxLength(100)]
        public string nombre { get; set; }

        [Required]
        public int edad { get; set; }

        [MaxLength(100)]
        public string? raza { get; set; }

        [MaxLength(100)]   
        public string? especie { get; set; }

        [MaxLength(50)]
        public string? estado { get; set; }

        [MaxLength(45)] 
        public string? ip { get; set; }

        public DateTime fecha_creacion { get; set; }  

        public DateTime fecha_actualizacion { get; set; }
    }
}
