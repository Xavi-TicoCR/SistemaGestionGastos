using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionGastos.Models;

namespace SistemaGestionGastos.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public NotificacionesController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: Notificaciones
        public async Task<IActionResult> Index()
        {
            var sistemaGestionGastosContext = _context.Notificaciones.Include(n => n.IdUsuarioNavigation);
            return View(await sistemaGestionGastosContext.ToListAsync());
        }

        // GET: Notificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notificaciones == null)
            {
                return NotFound();
            }

            var notificacione = await _context.Notificaciones
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNotificacion == id);
            if (notificacione == null)
            {
                return NotFound();
            }

            return View(notificacione);
        }

        // GET: Notificaciones/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Notificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNotificacion,IdUsuario,Mensaje,Fecha,Estado")] Notificacione notificacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notificacione.IdUsuario);
            return View(notificacione);
        }

        // GET: Notificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notificaciones == null)
            {
                return NotFound();
            }

            var notificacione = await _context.Notificaciones.FindAsync(id);
            if (notificacione == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notificacione.IdUsuario);
            return View(notificacione);
        }

        // POST: Notificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNotificacion,IdUsuario,Mensaje,Fecha,Estado")] Notificacione notificacione)
        {
            if (id != notificacione.IdNotificacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacioneExists(notificacione.IdNotificacion))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notificacione.IdUsuario);
            return View(notificacione);
        }

        // GET: Notificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notificaciones == null)
            {
                return NotFound();
            }

            var notificacione = await _context.Notificaciones
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNotificacion == id);
            if (notificacione == null)
            {
                return NotFound();
            }

            return View(notificacione);
        }

        // POST: Notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notificaciones == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.Notificaciones'  is null.");
            }
            var notificacione = await _context.Notificaciones.FindAsync(id);
            if (notificacione != null)
            {
                _context.Notificaciones.Remove(notificacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacioneExists(int id)
        {
          return (_context.Notificaciones?.Any(e => e.IdNotificacion == id)).GetValueOrDefault();
        }
    }
}
