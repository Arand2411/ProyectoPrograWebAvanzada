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


    public class ProductoModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : IProductoModel
    {
        public Respuesta RegistrarProducto(Producto ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/RegistrarProducto";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }


        public Respuesta ConsultarProducto()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/ConsultarProducto";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }


        public Respuesta ConsultarUnProducto(int IdProducto)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/ConsultarUnProducto?IdProducto=" + IdProducto;
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }




        public Respuesta ActualizarProducto(Producto ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/ActualizarProducto";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                JsonContent body = JsonContent.Create(ent);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                else
                    return new Respuesta();
            }
        }

        public Respuesta EliminarProducto(int IdProducto)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/EliminarProducto?IdProducto=" + IdProducto;
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }



        }

    }
}

