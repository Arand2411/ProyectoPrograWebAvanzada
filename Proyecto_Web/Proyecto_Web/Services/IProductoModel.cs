using Proyecto_Web.Entities;


using Proyecto_Web.Entities;

namespace Proyecto_Web.Services
{
    public interface IProductoModel
    {
        Respuesta RegistrarProducto(Producto ent);

        Respuesta ConsultarProductos();

    }
}