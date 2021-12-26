using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Task4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Task4.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationContext applicationContext;
        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, ApplicationContext _applicationContext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            applicationContext = _applicationContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = model.Email, UserName = model.Email,RegistrationDate = System.DateTime.UtcNow };
                applicationContext.SaveChanges();

                user.Status = applicationContext.Statuses.Where(s => s.StatusName == "Unblocked").FirstOrDefault();
                user.LastLoginDate = user.RegistrationDate;
                try
                {
                    int max = await applicationContext.Users.MaxAsync(p => p.UserId);
                    user.UserId = max + 1;
                }
                catch (System.InvalidOperationException ex)
                {
                    user.UserId = 0;
                }


                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    User user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null) 
                    {
                        user.LastLoginDate = DateTime.UtcNow;
                        await userManager.UpdateAsync(user);
                    }
                    if (user.StatusId == applicationContext.Statuses.Where(s => s.StatusName == "Blocked").FirstOrDefault().Id) 
                    {
                        await signInManager.SignOutAsync();
                    }
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return RedirectToAction(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UsersTableViewModel model) 
        {
            List<User> users = (await userManager.Users.ToListAsync());

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            for (int i = 0; i < model.UserChecked.Length; i++) 
            {
                if (model.UserChecked[i])
                {
                    if(currentUser == users[i])
                        await signInManager.SignOutAsync();
                     await userManager.DeleteAsync(users[i]);
                }
            }
            

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Block(UsersTableViewModel model)
        {
            List<User> users = (await userManager.Users.ToListAsync());
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            for (int i = 0; i < model.UserChecked.Length; i++)
            {
                if (model.UserChecked[i])
                {
                    users[i].Status = applicationContext.Statuses.Where(s => s.StatusName == "Blocked").FirstOrDefault(); 
                    if (currentUser == users[i])
                        await signInManager.SignOutAsync();
                    await userManager.UpdateAsync(users[i]);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unblock(UsersTableViewModel model)
        {

            List<User> users = (await userManager.Users.ToListAsync());
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            for (int i = 0; i < model.UserChecked.Length; i++)
            {
                if (model.UserChecked[i])
                {
                    users[i].Status = applicationContext.Statuses.Where(s => s.StatusName == "Unblocked").FirstOrDefault();
                    await userManager.UpdateAsync(users[i]);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
