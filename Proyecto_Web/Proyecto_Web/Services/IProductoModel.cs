using Proyecto_Web.Entities;
using System.Threading.Tasks;

namespace Proyecto_Web.Services
{
    public interface IProductoModel
    {
        Respuesta RegistrarProducto(Producto ent);
        Respuesta ConsultarProductos();
        Respuesta ActualizarProducto(Producto ent);
        Respuesta EliminarProducto(int IdProducto);
    }
}
