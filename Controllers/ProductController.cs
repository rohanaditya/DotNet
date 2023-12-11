using Amazon.Runtime;
using EO.WebBrowser.DOM;
using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using MVC_CRUD.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace MVC_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public static List<Product> Products { get; set; }
        public bool loggedIn { get; set; }

        public ProductController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
            Products = mvcDbContext.Products.ToList();
        }

        public string findProductByID(string productID)
        {
            var productWithProductId = Products.FirstOrDefault(p => p.productID.ToString() == productID);
            var json = new JObject();
            json["productID"] = productWithProductId.productID.ToString();
            json["productName"] = productWithProductId.productName;
            json["productQuantity"] = productWithProductId.productQuantity;
            json["productCost"] = productWithProductId.productCost;
            json["supplier"] = productWithProductId.supplier;
            json["description"] = productWithProductId.description;
            json["productImage"] = productWithProductId.productImage;
            var jsonString = JsonConvert.SerializeObject(json);

            return jsonString;
        }

        public static JObject findProduct(string productID)
        {
            var productWithProductId = Products.FirstOrDefault(p => p.productID.ToString() == productID);
            var json = new JObject();
            json["productID"] = productWithProductId.productID.ToString();
            json["productName"] = productWithProductId.productName;
            json["productQuantity"] = productWithProductId.productQuantity;
            json["productCost"] = productWithProductId.productCost;
            json["supplier"] = productWithProductId.supplier;
            json["description"] = productWithProductId.description;
            json["productImage"] = productWithProductId.productImage;

            return json;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool email = LoginController.login;
            if (email)
                ViewData["loggedIn"] = false;
            else
                ViewData["loggedIn"] = true;
            return View(Products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult GoToMain()
        {
            Console.WriteLine("This function is being called");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel addProductRequest)
        {
            var product = new Product()
            {
                productID = Guid.NewGuid(),
                productName = addProductRequest.productName,
                productQuantity = addProductRequest.productQuantity,
                productCost = addProductRequest.productCost,
                supplier = addProductRequest.supplier,
                description = addProductRequest.description,
                productImage = addProductRequest.productImage,
                categoryID = addProductRequest.categoryID
            };
            await mvcDbContext.Products.AddAsync(product);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> uploadFile([FromForm] string[] stringToSend)
        {
            foreach (var product in stringToSend.Skip(1))
            {
                if (product != null)
                {
                    string[] subProducts = product.Split(',');

                    var productWithProductName = Products.FirstOrDefault(p => p.productName.ToString() == subProducts[0]);
                    if (productWithProductName != null)
                    {
                        Console.WriteLine(productWithProductName.productID.ToString());
                    }
                    else
                    {
                       
                        var insertProduct = new Product()
                        {
                            productID = Guid.NewGuid(),
                            productName = subProducts[0],
                            productQuantity = int.Parse(subProducts[1]),
                            productCost = double.Parse(subProducts[2]),
                            supplier = subProducts[3],
                            description = subProducts[4],
                            productImage = subProducts[5],
                            categoryID = int.Parse(subProducts[6]),
                        };

                        await mvcDbContext.Products.AddAsync(insertProduct);
                        await mvcDbContext.SaveChangesAsync();
                    }
                }
                else
                {         
                    break;
                }
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
