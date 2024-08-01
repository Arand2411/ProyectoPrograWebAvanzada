using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Entities;
using Proyecto_Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Proyecto_Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class CategoriaController(ICategoriaModel iCategoriaModel) : Controller
    {
        [HttpGet]
        public IActionResult RegistrarCategoria()
        {
            return View();
        }



        [HttpPost]
        public IActionResult RegistrarCategoria(Categoria ent)
        {
            
            var resp = iCategoriaModel.RegistrarCategoria(ent);

            if (resp.Codigo == 1)
            {
                ViewBag.Success = true;
                ViewBag.Message = "La categoria fue agregada exitosamente";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Message = resp.Mensaje;
            }

            return View();
        }
        [HttpGet]
        public IActionResult ConsultarCategorias()
        {
            var resp = iCategoriaModel.ConsultarCategorias();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Categoria>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.IdCategoria != HttpContext.Session.GetInt32("IDCATEGORIA")).ToList());
            }

            return View(new List<Categoria>());
        }


    }
}
