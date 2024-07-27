using Proyecto_Web.Entities;
using Proyecto_Web.Services;
using System.Net.Http.Headers;
using System.Text.Json;
using static Proyecto_Web.Entities.UsuarioEnt;

namespace Proyecto_Web.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _iConfiguration;
        private readonly IHttpContextAccessor _iContextAccesor;

        public UsuarioModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor)
        {
            _httpClient = httpClient;
            _iConfiguration = iConfiguration;
            _iContextAccesor = iContextAccesor;
        }

        public async Task<UsuarioRespuesta?> RegistrarUsuarioAsync(UsuarioEnt entidad)
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = await _httpClient.PostAsync(url, body);
            if (respuestaApi.IsSuccessStatusCode)
                return await respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }

        public async Task<UsuarioRespuesta?> LoginUsuarioAsync(UsuarioEnt entidad)
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/LoginUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = await _httpClient.PostAsync(url, body);
            if (respuestaApi.IsSuccessStatusCode)
                return await respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }

        public async Task<UsuarioRespuesta?> ConsultarUsuariosAsync()
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUsuarios";
            string token = _iContextAccesor.HttpContext!.Session.GetString("TOKEN")!;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuestaApi = await _httpClient.GetAsync(url);

            if (respuestaApi.IsSuccessStatusCode)
                return await respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }

        public async Task<UsuarioRespuesta?> ConsultarUnUsuarioAsync(int IdUsuario)
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUnUsuario?IdUsuario=" + IdUsuario;
            var resp = await _httpClient.GetAsync(url);
            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }

        public async Task<UsuarioRespuesta?> ActualizarUsuarioAsync(UsuarioEnt entidad)
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ActualizarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = await _httpClient.PutAsync(url, body);
            if (respuestaApi.IsSuccessStatusCode)
                return await respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }

        public async Task<UsuarioRespuesta?> EliminarUsuarioAsync(int IdUsuario)
        {
            string url = _iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/EliminarUsuario?IdUsuario=" + IdUsuario;
            var resp = await _httpClient.DeleteAsync(url);
            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<UsuarioRespuesta>();
            return null;
        }
    }
}