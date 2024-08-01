using Proyecto_Web.Entities;

using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Proyecto_Web.Services;
using Proyecto_Web.Models;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using System.Configuration;
namespace Proyecto_Web.Models
{
    public class ProductoModel : IProductoModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductoModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Respuesta> ConsultarProductos()
        {
            string url = _configuration["settings:UrlApi"] + "api/Producto/ConsultarProductos";
            HttpResponseMessage resp = await _httpClient.GetAsync(url);

            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<Respuesta>();
            else
                return new Respuesta();
        }

        public async Task<Respuesta> RegistrarProducto(Producto ent)
        {
            string url = _configuration["settings:UrlApi"] + "api/Producto/RegistrarProducto";
            JsonContent body = JsonContent.Create(ent);
            HttpResponseMessage resp = await _httpClient.PostAsync(url, body);

            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<Respuesta>();
            else
                return new Respuesta();
        }

        public async Task<Respuesta> ActualizarProducto(Producto ent)
        {
            string url = _configuration["settings:UrlApi"] + "api/Producto/ActualizarProducto";
            JsonContent body = JsonContent.Create(ent);
            HttpResponseMessage resp = await _httpClient.PutAsync(url, body);

            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<Respuesta>();
            else
                return new Respuesta();
        }

        public async Task<Respuesta> EliminarProducto(int IdProducto)
        {
            string url = _configuration["settings:UrlApi"] + $"api/Producto/EliminarProducto/{IdProducto}";
            HttpResponseMessage resp = await _httpClient.DeleteAsync(url);

            if (resp.IsSuccessStatusCode)
                return await resp.Content.ReadFromJsonAsync<Respuesta>();
            else
                return new Respuesta();
        }
    }

}
