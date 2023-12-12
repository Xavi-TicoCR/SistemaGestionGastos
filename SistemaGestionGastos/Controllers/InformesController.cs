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
    public class InformesController : Controller
    {
        private readonly SistemaGestionGastosContext _context;

        public InformesController(SistemaGestionGastosContext context)
        {
            _context = context;
        }

        // GET: Informes
        public async Task<IActionResult> Index()
        {
            var sistemaGestionGastosContext = _context.Informes.Include(i => i.IdUsuarioNavigation);
            return View(await sistemaGestionGastosContext.ToListAsync());
        }

        // GET: Informes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Informes == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes
                .Include(i => i.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdInforme == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // GET: Informes/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Informes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInforme,IdUsuario,Periodo,TotalGastos")] Informe informe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", informe.IdUsuario);
            return View(informe);
        }

        // GET: Informes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Informes == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes.FindAsync(id);
            if (informe == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", informe.IdUsuario);
            return View(informe);
        }

        // POST: Informes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInforme,IdUsuario,Periodo,TotalGastos")] Informe informe)
        {
            if (id != informe.IdInforme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformeExists(informe.IdInforme))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", informe.IdUsuario);
            return View(informe);
        }

        // GET: Informes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Informes == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes
                .Include(i => i.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdInforme == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // POST: Informes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Informes == null)
            {
                return Problem("Entity set 'SistemaGestionGastosContext.Informes'  is null.");
            }
            var informe = await _context.Informes.FindAsync(id);
            if (informe != null)
            {
                _context.Informes.Remove(informe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformeExists(int id)
        {
          return (_context.Informes?.Any(e => e.IdInforme == id)).GetValueOrDefault();
        }
    }
}
