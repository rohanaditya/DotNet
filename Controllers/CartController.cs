using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC_CRUD.Controllers
{
    public class CartController : Controller
    {
        public JObject AddItemToCart(string productId, int quantity)
        {
            Console.WriteLine(productId+ " Quantity = "+quantity);
            var cartItem = new JObject()
            {
                { "ProductId", productId },
                { "Quantity", quantity }
            };
            CheckoutController.AddProductsToCart(cartItem);
            return cartItem;

        }
        public Product convertToJson(string product)
        {
            Product productWithID = JsonConvert.DeserializeObject<Product>(product);
            return productWithID;
        }
        public IActionResult GoBack()
        {
            try
            {
                return RedirectToAction("Index", "Product"); // Replace with your desired URL
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home"); // Redirect to an error page or handle the error gracefully
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}