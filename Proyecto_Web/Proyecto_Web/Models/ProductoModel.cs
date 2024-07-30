using Proyecto_Web.Entities;

using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Proyecto_Web.Entities;
using Proyecto_Web.Services;
using Proyecto_Web.Models;
using System.Net.Http.Headers;
namespace Proyecto_Web.Models
{
    public class ProductoModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : IProductoModel
    {
        Respuesta IProductoModel.ConsultarProductos()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "https://localhost:7022/api/Producto/ConsultarProductos";
                //string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }

        Respuesta IProductoModel.RegistrarProducto(Producto ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "https://localhost:7022/api/Producto/RegistrarProducto";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }
    }
}
