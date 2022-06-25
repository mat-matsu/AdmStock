using Microsoft.AspNetCore.Mvc;
using AdmStock.Models;
using System.Text.Json;
using AdmStock.Context;


namespace AdmStock.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly AdmStockContext _context;

        public ProveedorController(AdmStockContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Proveedor prov = _context.Proveedores.Find(1);

            TempData["proveedor"] = JsonSerializer.Serialize(prov);

            return RedirectToAction("showProv");
        }

        public IActionResult showProv()
        {
            Proveedor prov = JsonSerializer.Deserialize<Proveedor>((string)TempData["proveedor"]);

            return View("Index", prov);
        }
    }
}
