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
    public class LimitesGastosController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public LimitesGastosController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: LimitesGastos
        public async Task<IActionResult> Index()
        {
            var sistemaGestionGastosContext = _context.LimitesGastos.Include(l => l.IdCategoriaNavigation).Include(l => l.IdUsuarioNavigation);
            return View(await sistemaGestionGastosContext.ToListAsync());
        }

        // GET: LimitesGastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LimitesGastos == null)
            {
                return NotFound();
            }

            var limitesGasto = await _context.LimitesGastos
                .Include(l => l.IdCategoriaNavigation)
                .Include(l => l.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdLimite == id);
            if (limitesGasto == null)
            {
                return NotFound();
            }

            return View(limitesGasto);
        }

        // GET: LimitesGastos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: LimitesGastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLimite,IdUsuario,IdCategoria,MontoMaximo")] LimitesGasto limitesGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(limitesGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", limitesGasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", limitesGasto.IdUsuario);
            return View(limitesGasto);
        }

        // GET: LimitesGastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LimitesGastos == null)
            {
                return NotFound();
            }

            var limitesGasto = await _context.LimitesGastos.FindAsync(id);
            if (limitesGasto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", limitesGasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", limitesGasto.IdUsuario);
            return View(limitesGasto);
        }

        // POST: LimitesGastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLimite,IdUsuario,IdCategoria,MontoMaximo")] LimitesGasto limitesGasto)
        {
            if (id != limitesGasto.IdLimite)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(limitesGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LimitesGastoExists(limitesGasto.IdLimite))
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
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", limitesGasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", limitesGasto.IdUsuario);
            return View(limitesGasto);
        }

        // GET: LimitesGastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LimitesGastos == null)
            {
                return NotFound();
            }

            var limitesGasto = await _context.LimitesGastos
                .Include(l => l.IdCategoriaNavigation)
                .Include(l => l.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdLimite == id);
            if (limitesGasto == null)
            {
                return NotFound();
            }

            return View(limitesGasto);
        }

        // POST: LimitesGastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LimitesGastos == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.LimitesGastos'  is null.");
            }
            var limitesGasto = await _context.LimitesGastos.FindAsync(id);
            if (limitesGasto != null)
            {
                _context.LimitesGastos.Remove(limitesGasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LimitesGastoExists(int id)
        {
          return (_context.LimitesGastos?.Any(e => e.IdLimite == id)).GetValueOrDefault();
        }
    }
}
