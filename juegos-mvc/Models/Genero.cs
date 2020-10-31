using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Models
{
    public class Genero
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima del campo es de 100 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "El campo admite sólo caracteres alfabéticos")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public List<JuegoGenero> Juegos { get; set; }
    }
}
