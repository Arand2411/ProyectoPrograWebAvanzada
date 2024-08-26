using Proyecto_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static Proyecto_API.Entities.Producto;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(IConfiguration iConfiguration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarProducto")]
        public async Task<IActionResult> RegistrarProducto(Producto ent)
        {
            ProductoRespuesta resp = new ProductoRespuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarProducto", new { ent.Descripcion, ent.Precio, ent.Cantidad, ent.Ruta_Imagen, ent.Estado}, commandType: CommandType.StoredProcedure);

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
        [Route("ConsultarProductos")]
        public async Task<IActionResult> ConsultarProductos()
        {
            ProductoRespuesta resp = new ProductoRespuesta();
            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Producto>("ConsultarProductos", new { }, commandType: CommandType.StoredProcedure);

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
        [Route("ActualizarProducto")]
        [HttpPut]
        public IActionResult ActualizarProducto(Producto producto)
        {
            var productoRespuesta = new ProductoRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarProducto",
                        new
                        {
                            producto.IdProducto,
                            producto.Descripcion,
                            producto.Precio,
                            producto.Cantidad,
                            producto.Ruta_Imagen,
                            producto.Estado
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        productoRespuesta.Codigo = -1;
                        productoRespuesta.Mensaje = "No se ha podido actualizar en la base de datos, intenta de nuevo";
                        return BadRequest(productoRespuesta);
                    }
                    else
                    {
                        productoRespuesta.Codigo = 1;
                        productoRespuesta.Mensaje = "Producto actualizado con éxito.";
                        return Ok(productoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el producto en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el producto.", error = ex.Message });
            }
        }


        [AllowAnonymous]
        [Route("EliminarProducto")]
        [HttpDelete]
        public IActionResult EliminarProducto(int IdProducto)
        {
            var productoRespuesta = new ProductoRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarProducto",
                        new
                        {
                            IdProducto
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        productoRespuesta.Codigo = -1;
                        productoRespuesta.Mensaje = "No se ha podido eliminar el proveedor en la base de datos, intenta de nuevo";
                        return BadRequest(productoRespuesta);
                    }
                    else
                    {
                        productoRespuesta.Codigo = 1;
                        productoRespuesta.Mensaje = "Proveedor eliminado con éxito.";
                        return Ok(productoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al eliminar el proveedor en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el proveedor.", error = ex.Message });
            }
        }

    }
}
