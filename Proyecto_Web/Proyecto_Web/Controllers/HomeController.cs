using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Entities;
using Proyecto_Web.Models;
using Proyecto_Web.Services;
using System.Diagnostics;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    public class HomeController(IProductoModel iProductoModel) : Controller
    {


        public IActionResult Index()
        {
            var resp = iProductoModel.ConsultarProductos();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
            }

            return View(new List<Producto>());
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
