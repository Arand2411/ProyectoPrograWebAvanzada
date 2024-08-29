using Proyecto_Web.Entities;

namespace Proyecto_Web.Services
{
    public interface ICarritoModel
    {
        Respuesta RegistrarCarrito(Carrito ent);

        Respuesta ConsultarCarrito();
    }
}
