using Proyecto_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static Proyecto_API.Entities.Producto;
using Dapper;
using Microsoft.Extensions.Configuration;
using Proyecto_API.Models;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(IConfiguration iConfiguration, IComunesModel iComunesModel) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarProducto")]
        public async Task<IActionResult> RegistrarProducto(Producto ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarProducto", new { ent.DescripcionProducto, ent.PrecioUnitario, ent.Cantidad, ent.Imagen, ent.Estado, ent.IdCategoria }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Producto Guardado Con exito";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se pudo Guardar el Producto";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("ConsultarProducto")]
        public async Task<IActionResult> ConsultarProducto()
        {
            Respuesta resp = new Respuesta();
            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Producto>("ConsultarProducto", new { }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Exito!!!";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay Productos";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }


        [AllowAnonymous]
        [Route("ConsultarUnProducto")]
        [HttpGet]
        public async Task <IActionResult> ConsultarUnProducto(int idProducto)
        {
            var resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryFirstOrDefaultAsync<Producto>("ObtenerProductoPorID", new { idProducto }, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay usuarios registrados en este momento";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }

        }



        [Authorize]
        [HttpPut]
        [Route("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(Producto ent)
        {
            if (!iComunesModel.EsAdministrador(User))
               return StatusCode(403);

            Respuesta resp = new Respuesta();
            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("ActualizarProducto", new { ent.IdProducto, ent.DescripcionProducto, ent.PrecioUnitario, ent.Cantidad, ent.Imagen, ent.Estado, ent.IdCategoria }, commandType: CommandType.StoredProcedure);

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
                    resp.Mensaje = "El producto no se pudo actualizar";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }


        //[AllowAnonymous]
        [Route("EliminarProducto")]
        [HttpDelete]
        public IActionResult EliminarProducto(int idProducto)
        {
            var resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))

            {
                var result = context.Execute("EliminarProducto", new { IdProducto = idProducto }, commandType: CommandType.StoredProcedure);

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
                    resp.Mensaje = "El producto no se pudo eliminar";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }


    }
}



