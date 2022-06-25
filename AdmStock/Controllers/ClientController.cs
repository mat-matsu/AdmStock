using Microsoft.AspNetCore.Mvc;
using AdmStock.Models;
using System.Text.Json;

namespace AdmStock.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {

            // TESTOSTO
            //Cliente cli = context.Clientes.Find(1);
            /*
             * AdmStockContext context = new ();
             * List<Cliente> clientes = context.Clientes.ToList();
             * TempData["cliente"] = JsonSerializer.Serialize(clientes);
             * return RedirectToAction("Lista");
            */

            return View("Index");
        }
        /*
        public IActionResult Listar()
        {
            AdmStockContext context = new();

            //List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>((string)TempData["cliente"]);

            List<Cliente> clientes = context.Clientes.ToList();

            return View("Listar", clientes);
        }

        public IActionResult BuscarDNI()
        {
            return View("BuscarDNI");
        }

        public IActionResult BuscaDNI()
        {
            AdmStockContext context = new();

            //List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>((string)TempData["cliente"]);

            List<Cliente> clientes = context.Clientes.ToList();

            return View("ClienteDNI", clientes);
        }
        */
    }
}
