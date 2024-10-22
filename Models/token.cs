using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Token
    {
        [Key]
        public int Id { get; set; }

        public int IdUser { get; set; }

        [Required, MaxLength(500)]
        public string TokenValue { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
