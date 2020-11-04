using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using juegos_mvc.Database;
using juegos_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using juegos_mvc.Models.Enums;

namespace juegos_mvc.Controllers
{
    [Authorize(Roles = nameof(Rol.Administrador))]
    public class JuegosController : Controller
    {
        private readonly PortalJuegosDbContext _context;

        public JuegosController(PortalJuegosDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var portalJuegosDbContext = _context.Juegos.Include(j => j.Categoria).Include(j => j.Consola);
            return View(await portalJuegosDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.Categoria)
                .Include(j => j.Consola)
                .Include(juego => juego.Generos).ThenInclude(juegoGenero => juegoGenero.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            ViewData["ConsolaId"] = new SelectList(_context.Consolas, "Id", "Descripcion");
            ViewData[nameof(Genero)] = new MultiSelectList(_context.Generos, nameof(Genero.Id), nameof(Genero.Descripcion));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Juego juego, Guid[] generoIds)
        {
            if (ModelState.IsValid)
            {
                juego.Id = Guid.NewGuid();

                foreach (Guid generoId in generoIds)
                {
                    var juegoGenero = new JuegoGenero()
                    {
                        Id = Guid.NewGuid(),
                        GeneroId = generoId,
                        JuegoId = juego.Id
                    };

                    _context.Add(juegoGenero);
                }

                _context.Add(juego);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", juego.CategoriaId);
            ViewData["ConsolaId"] = new SelectList(_context.Consolas, "Id", "Descripcion", juego.ConsolaId);
            ViewData[nameof(Genero)] = new MultiSelectList(_context.Generos, nameof(Genero.Id), nameof(Genero.Descripcion), generoIds);

            return View(juego);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = _context.Juegos
                .Include(juego => juego.Generos)
                .FirstOrDefault(juego => juego.Id == id);

            if (juego == null)
            {
                return NotFound();
            }

            var generoIds = juego.Generos.Select(juegoGenero => juegoGenero.GeneroId);

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", juego.CategoriaId);
            ViewData["ConsolaId"] = new SelectList(_context.Consolas, "Id", "Descripcion", juego.ConsolaId);
            ViewData[nameof(Genero)] = new MultiSelectList(_context.Generos, nameof(Genero.Id), nameof(Genero.Descripcion), generoIds);

            return View(juego);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Juego juego, Guid[] generoIds)
        {
            if (id != juego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var juegoDb = _context.Juegos
                        .Include(juego => juego.Generos)
                        .FirstOrDefault(juego => juego.Id == id);

                    juegoDb.Generos.Clear();

                    foreach (Guid generoId in generoIds)
                    {
                        var juegoGenero = new JuegoGenero()
                        {
                            Id = Guid.NewGuid(),
                            GeneroId = generoId,
                            JuegoId = id
                        };

                        _context.Add(juegoGenero);
                    }

                    juegoDb.AnioLanzamiento = juego.AnioLanzamiento;
                    juegoDb.CategoriaId = juego.CategoriaId;
                    juegoDb.ConsolaId = juego.ConsolaId;
                    juegoDb.PrecioOriginal = juego.PrecioOriginal;
                    juegoDb.Stock = juego.Stock;
                    juegoDb.Titulo = juego.Titulo;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuegoExists(juego.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", juego.CategoriaId);
            ViewData["ConsolaId"] = new SelectList(_context.Consolas, "Id", "Descripcion", juego.ConsolaId);
            return View(juego);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.Categoria)
                .Include(j => j.Consola)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var juego = await _context.Juegos.FindAsync(id);
            _context.Juegos.Remove(juego);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Buscar(string titulo, Guid? consolaId, Guid? categoriaId, Guid? generoId)
        {
            var juegos = _context
                .Juegos
                .Include(x => x.Generos).ThenInclude(x => x.Genero)
                .Include(x => x.Consola)
                .Include(x => x.Categoria)
                .Where(x => (string.IsNullOrWhiteSpace(titulo) || EF.Functions.Like(x.Titulo, $"%{titulo}%"))
                            && (!consolaId.HasValue || x.ConsolaId == consolaId.Value)
                            && (!categoriaId.HasValue || x.CategoriaId == categoriaId.Value)
                            && (!generoId.HasValue || x.Generos.Any(genero => genero.GeneroId == generoId.Value)))
                .ToList();

            ViewBag.Consolas = new SelectList(_context.Consolas, nameof(Consola.Id), nameof(Consola.Descripcion), consolaId);
            ViewBag.Categorias = new SelectList(_context.Categorias, nameof(Categoria.Id), nameof(Categoria.Descripcion), categoriaId);
            ViewBag.Generos = new SelectList(_context.Generos, nameof(Genero.Id), nameof(Genero.Descripcion), generoId);
            ViewBag.Titulo = titulo;

            return View(juegos);
        }


        private bool JuegoExists(Guid id)
        {
            return _context.Juegos.Any(e => e.Id == id);
        }
    }
}
