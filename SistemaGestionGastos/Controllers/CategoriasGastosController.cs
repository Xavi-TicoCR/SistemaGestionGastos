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
    public class CategoriasGastosController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public CategoriasGastosController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: CategoriasGastos
        public async Task<IActionResult> Index()
        {
              return _context.CategoriasGastos != null ? 
                          View(await _context.CategoriasGastos.ToListAsync()) :
                          Problem("Entity set 'SistemaGestionGastosContext.CategoriasGastos'  is null.");
        }

        // GET: CategoriasGastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriasGastos == null)
            {
                return NotFound();
            }

            var categoriasGasto = await _context.CategoriasGastos
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoriasGasto == null)
            {
                return NotFound();
            }

            return View(categoriasGasto);
        }

        // GET: CategoriasGastos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasGastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,NombreCategoria,Descripcion")] CategoriasGasto categoriasGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriasGasto);
        }

        // GET: CategoriasGastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriasGastos == null)
            {
                return NotFound();
            }

            var categoriasGasto = await _context.CategoriasGastos.FindAsync(id);
            if (categoriasGasto == null)
            {
                return NotFound();
            }
            return View(categoriasGasto);
        }

        // POST: CategoriasGastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,NombreCategoria,Descripcion")] CategoriasGasto categoriasGasto)
        {
            if (id != categoriasGasto.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriasGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasGastoExists(categoriasGasto.IdCategoria))
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
            return View(categoriasGasto);
        }

        // GET: CategoriasGastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriasGastos == null)
            {
                return NotFound();
            }

            var categoriasGasto = await _context.CategoriasGastos
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoriasGasto == null)
            {
                return NotFound();
            }

            return View(categoriasGasto);
        }

        // POST: CategoriasGastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriasGastos == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.CategoriasGastos'  is null.");
            }
            var categoriasGasto = await _context.CategoriasGastos.FindAsync(id);
            if (categoriasGasto != null)
            {
                _context.CategoriasGastos.Remove(categoriasGasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasGastoExists(int id)
        {
          return (_context.CategoriasGastos?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
