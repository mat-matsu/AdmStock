using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmStock.Models;
using AdmStock.Context;

namespace AdmStock.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AdmStockContext _context;

        public ClientesController(AdmStockContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string sortOrder, string searchStrNom, int searchIntDni, string searchStrDir, string searchStrTel)
        {
            ViewBag.Message = "Lista de Clientes.";
            ViewBag.NomSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_desc" : "";
            ViewBag.DniSortParm = sortOrder == "dni_asc" ? "dni_desc" : "dni_asc";

            var admStockContext = _context.Clientes.OrderBy(c => c.cliente_nom);

            if (searchIntDni > 0)
            {
                admStockContext = (IOrderedQueryable<Cliente>)admStockContext
                    .Where(a =>
                        a.cliente_dni == searchIntDni
                        &&
                        a.cliente_nom.Contains(String.IsNullOrEmpty(searchStrNom) ? "" : searchStrNom)
                        &&
                        a.cliente_dir.Contains(String.IsNullOrEmpty(searchStrDir) ? "" : searchStrDir)
                        &&
                        a.cliente_tel.Contains(String.IsNullOrEmpty(searchStrTel) ? "" : searchStrTel)
                    );
            }
            else
            {
                admStockContext = (IOrderedQueryable<Cliente>)admStockContext
                    .Where(a =>
                        a.cliente_nom.Contains(String.IsNullOrEmpty(searchStrNom) ? "" : searchStrNom)
                        &&
                        a.cliente_dir.Contains(String.IsNullOrEmpty(searchStrDir) ? "" : searchStrDir)
                        &&
                        a.cliente_tel.Contains(String.IsNullOrEmpty(searchStrTel) ? "" : searchStrTel)
                    );
            }

            switch (sortOrder)
            {
                case "nom_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.cliente_nom);
                    break;
                case "dni_asc":
                    admStockContext = admStockContext.OrderBy(a => a.cliente_dni);
                    break;
                case "dni_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.cliente_dni);
                    break;
                default:
                    ViewBag.NomSortParm = "nom_desc";
                    ViewBag.DniSortParm = "dni_desc";
                    break;
            }

            return View(await admStockContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.cliente_id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cliente_id,cliente_nom,cliente_dni,cliente_dir,cliente_tel")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cliente_id,cliente_nom,cliente_dni,cliente_dir,cliente_tel")] Cliente cliente)
        {
            if (id != cliente.cliente_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.cliente_id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.cliente_id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'AdmStockContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.cliente_id == id)).GetValueOrDefault();
        }
    }
}
