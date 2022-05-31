using Microsoft.AspNetCore.Mvc;
using AdmStock.Models;
using System.Text.Json;


namespace AdmStock.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Index()
        {
            AdmStockContext context = new();

            Proveedor prov = context.Proveedores.Find(1);

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
