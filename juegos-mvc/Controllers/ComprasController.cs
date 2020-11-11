using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using juegos_mvc.Database;
using juegos_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using juegos_mvc.Models.Enums;
using System.Linq;
using System.Security.Claims;

namespace juegos_mvc.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private readonly PortalJuegosDbContext _context;

        public ComprasController(PortalJuegosDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpGet]
        public IActionResult Index()
        {
            var compras = _context.Compras
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Categoria)
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Consola)
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Generos).ThenInclude(juegoGenero => juegoGenero.Genero)
                .Include(compra => compra.Cliente)
                .ToList();

            return View(compras);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = _context.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Juego)
                .FirstOrDefault(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        [HttpGet("Compras/Juego/{juegoId}")]
        [Authorize(Roles = nameof(Rol.Cliente))]
        public IActionResult Comprar(Guid juegoId)
        {
            var juego = _context.Juegos
                .Include(juego => juego.Categoria)
                .Include(juego => juego.Consola)
                .Include(juego => juego.Generos).ThenInclude(juegoGenero => juegoGenero.Genero)
                .FirstOrDefault(juego => juego.Id == juegoId);

            var compra = new Compra
            {
                Juego = juego,
                JuegoId = juego.Id,
                PrecioOriginal = juego.PrecioOriginal,
                PrecioFinal = juego.PrecioOriginal - (juego.PrecioOriginal * juego.Categoria.PorcentajeDescuento / 100)
            };

            return View(compra);
        }

        [HttpPost("Compras/Juego/{juegoId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Cliente))]
        public IActionResult ComprarPost(Guid juegoId)
        {
            var clienteId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var juego = _context.Juegos
                .Include(juego => juego.Categoria)
                .FirstOrDefault(juego => juego.Id == juegoId);

            if (juego.Stock > 0)
            {
                juego.Stock -= 1;

                var compra = new Compra()
                {
                    Id = Guid.NewGuid(),
                    ClienteId = clienteId,
                    JuegoId = juegoId,
                    FechaCompra = DateTime.Now,
                    PrecioOriginal = juego.PrecioOriginal,
                    PrecioFinal = juego.PrecioOriginal - (juego.PrecioOriginal * juego.Categoria.PorcentajeDescuento / 100)
                };

                _context.Add(compra);
                _context.SaveChanges();
            }
            else
            {
                TempData["Error"] = $"No se dispone de stock para el juego {juego.Titulo}";
            }

            return RedirectToAction(nameof(MisCompras));
        }

        [HttpGet]
        [Authorize(Roles = nameof(Rol.Cliente))]
        public IActionResult MisCompras()
        {
            var clienteId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var compras = _context.Compras
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Categoria)
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Consola)
                .Include(compra => compra.Juego).ThenInclude(juego => juego.Generos).ThenInclude(juegoGenero => juegoGenero.Genero)
                .Where(compra => compra.ClienteId == clienteId)
                .ToList();

            return View(compras);
        }

    }
}
