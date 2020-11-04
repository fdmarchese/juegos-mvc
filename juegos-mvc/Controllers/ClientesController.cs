using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using juegos_mvc.Database;
using juegos_mvc.Models;
using usando_seguridad.Extensions;
using Microsoft.AspNetCore.Authorization;
using juegos_mvc.Models.Enums;

namespace juegos_mvc.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly PortalJuegosDbContext _context;

        public ClientesController(PortalJuegosDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente, string pass)
        {
            try
            {
                pass.ValidarPassword();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Cliente.Password), ex.Message);
            }


            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                cliente.FechaAlta = DateTime.Now;
                cliente.FechaUltimaModificacion = DateTime.Now;
                cliente.Password = pass.Encriptar();
                _context.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Cliente cliente, string pass)
        {
            if (!string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    pass.ValidarPassword();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(Cliente.Password), ex.Message);
                }
            }

            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var clienteDb = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);

                    clienteDb.Nombre = cliente.Nombre;
                    clienteDb.Apellido = cliente.Apellido;
                    clienteDb.FechaDeNacimiento = cliente.FechaDeNacimiento;
                    clienteDb.Username = cliente.Username;
                    clienteDb.Dni = cliente.Dni;

                    if (!string.IsNullOrWhiteSpace(pass))
                    {
                        clienteDb.Password = pass.Encriptar();
                    }

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
