using Proyecto_Web.Entities;
using static Proyecto_Web.Entities.UsuarioEnt;

namespace Proyecto_Web.Services
{
    public interface IUsuarioModel
    {
        Task<UsuarioRespuesta?> RegistrarUsuarioAsync(UsuarioEnt entidad);
        Task<UsuarioRespuesta?> LoginUsuarioAsync(UsuarioEnt entidad);
        Task<UsuarioRespuesta?> ConsultarUsuariosAsync();
        Task<UsuarioRespuesta?> ConsultarUnUsuarioAsync(int IdUsuario);
        Task<UsuarioRespuesta?> ActualizarUsuarioAsync(UsuarioEnt entidad);
        Task<UsuarioRespuesta?> EliminarUsuarioAsync(int IdUsuario);
    }
}