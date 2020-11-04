using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using juegos_mvc.Database;
using juegos_mvc.Models;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> Index()
        {
            var portalJuegosDbContext = _context.Compras.Include(c => c.Cliente).Include(c => c.Juego);
            return View(await portalJuegosDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Juego)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido");
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JuegoId,ClienteId,FechaCompra,PrecioOriginal,PrecioFinal")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                compra.Id = Guid.NewGuid();
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", compra.ClienteId);
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Titulo", compra.JuegoId);
            return View(compra);
        }
    }
}
