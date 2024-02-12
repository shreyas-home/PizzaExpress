using PizzaExpress.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool CreateUser(User user , string password);
        Task<bool> SignOut();
        User AuthenticateUser(string username, string password);
        User GetUser(string username);
    }
}
