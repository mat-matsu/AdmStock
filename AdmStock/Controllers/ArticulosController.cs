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
    public class ArticulosController : Controller
    {
        private readonly AdmStockContext _context;

        public ArticulosController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Articuloes
        public async Task<IActionResult> Index(string sortOrder, string searchStrArt)
        {
            ViewBag.Message = "Lista de Articulos.";
            ViewBag.ArtSortParm = String.IsNullOrEmpty(sortOrder) ? "art_desc" : "";

            var admStockContext = _context.Articulos.OrderBy(a => a.art_id);

            admStockContext = (IOrderedQueryable<Articulo>)admStockContext
                    .Where(a => a.tipo_prod.Contains(String.IsNullOrEmpty(searchStrArt) ? "" : searchStrArt));

            switch (sortOrder)
            {
                case "art_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.art_id);
                    break;
                default:
                    //admStockContext = admStockContext.OrderBy(a => a.art_id);
                    ViewBag.ArtSortParm = "art_desc";
                    break;
            }

            return View(await admStockContext.ToListAsync());
        }

        // GET: Articuloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(m => m.art_id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Articuloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articuloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("art_id,tipo_prod")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articuloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // POST: Articuloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("art_id,tipo_prod")] Articulo articulo)
        {
            if (id != articulo.art_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.art_id))
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
            return View(articulo);
        }

        // GET: Articuloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .FirstOrDefaultAsync(m => m.art_id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articuloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articulos == null)
            {
                return Problem("Entity set 'AdmStockContext.Articulos'  is null.");
            }
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
          return (_context.Articulos?.Any(e => e.art_id == id)).GetValueOrDefault();
        }
    }
}
