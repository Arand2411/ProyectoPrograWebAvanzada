using Proyecto_Web.Entities;
using System.Threading.Tasks;

namespace Proyecto_Web.Services
{
    public interface IProductoModel
    {
        Task<Respuesta> RegistrarProducto(Producto ent);
        Task<Respuesta> ConsultarProductos();
        Task<Respuesta> ActualizarProducto(Producto ent);
        Task<Respuesta> EliminarProducto(int IdProducto);
    }
}
