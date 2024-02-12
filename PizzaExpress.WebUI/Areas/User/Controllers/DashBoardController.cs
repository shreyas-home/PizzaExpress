using Microsoft.AspNetCore.Mvc;
using PizzaExpress.WebUI.Interfaces;

namespace PizzaExpress.WebUI.Areas.User.Controllers
{
    
    public class DashBoardController : BaseController
    {
        public DashBoardController(IUserAccessor userAccessor) : base(userAccessor)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
