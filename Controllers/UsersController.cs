using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using MVC_CRUD.Models.Domain;

namespace MVC_CRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public UsersController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]

        public async Task <IActionResult> Index()
        {
            var users = await mvcDbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWalletBalance(string loggedEmail, double amount)
        {
            var user = await mvcDbContext.Users.FirstOrDefaultAsync(u => u.email == loggedEmail);
            Console.WriteLine(user);
           // await mvcDbContext.Users.AddAsync(user);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
        {
            var user = new User()
            {
                userID = Guid.NewGuid(),
                userName = addUserRequest.userName,
                email = addUserRequest.email,
                address = addUserRequest.address,
                pincode = addUserRequest.pincode,
                mobile = addUserRequest.mobile,
                walletBalance = addUserRequest.walletBalance
            };
            
            await mvcDbContext.Users.AddAsync(user);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Login"); 
        }
    }
}
