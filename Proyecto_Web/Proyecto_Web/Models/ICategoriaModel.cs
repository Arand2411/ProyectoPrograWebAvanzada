using Proyecto_Web.Entities;

namespace Proyecto_Web.Models
{
    public interface ICategoriaModel
    {
        Respuesta RegistrarCategoria(Categoria ent);

        Respuesta ConsultarCategorias();
    }
}
