using Proyecto_Web.Entities;
using System.Threading.Tasks;

namespace Proyecto_Web.Services
{
    public interface IProductoModel
    {
        Respuesta RegistrarProducto(Producto ent);
        Respuesta ConsultarProducto();
        Respuesta ConsultarUnProducto(int IdProducto);
        Respuesta ActualizarProducto(Producto ent);
        Respuesta EliminarProducto(int IdProducto);

		Respuesta ConsultarComponentes();

		Respuesta ConsultarPerifericos();

		Respuesta ConsultarAccesorios();

		Respuesta ConsultarComputadoras();
	}
}
