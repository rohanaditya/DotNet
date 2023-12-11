using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Data;
using Microsoft.Extensions.Caching.Memory;
using Amazon.Runtime.Internal.Util;
using Microsoft.EntityFrameworkCore;

namespace MVC_CRUD.Controllers
{
    public class LoginController : Controller
    {
        private readonly MVCDbContext mvcDbContext;
        public static bool login = false;


        public LoginController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public bool userInList(string email)
        {
            var users = mvcDbContext.Users.ToList();
            return users.Any(user => user.email == email);
        }
        [HttpPost]
        public IActionResult Index(string email)
        {
            bool userPresent = userInList(email);

            if (userPresent)
            {
                login = true;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                //sessionStorage.setItem('email', null);
                ViewBag.ClearEmailFromSessionStorage = true;

                return RedirectToAction("Index", "Login");
            }
        }
    }
}    