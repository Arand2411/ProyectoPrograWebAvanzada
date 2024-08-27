using Proyecto_Web.Entities;
using Proyecto_Web.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Proyecto_Web.Models
{


    public class CarritoModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : ICarritoModel
    {

        public Respuesta RegistrarCarrito(Carrito ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Carrito/RegistrarCarrito";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                JsonContent body = JsonContent.Create(ent);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }

        public Respuesta ConsultarCarrito()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Carrito/ConsultarCarrito";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }

    }

}
