using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Models
{
    public class Consola
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de 200 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de 200 caracteres")]
        [RegularExpression(@"[a-zA-Z0-9_\- ]*", ErrorMessage = "Sólo admite caracteres alfanuméricos, '_' y '-'.")]
        [Display(Name = "Clase de css para el ícono")]
        public string IconoCss { get; set; }

        public List<Juego> Juegos { get; set; }
    }
}
