using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;
using Microsoft.AspNetCore.Identity;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationContext applicationContext;

        public HomeController(ILogger<HomeController> logger, UserManager<User> _userManager, SignInManager<User> _signInManager, ApplicationContext _applicationContext)
        {
            _logger = logger;
            userManager = _userManager;
            signInManager = _signInManager;
            applicationContext = _applicationContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) 
                return View();
            var usersTableViewModel = new UsersTableViewModel() { Users = userManager.Users.ToList(),
                    Statuses = applicationContext.Statuses.ToList()};
            
            usersTableViewModel.UserChecked = new bool[usersTableViewModel.Users.Count()];
            return View(usersTableViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register() 
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction( "Register", "Account");
            else
                return View("Index");
        }

        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            else
                return View("Index");        
        }
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Logout", "Account");
            else
                return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
