using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Services;
using Proyecto_Web.Entities;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Proyecto_Web.Models;


namespace Proyecto_Web.Controllers
{

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductoController(IProductoModel iProductoModel) : Controller
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

            return RedirectToAction("ConsultarProducto", "Producto");
        }

        [HttpGet]
        public IActionResult ConsultarProducto()
        {
            var resp = iProductoModel.ConsultarProducto();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
            }

            return View(new List<Producto>());
        }

        [HttpGet]
        public IActionResult ActualizarProducto(int IdProducto)
        {
            var resp = iProductoModel.ConsultarUnProducto(IdProducto);

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<Producto>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new Producto());
        }



        [HttpPost]
        public IActionResult ActualizarProducto(Producto ent)
        {
            var respuestaModelo = iProductoModel.ActualizarProducto(ent);

            if (respuestaModelo.Codigo == 1)
            {
                ViewBag.Success = true;
                ViewBag.Message = "Producto actualizado exitosamente.";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Message = respuestaModelo.Mensaje;
            }

            return RedirectToAction("ConsultarProducto", "Producto");
        }

        [HttpPost]
        public IActionResult EliminarProducto(int IdProducto)
        {
            var respuestaModelo = iProductoModel.EliminarProducto(IdProducto);
            if (respuestaModelo.Codigo == 1)
            {
                return RedirectToAction("ConsultarProducto", "Producto"); ;
            }
            else
            {
                ViewBag.MsjPantalla = respuestaModelo.Mensaje;
                return RedirectToAction("ConsultarProducto", "Producto");
            }
        }
    }
}

