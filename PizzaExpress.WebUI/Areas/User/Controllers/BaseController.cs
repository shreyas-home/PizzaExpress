using Microsoft.AspNetCore.Mvc;
using PizzaExpress.WebUI.Helpers;
using PizzaExpress.WebUI.Interfaces;

namespace PizzaExpress.WebUI.Areas.User.Controllers
{
    [CustomAuthorise(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public Entities.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        IUserAccessor _userAccessor;
        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

    }
}
