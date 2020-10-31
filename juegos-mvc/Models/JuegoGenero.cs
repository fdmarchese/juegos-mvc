using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace juegos_mvc.Models
{
    public class JuegoGenero
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Genero))]
        public Guid GeneroId { get; set; }
        public Genero Genero { get; set; }

        [ForeignKey(nameof(Juego))]
        public Guid JuegoId { get; set; }
        public Juego Juego { get; set; }
    }
}
