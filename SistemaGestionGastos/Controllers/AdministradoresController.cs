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
    public class AdministradoresController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public AdministradoresController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: Administradores
        public async Task<IActionResult> Index()
        {
            var sistemaGestionGastosContext = _context.Administradores.Include(a => a.IdUsuarioNavigation);
            return View(await sistemaGestionGastosContext.ToListAsync());
        }

        // GET: Administradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Administradores == null)
            {
                return NotFound();
            }

            var administradore = await _context.Administradores
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAdministrador == id);
            if (administradore == null)
            {
                return NotFound();
            }

            return View(administradore);
        }

        // GET: Administradores/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Administradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdministrador,IdUsuario,Rol")] Administradore administradore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administradore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", administradore.IdUsuario);
            return View(administradore);
        }

        // GET: Administradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Administradores == null)
            {
                return NotFound();
            }

            var administradore = await _context.Administradores.FindAsync(id);
            if (administradore == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", administradore.IdUsuario);
            return View(administradore);
        }

        // POST: Administradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdministrador,IdUsuario,Rol")] Administradore administradore)
        {
            if (id != administradore.IdAdministrador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administradore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradoreExists(administradore.IdAdministrador))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", administradore.IdUsuario);
            return View(administradore);
        }

        // GET: Administradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Administradores == null)
            {
                return NotFound();
            }

            var administradore = await _context.Administradores
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAdministrador == id);
            if (administradore == null)
            {
                return NotFound();
            }

            return View(administradore);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Administradores == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.Administradores'  is null.");
            }
            var administradore = await _context.Administradores.FindAsync(id);
            if (administradore != null)
            {
                _context.Administradores.Remove(administradore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministradoreExists(int id)
        {
          return (_context.Administradores?.Any(e => e.IdAdministrador == id)).GetValueOrDefault();
        }
    }
}
