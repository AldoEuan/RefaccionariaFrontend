
using RefaccionariaFrontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using RefaccionariaFrontend.Models.Response;

namespace RefaccionariaFrontend.Services
{
    public class Repository<T> : IRepository<T>
    {
        public readonly string apiBase = "https://localhost:7079/api";

        public async Task<(HttpStatusCode statusCode, Response<IEnumerable<T>> result)> GetAllAsync(string route)
        {
            Response<IEnumerable<T>> result = new Response<IEnumerable<T>>();
            HttpStatusCode statusCode;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{apiBase}/{route}/");
                var response = await client.GetAsync("getAll");
                statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Response<IEnumerable<T>>>();
                }
                else
                {
                    result = (Response<IEnumerable<T>>)Enumerable.Empty<T>();
                }
            }
            return (statusCode, result);
        }


        public async Task<(HttpStatusCode statusCode, Response<T> result)> GetByIdAsync(string route, object id)
        {
            Response<T> result = new Response<T>();
            HttpStatusCode statusCode;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{apiBase}/{route}/");
                var response = await client.GetAsync($"getItem/{id}");

                statusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Response<T>>();
                }
            }
            return (statusCode, result);
        }

        public async Task<(HttpStatusCode statusCode, Response<T> result)> CreateAsync(string route, T model)
        {
            HttpStatusCode statusCode;
            Response<T> result = new Response<T>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{apiBase}/{route}/");
                var response = await client.PostAsJsonAsync<T>("create", model);

                statusCode = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Response<T>>();
                }
                else
                {
                    result = default(Response<T>);
                }
                return (statusCode, result);
            }
        }

        public async Task<(HttpStatusCode statusCode, Response<T> result)> UpdateAsync(string route, object id, T model)
        {
            HttpStatusCode statusCode;
            Response<T> result = new Response<T>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{apiBase}/{route}/");
                var response = await client.PutAsJsonAsync<T>($"update?id={id}", model);

                statusCode = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Response<T>>();
                }
                else
                {
                    result = default(Response<T>);
                }
                return (statusCode, result);
            }
        }

        public async Task<HttpStatusCode> DeleteAsync(string route, object id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{apiBase}/{route}/");
                var response = await client.DeleteAsync($"delete/{id}");
                return response.StatusCode;
            }
        }

        
    }
}