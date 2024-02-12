using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Entities;
using PizzaExpress.Services.Interfaces;
using PizzaExpress.WebUI.Models;

namespace PizzaExpress.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService service)
        {
            _authenticationService = service;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model,string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = _authenticationService.AuthenticateUser(model.Email,model.Password);
                if(user != null)
                {
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);


                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "DashBoard", new { area = "User" });
                    }
                }
            }
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserModel user)
        {
            if(ModelState.IsValid)
            {
                User _user = new User
                {
                    UserName = user.Email,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,

                };
                bool result = _authenticationService.CreateUser(_user, user.Password);

                if(result)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.SignOut();

            Response.Cookies.Append("CId", "");
            Response.Cookies.Delete("CId");//resettingg cartId in cookie

            return RedirectToAction("LogOutComplete");
        }
        public IActionResult LogOutComplete()
        {
            return View();
        }
        public IActionResult Unauthorize()
        {
            return View();
        }
    }
}
