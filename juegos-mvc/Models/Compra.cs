using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace juegos_mvc.Models
{
    public class Compra
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Juego))]
        public Guid JuegoId { get; set; }
        public Juego Juego { get; set; }

        [ForeignKey(nameof(Cliente))]
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime FechaCompra { get; set; }

        public decimal PrecioOriginal { get; set; }

        public decimal PrecioFinal { get; set; }
    }
}
