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
    public class ProductosController : Controller
    {
        private readonly AdmStockContext _context;

        public ProductosController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Productoes
        /*
        public async Task<IActionResult> Index()
        {
            ViewBag.Filters = _context.Articulos.ToList();
            var admStockContext = _context.Productos.Include(p => p.Articulos);
            return View(await admStockContext.ToListAsync());
        }
        */
        public async Task<IActionResult> Index(string sortOrder, string searchStrArt, string searchStrProd)
        {
            ViewBag.Message = "Listado de productos";
            ViewBag.ArtSortParm = String.IsNullOrEmpty(sortOrder) ? "art_desc" : "";
            ViewBag.ProdSortParm = sortOrder == "prod_asc" ? "prod_desc" : "prod_asc";

            var admStockContext = _context.Productos.Include(p => p.Articulos).OrderBy(a => a.Articulos.tipo_prod);
            /*
            if (!String.IsNullOrEmpty(searchStrArt) || !String.IsNullOrEmpty(searchStrProd))
            {
                admStockContext = (IOrderedQueryable<Producto>) admStockContext
                    .Where(a =>
                        a.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                        &&
                        a.Articulos.tipo_prod.Contains(String.IsNullOrEmpty(searchStrArt) ? "" : searchStrArt)
                    );
            }
            */

            admStockContext = (IOrderedQueryable<Producto>)admStockContext
                .Where(a =>
                    a.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                    &&
                    a.Articulos.tipo_prod.Contains(String.IsNullOrEmpty(searchStrArt) ? "" : searchStrArt)
                );

            switch (sortOrder)
            {
                case "art_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.Articulos.tipo_prod);
                    break;
                case "prod_asc":
                    admStockContext = admStockContext.OrderBy(a => a.prod_nom);
                    break;
                case "prod_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.prod_nom);
                    break;
                default:
                    //admStockContext = admStockContext.OrderBy(a => a.art_id);
                    ViewBag.ArtSortParm = "art_desc";
                    ViewBag.ProdSortParm = "prod_desc";
                    break;
            }
            
            return View(await admStockContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Articulos)
                .FirstOrDefaultAsync(m => m.prod_id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["art_id"] = new SelectList(_context.Articulos, "art_id", "tipo_prod");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prod_id,art_id,prod_nom,prod_desc")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Articulos = await _context.Articulos.FindAsync(producto.art_id);

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["art_id"] = new SelectList(_context.Articulos, "art_id", "tipo_prod", producto.art_id);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["art_id"] = new SelectList(_context.Articulos, "art_id", "tipo_prod", producto.art_id);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prod_id,art_id,prod_nom,prod_desc")] Producto producto)
        {
            if (id != producto.prod_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.prod_id))
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
            ViewData["art_id"] = new SelectList(_context.Articulos, "art_id", "tipo_prod", producto.art_id);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Articulos)
                .FirstOrDefaultAsync(m => m.prod_id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'AdmStockContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.prod_id == id)).GetValueOrDefault();
        }
    }
}
