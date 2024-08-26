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

        public Respuesta ConsultarProductos()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/ConsultarProductos";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }



        public  Respuesta RegistrarProducto(Producto ent)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/RegistrarProducto";
            JsonContent body = JsonContent.Create(ent);
            var resp = httpClient.PostAsync(url, body).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;
            return null;
        }


      

        public  Respuesta ActualizarProducto(Producto ent)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Producto/ActualizarProducto";
            JsonContent body = JsonContent.Create(ent);
            var resp = httpClient.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }



        public  Respuesta EliminarProducto(int IdProducto)
        {
            string url = iConfiguration["Llaves:UrlApi"] + $"api/Producto/EliminarProducto?IdProducto=" + IdProducto;
            var resp = httpClient.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }


       
    }

}

