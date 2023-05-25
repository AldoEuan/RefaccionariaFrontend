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

namespace RefaccionariaFrontend.Controllers
{
    public class VentaController : Controller
    {
        private List<ProductoModel> _cart = new List<ProductoModel>();
        private readonly IRepository<ProductoModel> repositoryProduct;
        private string route = "venta";

        public VentaController()
        {
            this.repositoryProduct = new Repository<ProductoModel>();
        }

        public ActionResult Principal()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<ProductoModel> cart = (List<ProductoModel>)Session["cart"];

            if (cart != null && cart.Any())
            {
                // Si hay productos en el carrito, los pasa a la vista
                ViewBag.TotalAmount = cart.Sum(p => p.Preciocosto * 1);
                return View(cart);
            }
            else
            {
                // Si no hay productos en el carrito, muestra un mensaje
                return View();
            }
        }

        public async Task<ActionResult> PartialViewSearch()
        {
            var (statusProductos, resultProducto) = await repositoryProduct.GetAllAsync("producto");

            if (statusProductos == HttpStatusCode.OK)
            {
                return PartialView(resultProducto.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddToCart(int id_producto)
        {
            // Obtiene el producto a agregar al carrito a través de la API
            var (statusCode, result) = await repositoryProduct.GetByIdAsync("producto", id_producto);

            if (statusCode == HttpStatusCode.OK)
            {
                // Si se obtuvo el producto correctamente, lo agrega al carrito
                List<ProductoModel> cart = (List<ProductoModel>)Session["cart"];
                if (cart == null)
                {
                    cart = new List<ProductoModel>();
                }
                cart.Add(result.Data);
                Session["cart"] = cart;
                return RedirectToAction("Index");
            }
            else
            {
                // Si hubo un error al obtener el producto, muestra un mensaje de error
                ViewBag.ErrorMessage = "Error al obtener el producto: " + statusCode;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id_producto)
        {
            // Recupera los productos del carrito de compras de la sesión
            List<ProductoModel> cart = (List<ProductoModel>)Session["cart"];
            if (cart != null && cart.Any())
            {
                // Si hay productos en el carrito, elimina el producto especificado
                cart.RemoveAll(p => p.Id == id_producto);
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult CartSummary()
        {
            // Calcula el resumen del carrito (cantidad de productos y monto total)
            int totalQuantity = 0;
            double totalAmount = 0;
            foreach (var product in _cart)
            {
                totalQuantity += product.Existencia;
                totalAmount += product.Preciocosto * 1;
            }

            ViewBag.TotalQuantity = totalQuantity;
            ViewBag.TotalAmount = totalAmount;

            return View();
        }

        public async Task<ActionResult> Checkout()
        {
            _cart = (List<ProductoModel>)Session["cart"]; 
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7079/api/venta/");
                var response = await client.PostAsJsonAsync("checkout", _cart);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Response<SalesModel>>();
                    _cart.Clear();
                    Session["cart"] = null;
                    return RedirectToAction("Confirmation", new { id_venta = result.Data.Id });
                }
                return RedirectToAction("Principal");
            }
        }

        public async Task<ActionResult> Confirmation(int id_venta)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7079/api/detalleventa/");
                var response = await client.GetAsync($"getWithSale/{id_venta}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Response<IEnumerable<DetalleVentaModel>>>();
                    return View(result.Data);
                }
            }
            return RedirectToAction("Index");
        }
    }
}