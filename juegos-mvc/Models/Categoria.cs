using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de {1} caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de {1} caracteres")]
        [RegularExpression(@"[a-zA-Z0-9_\- ]*", ErrorMessage = "Sólo admite caracteres alfanuméricos, '_' y '-'.")]
        [Display(Name = "Clase de css")]
        public string Css { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0D, 100D, ErrorMessage = "El porcentaje de descuento debe estar comprendido entre {1} y {2}")]
        [Display(Name = "Porcentaje de descuento")]
        public decimal PorcentajeDescuento { get; set; }

        public List<Juego> Juegos { get; set; }

    }
}
