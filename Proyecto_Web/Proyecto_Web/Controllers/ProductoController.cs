using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Services;
using Proyecto_Web.Models;
using Proyecto_Web.Entities;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    public class ProductoController(IProductoModel iProductoModel, IComunModel iComunModel) : Controller
    {

        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto ent)
        {
            var resp = iProductoModel.RegistrarProducto(ent);

            if (resp.Codigo == 1)
            {
                ViewBag.Success = true;
                ViewBag.Message = "Producto registrado exitosamente.";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Message = resp.Mensaje;
            }

            return View();
        }
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var resp = iProductoModel.ConsultarProductos();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
            }

            return View(new List<Producto>());
        }

    }
}