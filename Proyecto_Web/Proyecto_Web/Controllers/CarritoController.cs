using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Entities;
using Proyecto_Web.Services;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    public class CarritoController(ICarritoModel iCarritoModel) : Controller
    {
        [HttpPost]
        public IActionResult RegistrarCarrito(int IdProducto, int Cantidad)
        {
            var ent = new Carrito();
            ent.ConsecutivoUsuario = HttpContext.Session.GetInt32("CONSECUTIVO")!.Value;
            ent.IdProducto = IdProducto;
            ent.Cantidad = Cantidad;

            iCarritoModel.RegistrarCarrito(ent);
            return Json("OK");
        }

        [HttpGet]
        public IActionResult ConsultarCarrito()
        {
            var respuesta = iCarritoModel.ConsultarCarrito();

            if (respuesta.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Carrito>>((JsonElement)respuesta.Contenido!);
                return View(datos);
            }

            return View(new List<Carrito>());
        }
    }

}
