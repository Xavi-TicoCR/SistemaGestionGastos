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
    public class GastosController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public GastosController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var sistemaGestionGastosContext = _context.Gastos.Include(g => g.IdCategoriaNavigation).Include(g => g.IdUsuarioNavigation);
            return View(await sistemaGestionGastosContext.ToListAsync());
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.IdCategoriaNavigation)
                .Include(g => g.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdGasto == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGasto,IdUsuario,IdCategoria,Cantidad,Fecha,Descripcion")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", gasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", gasto.IdUsuario);
            return View(gasto);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", gasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", gasto.IdUsuario);
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGasto,IdUsuario,IdCategoria,Cantidad,Fecha,Descripcion")] Gasto gasto)
        {
            if (id != gasto.IdGasto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.IdGasto))
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
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasGastos, "IdCategoria", "IdCategoria", gasto.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", gasto.IdUsuario);
            return View(gasto);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.IdCategoriaNavigation)
                .Include(g => g.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdGasto == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gastos == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.Gastos'  is null.");
            }
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
          return (_context.Gastos?.Any(e => e.IdGasto == id)).GetValueOrDefault();
        }
    }
}
