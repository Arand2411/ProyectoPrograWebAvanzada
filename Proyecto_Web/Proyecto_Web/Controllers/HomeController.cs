using Proyecto_Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Web.Models;
using Proyecto_Web.Services;
using System.Text.Json;

namespace Proyecto_Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel iUsuarioModel, IComunModel iComunModel, IRolModel iRolModel) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario ent)
        {
            ent.Contrasenna = iComunModel.Encrypt(ent.Contrasenna!);
            var resp = iUsuarioModel.IniciarSesion(ent);

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<Usuario>((JsonElement)resp.Contenido!);

                if (datos!.EsTemporal)
                {
                    if (datos!.VigenciaTemporal <= DateTime.Now)
                    {
                        ViewBag.msj = "Su contraseņa temporal ha caducado.";
                        return View();
                    }
                }

                HttpContext.Session.SetString("TOKEN", datos!.Token!);
                HttpContext.Session.SetString("NOMBRE", datos!.Nombre!);
                HttpContext.Session.SetString("ROL", datos!.IdRol.ToString());
                HttpContext.Session.SetInt32("CONSECUTIVO", datos!.Consecutivo);
                return RedirectToAction("ConsultarProducto", "Producto");
            }

            ViewBag.msj = resp.Mensaje;
            return View();
        }



        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario ent)
        {
            ent.Contrasenna = iComunModel.Encrypt(ent.Contrasenna!);
            var resp = iUsuarioModel.RegistrarUsuario(ent);

            if (resp.Codigo == 1)
                return RedirectToAction("Index", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }



        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario ent)
        {
            var resp = iUsuarioModel.RecuperarAcceso(ent.Identificacion!);

            if (resp.Codigo == 1)
                return RedirectToAction("Index", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }




        [FiltroSesiones]
        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



        [FiltroSesiones]
        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            var resp = iUsuarioModel.ConsultarUsuarios();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Usuario>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.Consecutivo != HttpContext.Session.GetInt32("CONSECUTIVO")).ToList());
            }

            return View(new List<Usuario>());
        }


        [FiltroSesiones]
        [HttpGet]
        public IActionResult ActualizarUsuario(int q)
        {
            var roles = iRolModel.ConsultarRoles();

            if (roles.Codigo == 1)
            {
                ViewBag.Roles = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)roles.Contenido!);
            }
            else
            {
                ViewBag.Roles = new List<SelectListItem>();
            }

            var resp = iUsuarioModel.ConsultarUsuario(q);

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<Usuario>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new Usuario());
        }


        [FiltroSesiones]
        [HttpPost]
        public IActionResult ActualizarUsuario(Usuario ent)
        {
            var resp = iUsuarioModel.ActualizarUsuario(ent);

            if (resp.Codigo == 1)
                return RedirectToAction("ConsultarUsuarios", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }



        [FiltroSesiones]
        [HttpPost]
        public IActionResult CambiarEstadoUsuario(Usuario ent)
        {
            var resp = iUsuarioModel.CambiarEstadoUsuario(ent);

            if (resp.Codigo == 1)
            {
                return RedirectToAction("ConsultarUsuarios", "Home");
            }

            TempData["ErrorMessage"] = resp.Mensaje;
            return RedirectToAction("ConsultarUsuarios", "Home");
        }


    }
}
