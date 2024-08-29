using Dapper;
using Proyecto_API.Entities;
using Proyecto_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController(IConfiguration iConfiguration) : ControllerBase
    {

        [Authorize]
        [HttpPost]
        [Route("RegistrarCarrito")]
        public async Task<IActionResult> RegistrarCarrito(Carrito ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarCarrito",
                    new { ent.ConsecutivoUsuario, ent.IdProducto, ent.Cantidad }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "El producto no se pudo agregar a su carrito";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("ConsultarCarrito")]
        public async Task<IActionResult> ConsultarCarrito()
        {
            Respuesta resp = new Respuesta();
            int ConsecutivoUsuario = int.Parse(User.Identity!.Name!);

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Carrito>("ConsultarCarrito", new { ConsecutivoUsuario }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay productos registrados en su carrito";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }


    }
}
