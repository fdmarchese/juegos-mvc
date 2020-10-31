using System;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Models
{
    public abstract class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima del campo es de 100 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "El campo admite sólo caracteres alfabéticos")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima del campo es de 100 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "El campo admite sólo caracteres alfabéticos")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Alta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        public DateTime FechaAlta { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Ultima modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        public DateTime? FechaUltimaModificacion { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Ultimo acceso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        public DateTime? FechaUltimoAcceso { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(50, ErrorMessage = "La longitud máxima del campo es de 50 caracteres")]
        [RegularExpression(@"[a-zA-Z0-9_\-]*", ErrorMessage = "El campo admite sólo caracteres alfanuméricos, guión bajo o guión medio")]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Constraseña")]
        public byte[] Password { get; set; }

        public string NombreYApellido => $"{Nombre} {Apellido}";
    }
}
