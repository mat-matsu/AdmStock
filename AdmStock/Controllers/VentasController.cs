using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmStock.Context;
using AdmStock.Models;
using ClosedXML.Excel;
using System.Data;

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
        public async Task<IActionResult> Index(string sortOrder, DateTime searchDate, string searchStrNom, string searchStrProd)
        {
            ViewBag.Message = "Lista de Ventas.";
            ViewBag.FecSortParm = String.IsNullOrEmpty(sortOrder) ? "fec_asc" : "";
            ViewBag.CliSortParm = sortOrder == "cli_asc" ? "cli_desc" : "cli_asc";
            ViewBag.ProdSortParm = sortOrder == "prod_asc" ? "prod_desc" : "prod_asc";

            var searchDateFormat = searchDate.ToShortDateString();

            var admStockContext = _context.Ventas.Include(v => v.Clientes).Include(v => v.Lote).Include(v => v.Lote.Productos).OrderByDescending(v => v.venta_fecha);

            if(!searchDateFormat.Equals("1/1/0001"))
            {
                admStockContext = (IOrderedQueryable<Venta>)admStockContext
                    .Where(a =>
                        (a.venta_fecha > searchDate && a.venta_fecha < searchDate.AddDays(1))
                        &&
                        a.Clientes.cliente_nom.Contains(String.IsNullOrEmpty(searchStrNom) ? "" : searchStrNom)
                        &&
                        a.Lote.Productos.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                    );
            } else
            {
                admStockContext = (IOrderedQueryable<Venta>)admStockContext
                    .Where(a =>
                        a.Clientes.cliente_nom.Contains(String.IsNullOrEmpty(searchStrNom) ? "" : searchStrNom)
                        &&
                        a.Lote.Productos.prod_nom.Contains(String.IsNullOrEmpty(searchStrProd) ? "" : searchStrProd)
                    );
            }

            switch (sortOrder)
            {
                case "fec_asc":
                    admStockContext = admStockContext.OrderBy(a => a.venta_fecha);
                    break;
                case "cli_asc":
                    admStockContext = admStockContext.OrderBy(a => a.Clientes.cliente_nom);
                    break;
                case "cli_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.Clientes.cliente_nom);
                    break;
                case "prod_asc":
                    admStockContext = admStockContext.OrderBy(a => a.Lote.Productos.prod_nom);
                    break;
                case "prod_desc":
                    admStockContext = admStockContext.OrderByDescending(a => a.Lote.Productos.prod_nom);
                    break;
                default:
                    ViewBag.FecSortParm = "fec_asc";
                    ViewBag.CliSortParm = "cli_desc";
                    ViewBag.ProdSortParm = "prod_desc";
                    break;
            }

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
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_nom");
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
                venta.Lote = await _context.Lotes.FindAsync(venta.lote_id);
                venta.Clientes = await _context.Clientes.FindAsync(venta.cliente_id);

                if (venta.venta_cant <= venta.Lote.lote_cant)
                {
                    _context.Add(venta);
                    await _context.SaveChangesAsync();

                    venta.Lote.lote_cant = (venta.Lote.lote_cant - venta.venta_cant);
                    _context.Lotes.Update(venta.Lote);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                               
            }
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_nom", venta.cliente_id);
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
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_nom", venta.cliente_id);
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
            ViewData["cliente_id"] = new SelectList(_context.Clientes, "cliente_id", "cliente_nom", venta.cliente_id);
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

        // GET: Ventas/Report
        public async Task<IActionResult> Report(string sortOrder, DateTime startDate, DateTime endDate)
        {
            ViewBag.Message = "Reporte de ventas";
            ViewBag.FecSortParm = String.IsNullOrEmpty(sortOrder) ? "fec_asc" : "";
            
            var startDateFormat = startDate.ToShortDateString();
            var endDateFormat = endDate.ToShortDateString();

            var admStockContext = _context.Ventas.Include(v => v.Clientes).Include(v => v.Lote).Include(v => v.Lote.Productos).OrderByDescending(v => v.venta_fecha);

            if (!startDateFormat.Equals("1/1/0001"))
            {
                admStockContext = (IOrderedQueryable<Venta>)admStockContext
                    .Where(a => (a.venta_fecha > startDate && a.venta_fecha < endDate) );
            }
            
            switch (sortOrder)
            {
                case "fec_asc":
                    admStockContext = admStockContext.OrderBy(a => a.venta_fecha);
                    break;
                default:
                    ViewBag.FecSortParm = "fec_asc";
                    break;
            }

            return View(await admStockContext.ToListAsync());
        }

        // POST: Ventas/Report
        [HttpPost]
        public async Task<FileResult> Report(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Fecha Venta"),
                                                     new DataColumn("Nombre Cliente"),
                                                     new DataColumn("Producto"),
                                                     new DataColumn("Cantidad"),
                                                     new DataColumn("Precio"),
                                                     new DataColumn("Subtotal")});

            var startDateFormat = startDate.ToShortDateString();
            var endDateFormat = endDate.ToShortDateString();

            var admStockContext = _context.Ventas.Include(v => v.Clientes).Include(v => v.Lote).Include(v => v.Lote.Productos).OrderBy(v => v.venta_fecha);

            if (!startDateFormat.Equals("1/1/0001"))
            {
                admStockContext = (IOrderedQueryable<Venta>)admStockContext
                    .Where(a => (a.venta_fecha > startDate && a.venta_fecha < endDate.AddDays(1)));
            }

            foreach (var venta in admStockContext)
            {
                dt.Rows.Add(venta.venta_fecha, venta.Clientes.cliente_nom, venta.Lote.Productos.prod_nom, venta.venta_cant, venta.Lote.lote_precio, (venta.venta_cant * venta.Lote.lote_precio));
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte - " + startDateFormat + " - " + endDateFormat + ".xlsx");
                }
            }
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.venta_id == id)).GetValueOrDefault();
        }
    }
}
