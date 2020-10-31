using System;
using System.ComponentModel.DataAnnotations;

namespace juegos_mvc.Validation
{
    public class AnioDesdeHastaHoyAttribute : ValidationAttribute
    {
        private readonly int _anioDesde;

        public AnioDesdeHastaHoyAttribute(int anioDesde)
        {
            _anioDesde = anioDesde;
            ErrorMessage = "El año de debe estar comprendido entre {0} y {1}.";
        }

        public override bool IsValid(object value)
        {
            if (value is int anioLanzamiento)
            {
                return anioLanzamiento > _anioDesde && anioLanzamiento <= DateTime.Now.Year;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, _anioDesde, DateTime.Now.Year);
        }
    }
}
