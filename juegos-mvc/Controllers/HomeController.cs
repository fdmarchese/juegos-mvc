using juegos_mvc.Database;
using juegos_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace juegos_mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly PortalJuegosDbContext _context;
        public HomeController(PortalJuegosDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Consolas = new SelectList(_context.Consolas, nameof(Consola.Id), nameof(Consola.Descripcion));
            ViewBag.Categorias = new SelectList(_context.Categorias, nameof(Categoria.Id), nameof(Categoria.Descripcion));
            ViewBag.Generos = new SelectList(_context.Generos, nameof(Genero.Id), nameof(Genero.Descripcion));

            return View();
        }
    }
}
