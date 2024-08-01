using Proyecto_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static Proyecto_API.Entities.Categoria;
using Dapper;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController(IConfiguration iConfiguration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarCategoria")]
        public async Task<IActionResult> RegistrarCategoria(Categoria ent)
        {
            CategoriaRespuesta resp = new CategoriaRespuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarCategoria", new { ent.Nombre, ent.Descripcion, ent.RutaImagen }, commandType: CommandType.StoredProcedure);

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
                    resp.Mensaje = "La categoría ya se encuentra registrada";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("ConsultarCategorias")]
        public async Task<IActionResult> ConsultarCategorias()
        {

            CategoriaRespuesta resp = new CategoriaRespuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Categoria>("ConsultarCategorias", new { }, commandType: CommandType.StoredProcedure);

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
                    resp.Mensaje = "No hay categorías registradas en este momento";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }
    }
}
