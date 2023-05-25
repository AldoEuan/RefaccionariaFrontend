using RefaccionariaFrontend.Interfaces;
using RefaccionariaFrontend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using RefaccionariaFrontend.Services;

namespace RefaccionariaFrontend.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IRepository<ProductoModel> repository;
        private string route = "producto";

        public ProductosController()
        {
            repository = new Repository<ProductoModel>();
        }

        public async Task<ActionResult> Index()
        {
            var (statusCode, result) = await repository.GetAllAsync(route);

            if (statusCode == HttpStatusCode.OK)
            {
                var data = result.Data;
                return View(data);
            }
            return View();

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductoModel producto)
        {
            if (!ModelState.IsValid) ModelState.AddModelError(string.Empty, "No se pudo crear el Producto");
            
            var (statusCode,result) = await repository.CreateAsync(route, producto);
            if(statusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "El producto no pudo ser creado");
            return View(producto);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id) {
            var (statusCode, result) = await repository.GetByIdAsync(route, id);

            if(statusCode == HttpStatusCode.OK)
            {
                var data = result.Data;
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductoModel producto)
        {
            if (!ModelState.IsValid) ModelState.AddModelError(string.Empty, "El producto nose puede editar");
            
            var (statusCode, result) = await repository.UpdateAsync(route, producto.Id ,producto);
            if (statusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");   
            }

            ModelState.AddModelError(string.Empty, "El producto nose puede editar");
            return View(producto);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var statusCode = await repository.DeleteAsync(route,id);
            if(statusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }

            ModelState.AddModelError(string.Empty, "Ocurrio un error en el servidor. Por favor checar con el administrador");
            return RedirectToAction("index");
        }
    }
}