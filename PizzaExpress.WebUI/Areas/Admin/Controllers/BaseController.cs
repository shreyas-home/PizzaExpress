using Microsoft.AspNetCore.Mvc;
using PizzaExpress.WebUI.Helpers;

namespace PizzaExpress.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorise(Roles ="Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
