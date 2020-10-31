using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace juegos_mvc.Models
{
    public class Juego
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Precio original")]
        public decimal PrecioOriginal { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Año de lanzamiento")]
        public int AnioLanzamiento { get; set; }

        [ForeignKey(nameof(Categoria))]
        [Display(Name = "Categoría")]
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [ForeignKey(nameof(Consola))]
        [Display(Name = "Consola")]
        public Guid ConsolaId { get; set; }
        public Consola Consola { get; set; }

        public List<Compra> Compras { get; set; }
        public List<JuegoGenero> Generos { get; set; }
    }
}
