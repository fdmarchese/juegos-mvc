using System;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Models
{
    public class Administrador : Usuario
    {
        [ScaffoldColumn(false)]
        public Guid Legajo { get; set; }
    }
}
