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
    public class ProductoController(IProductoModel iProductoModel, ICarritoModel iCarritoModel) : Controller
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

            return RedirectToAction("Producto", "ConsultarProducto");
        }

        [HttpGet]
        public IActionResult ConsultarProducto()
        {

            var carritoActual = iCarritoModel.ConsultarCarrito();
            if(carritoActual.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Carrito>>((JsonElement)carritoActual.Contenido!);
				HttpContext.Session.SetString("SubTotal", datos!.Sum(x => x.SubTotal).ToString());
				HttpContext.Session.SetString("Cantidad", datos!.Sum(x => x.Cantidad).ToString());
				HttpContext.Session.SetString("Total", datos!.Sum(x => x.Total).ToString());
			}
			else
			{
				HttpContext.Session.SetString("SubTotal", "0");
				HttpContext.Session.SetString("Cantidad", "0");
				HttpContext.Session.SetString("Total", "0");
			}

			var resp = iProductoModel.ConsultarProducto();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
            }

            return View(new List<Producto>());
        }

		[HttpGet]
		public IActionResult ConsultarComponentes()
		{
			var resp = iProductoModel.ConsultarComponentes();

			if (resp.Codigo == 1)
			{
				var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
				return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
			}

			return View(new List<Producto>());
		}

		[HttpGet]
		public IActionResult ConsultarPerifericos()
		{
			var resp = iProductoModel.ConsultarPerifericos();

			if (resp.Codigo == 1)
			{
				var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
				return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
			}

			return View(new List<Producto>());
		}

		[HttpGet]
		public IActionResult ConsultarAccesorios()
		{
			var resp = iProductoModel.ConsultarAccesorios();

			if (resp.Codigo == 1)
			{
				var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
				return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
			}

			return View(new List<Producto>());
		}

		[HttpGet]
		public IActionResult ConsultarComputadoras()
		{
			var resp = iProductoModel.ConsultarComputadoras();

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

