using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Entities;
using Proyecto_Web.Models;
using Proyecto_Web.Services;
using System.Diagnostics;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    public class HomeController() : Controller
    {


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
