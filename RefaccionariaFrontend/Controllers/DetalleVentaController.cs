using RefaccionariaFrontend.Interfaces;
using RefaccionariaFrontend.Models;
using RefaccionariaFrontend.Models.Response;
using RefaccionariaFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RefaccionariaFrontend.Controllers
{
    public class DetalleVentaController : Controller
    {
        // GET: DetalleVenta
        private List<DetalleVentaModel> detalleVentaModels = new List<DetalleVentaModel>();
        private readonly IRepository<SalesModel> repositorydesales;
        private readonly IRepository<DetalleVentaModel> reposideventa;
        private string route = "venta";
         
        public DetalleVentaController() {
            this.repositorydesales = new Repository<SalesModel>();
            this.reposideventa = new Repository<DetalleVentaModel>();
        }
        
        public async Task<ActionResult> ObtenerVenta()
        {
            var (statusCode, result) = await repositorydesales.GetAllAsync(route);

            if (statusCode == HttpStatusCode.OK)
            {
                var data = result.Data;
                return View(data);
            }
            return View();
           
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7079/api/detalleventa/");
                var response = await client.GetAsync($"getWithSale/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Response<IEnumerable<DetalleVentaModel>>>();
                    return View(result.Data);
                }
                return View();
            }
        }
    }
}