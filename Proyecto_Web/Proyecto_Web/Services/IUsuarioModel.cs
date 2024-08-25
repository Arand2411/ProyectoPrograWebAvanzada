using Proyecto_Web.Entities;
using static Proyecto_Web.Entities.Usuario;

namespace Proyecto_Web.Services
{
    public interface IUsuarioModel
    {
        Respuesta RegistrarUsuario(Usuario ent);

        Respuesta IniciarSesion(Usuario ent);

        Respuesta ConsultarUsuarios();

        Respuesta ConsultarUsuario(int Consecutivo);

        Respuesta ActualizarUsuario(Usuario ent);

        Respuesta RecuperarAcceso(string Identificacion);

        Respuesta CambiarEstadoUsuario(Usuario ent);
    }
}