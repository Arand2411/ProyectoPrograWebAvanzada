using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Proyecto_Web.Entities;
using Proyecto_Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    public class HomeController (ICategoriaModel iCategoriaModel) : Controller
    {

        [HttpGet]
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
