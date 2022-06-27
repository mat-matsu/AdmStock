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
    public class ProveedoresController : Controller
    {
        private readonly AdmStockContext _context;

        public ProveedoresController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Proveedors
        public async Task<IActionResult> Index(string sortOrder, string searchStrCuil, string searchStrNom, string searchStrDir, string searchStrTel)
        {
            ViewBag.Message = "Lista de Proveedores.";
            ViewBag.NomSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_desc" : "";
            ViewBag.CuilSortParm = sortOrder == "cuil_asc" ? "cuil_desc" : "cuil_asc";

            var admStockContext = _context.Proveedores.OrderBy(p => p.prov_nom);

            admStockContext = (IOrderedQueryable<Proveedor>)admStockContext
                .Where(a =>
                    a.prov_cuil.Contains(String.IsNullOrEmpty(searchStrCuil) ? "" : searchStrCuil)
                    &&
                    a.prov_nom.Contains(String.IsNullOrEmpty(searchStrNom) ? "" : searchStrNom)
                    &&
                    a.prov_dir.Contains(String.IsNullOrEmpty(searchStrDir) ? "" : searchStrDir)
                    &&
                    a.prov_tel.Contains(String.IsNullOrEmpty(searchStrTel) ? "" : searchStrTel)
                );

            switch (sortOrder)
            {
                case "nom_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.prov_nom);
                    break;
                case "cuil_asc":
                    admStockContext = admStockContext.OrderBy(a => a.prov_cuil);
                    break;
                case "cuil_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.prov_cuil);
                    break;
                default:
                    ViewBag.NomSortParm = "nom_desc";
                    ViewBag.CuilSortParm = "cuil_desc";
                    break;
            }

            return View(await admStockContext.ToListAsync());
        }

        // GET: Proveedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.prov_id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prov_id,prov_cuil,prov_nom,prov_dir,prov_tel")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prov_id,prov_cuil,prov_nom,prov_dir,prov_tel")] Proveedor proveedor)
        {
            if (id != proveedor.prov_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.prov_id))
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
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.prov_id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'AdmStockContext.Proveedores'  is null.");
            }
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
          return (_context.Proveedores?.Any(e => e.prov_id == id)).GetValueOrDefault();
        }
    }
}
