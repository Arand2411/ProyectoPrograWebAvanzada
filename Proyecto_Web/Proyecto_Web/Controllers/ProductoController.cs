﻿using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Services;
using Proyecto_Web.Entities;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Proyecto_Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoModel _iProductoModel;

        public ProductoController(IProductoModel iProductoModel)
        {
            _iProductoModel = iProductoModel;
        }

        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarProducto(Producto ent)
        {
            var resp = await _iProductoModel.RegistrarProducto(ent);

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
        public async Task<IActionResult> ConsultarProductos()
        {
            var resp = await _iProductoModel.ConsultarProductos();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Producto>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdProducto != HttpContext.Session.GetInt32("IDPRODUCTO")).ToList());
            }

            return View(new List<Producto>());
        }

        [HttpGet]
        public IActionResult ActualizarProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarProducto(Producto ent)
        {
            var respuestaModelo = await _iProductoModel.ActualizarProducto(ent);

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

            return View(ent);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            var respuestaModelo = await _iProductoModel.EliminarProducto(idProducto);
            if (respuestaModelo.Codigo == 1)
            {
                return RedirectToAction("ConsultarProductos");
            }
            else
            {
                ViewBag.MsjPantalla = respuestaModelo.Mensaje;
                return View();
            }
        }
    }
}
