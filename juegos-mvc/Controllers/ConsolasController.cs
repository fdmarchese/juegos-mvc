using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using juegos_mvc.Database;
using juegos_mvc.Models;

namespace juegos_mvc.Controllers
{
    public class ConsolasController : Controller
    {
        private readonly PortalJuegosDbContext _context;

        public ConsolasController(PortalJuegosDbContext context)
        {
            _context = context;
        }

        // GET: Consolas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consolas.ToListAsync());
        }

        // GET: Consolas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consola == null)
            {
                return NotFound();
            }

            return View(consola);
        }

        // GET: Consolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consolas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,IconoCss")] Consola consola)
        {
            if (ModelState.IsValid)
            {
                consola.Id = Guid.NewGuid();
                _context.Add(consola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consola);
        }

        // GET: Consolas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas.FindAsync(id);
            if (consola == null)
            {
                return NotFound();
            }
            return View(consola);
        }

        // POST: Consolas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descripcion,IconoCss")] Consola consola)
        {
            if (id != consola.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsolaExists(consola.Id))
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
            return View(consola);
        }

        // GET: Consolas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consola == null)
            {
                return NotFound();
            }

            return View(consola);
        }

        // POST: Consolas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var consola = await _context.Consolas.FindAsync(id);
            _context.Consolas.Remove(consola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsolaExists(Guid id)
        {
            return _context.Consolas.Any(e => e.Id == id);
        }
    }
}
