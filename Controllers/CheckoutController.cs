using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC_CRUD.Controllers
{
    public class CheckoutController : Controller
    {
        public static List<JObject> CartItems = new List<JObject>();
        private readonly MVCDbContext _mvcDbContext;

        public CheckoutController(MVCDbContext mvcDbContext)
        {
            _mvcDbContext = mvcDbContext;
        }

        public static void AddProductsToCart(JObject cartProduct)
        {
            CartItems.Add(cartProduct);
            foreach (var cartItem in CartItems)
            {
                Console.WriteLine(cartItem.ToString());
            }
        }

        public static JObject dummy(string a)
        {
            return ProductController.findProduct(a);
        }
        public IActionResult Index()
        {
            ViewBag.CartItems = CartItems;
            return View();
        }
        public IActionResult Buy()
        {
            // Retrieve the user based on the email
           // UsersController.UpdateWalletBalance("test@test.com",0);
            return RedirectToAction("Index", "Product");
        }
    }
}
