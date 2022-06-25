using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmStock.Context;
using AdmStock.Models;

namespace AdmStock.Controllers
{
    public class LotesController : Controller
    {
        private readonly AdmStockContext _context;

        public LotesController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Lotes
        public async Task<IActionResult> Index()
        {
            var admStockContext = _context.Lotes.Include(l => l.Productos).Include(l => l.Proveedores);
            return View(await admStockContext.ToListAsync());
        }

        // GET: Lotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes
                .Include(l => l.Productos)
                .Include(l => l.Proveedores)
                .FirstOrDefaultAsync(m => m.lote_id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // GET: Lotes/Create
        public IActionResult Create()
        {
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_desc");
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_cuil");
            return View();
        }

        // POST: Lotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("lote_id,prod_id,prov_id,lote_cant,lote_precio")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_desc", lote.prod_id);
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_cuil", lote.prov_id);
            return View(lote);
        }

        // GET: Lotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                return NotFound();
            }
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_desc", lote.prod_id);
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_cuil", lote.prov_id);
            return View(lote);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("lote_id,prod_id,prov_id,lote_cant,lote_precio")] Lote lote)
        {
            if (id != lote.lote_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoteExists(lote.lote_id))
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
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_desc", lote.prod_id);
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_cuil", lote.prov_id);
            return View(lote);
        }

        // GET: Lotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes
                .Include(l => l.Productos)
                .Include(l => l.Proveedores)
                .FirstOrDefaultAsync(m => m.lote_id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lotes == null)
            {
                return Problem("Entity set 'AdmStockContext.Lotes'  is null.");
            }
            var lote = await _context.Lotes.FindAsync(id);
            if (lote != null)
            {
                _context.Lotes.Remove(lote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoteExists(int id)
        {
          return (_context.Lotes?.Any(e => e.lote_id == id)).GetValueOrDefault();
        }
    }
}
