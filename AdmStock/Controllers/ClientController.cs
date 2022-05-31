using Microsoft.AspNetCore.Mvc;
using AdmStock.Models;

namespace AdmStock.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
