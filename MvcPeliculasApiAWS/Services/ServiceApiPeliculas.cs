using Microsoft.AspNetCore.Server.HttpSys;
using MvcPeliculasApiAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcPeliculasApiAWS.Services
{
    public class ServiceApiPeliculas
    {
        private MediaTypeWithQualityHeaderValue header;
        private string UrlApi;

        public ServiceApiPeliculas(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPeliculasAWS");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/peliculas";
            List<Pelicula> pelis = await this.CallApiAsync<List<Pelicula>>(request);
            return pelis;
        }

        public async Task<List<Pelicula>> GetPeliculasByActorAsync(string actor)
        {
            string request = "api/peliculas/peliculasactores/" + actor;
            List<Pelicula> pelis = await this.CallApiAsync<List<Pelicula>>(request);
            return pelis;
        }

        public async Task<Pelicula> FindPeliculaAsync(int id)
        {
            string request = "api/peliculas/" + id;
            Pelicula peli = await this.CallApiAsync<Pelicula>(request);
            return peli;
        }

        public async Task CreatePeliculaAsync(Pelicula peli)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(peli);
                StringContent content = new StringContent(json, this.header);
                HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
            }
        }

        public async Task UpdatePeliculaAsync(Pelicula peli)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(peli);
                StringContent content = new StringContent(json, this.header);
                HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);
            }
        }

        public async Task DeletePeliculaAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas/" + id;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.DeleteAsync(this.UrlApi + request);
            }
        }
    }
}
