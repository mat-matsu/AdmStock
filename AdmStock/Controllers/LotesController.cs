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
        public async Task<IActionResult> Index(string sortOrder, int searchIntLote, string searchStrProv, string searchStrProd)
        {
            ViewBag.Message = "Lista de lotes de productos.";
            ViewBag.LoteSortParm = String.IsNullOrEmpty(sortOrder) ? "lote_desc" : "";
            ViewBag.ProvSortParm = sortOrder == "prov_asc" ? "prov_desc" : "prov_asc";
            ViewBag.ProdSortParm = sortOrder == "prod_asc" ? "prod_desc" : "prod_asc";

            var admStockContext = _context.Lotes.Include(l => l.Productos).Include(l => l.Proveedores).Where(l => l.lote_cant > 0).OrderBy(l => l.lote_id);

            if (searchIntLote > 0) {
                admStockContext = (IOrderedQueryable<Lote>)admStockContext
                    .Where(a =>
                        a.lote_id == searchIntLote
                        &&
                        a.Proveedores.prov_nom.Contains(String.IsNullOrEmpty(searchStrProv) ? "" : searchStrProv)
                        &&
                        a.Productos.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                    );
            } else
            {
                admStockContext = (IOrderedQueryable<Lote>)admStockContext
                    .Where(a =>
                        a.Proveedores.prov_nom.Contains(String.IsNullOrEmpty(searchStrProv) ? "" : searchStrProv)
                        &&
                        a.Productos.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                    );
            }

            switch (sortOrder)
            {
                case "lote_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.lote_id);
                    break;
                case "prov_asc":
                    admStockContext = admStockContext.OrderBy(a => a.Proveedores.prov_nom);
                    break;
                case "prov_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.Proveedores.prov_nom);
                    break;
                case "prod_asc":
                    admStockContext = admStockContext.OrderBy(a => a.Productos.prod_nom);
                    break;
                case "prod_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.Productos.prod_nom);
                    break;
                default:
                    //admStockContext = admStockContext.OrderBy(a => a.art_id);
                    ViewBag.LoteSortParm = "lote_desc";
                    ViewBag.ProvSortParm = "prov_desc";
                    ViewBag.ProdSortParm = "prod_desc";
                    break;
            }

            return View(await admStockContext.ToListAsync());
            /*ViewLoteProdProv myModel = new ViewLoteProdProv();
            myModel.lotes = _context.Lotes;
            myModel.productos = _context.Productos;
            myModel.proveedores = _context.Proveedores;
            
            return View(myModel);
            */
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
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_nom");
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_nom");
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
                lote.Productos = await _context.Productos.FindAsync(lote.prod_id);
                lote.Proveedores = await _context.Proveedores.FindAsync(lote.prov_id);

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
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_nom", lote.prod_id);
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_nom", lote.prov_id);
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
            ViewData["prod_id"] = new SelectList(_context.Productos, "prod_id", "prod_nom", lote.prod_id);
            ViewData["prov_id"] = new SelectList(_context.Proveedores, "prov_id", "prov_nom", lote.prov_id);
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
