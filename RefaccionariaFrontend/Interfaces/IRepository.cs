using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RefaccionariaFrontend.Models.Response;

namespace RefaccionariaFrontend.Interfaces
{
    public interface IRepository<T>
    {
        Task<(HttpStatusCode statusCode, Response<IEnumerable<T>> result)> GetAllAsync(string route);
        Task<(HttpStatusCode statusCode, Response<T> result)> GetByIdAsync(string route, object id);
        Task<(HttpStatusCode statusCode, Response<T> result)> CreateAsync(string route, T model);
        Task<(HttpStatusCode statusCode, Response<T> result)> UpdateAsync(string route, object id, T model);
        Task<HttpStatusCode> DeleteAsync(string route, object id);
    }
}