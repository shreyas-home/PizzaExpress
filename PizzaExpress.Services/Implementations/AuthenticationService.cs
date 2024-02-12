using Microsoft.AspNetCore.Identity;
using PizzaExpress.Entities;
using PizzaExpress.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> signInManager ,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public User AuthenticateUser(string username, string password)
        {
            var result = _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(username).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();

                return user;
            }
            return null;
        }

        public bool CreateUser(User user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                //Admin, User
                //string role = "Admin";

                string role = "User";
                var res = _userManager.AddToRoleAsync(user, role).Result;
                if (res.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string username)
        {
            return _userManager.FindByNameAsync(username).Result;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
