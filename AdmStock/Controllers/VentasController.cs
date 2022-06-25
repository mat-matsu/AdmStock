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
    public class VentasController : Controller
    {
        private readonly AdmStockContext _context;

        public VentasController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var admStockContext = _context.Ventas.Include(v => v.Clientes).Include(v => v.Lote);
            return View(await admStockContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Clientes)
                .Include(v => v.Lote)
                .FirstOrDefaultAsync(m => m.venta_id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_dir");
            ViewData["lote_id"] = new SelectList(_context.Lotes, "lote_id", "lote_id");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("venta_id,lote_id,cliente_id,venta_cant,venta_fecha")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_dir", venta.cliente_id);
            ViewData["lote_id"] = new SelectList(_context.Lotes, "lote_id", "lote_id", venta.lote_id);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_dir", venta.cliente_id);
            ViewData["lote_id"] = new SelectList(_context.Lotes, "lote_id", "lote_id", venta.lote_id);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("venta_id,lote_id,cliente_id,venta_cant,venta_fecha")] Venta venta)
        {
            if (id != venta.venta_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.venta_id))
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
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_dir", venta.cliente_id);
            ViewData["lote_id"] = new SelectList(_context.Lotes, "lote_id", "lote_id", venta.lote_id);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Clientes)
                .Include(v => v.Lote)
                .FirstOrDefaultAsync(m => m.venta_id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'AdmStockContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.venta_id == id)).GetValueOrDefault();
        }
    }
}
