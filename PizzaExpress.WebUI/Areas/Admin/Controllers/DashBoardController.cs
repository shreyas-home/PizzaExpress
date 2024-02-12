using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Repositories.Models;
using PizzaExpress.Services.Implementations;
using PizzaExpress.Services.Interfaces;

namespace PizzaExpress.WebUI.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {
        IOrderService _orderService;

        public DashBoardController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index(int page = 1, int pageSize = 2)
        {
            var orders = _orderService.GetOrderList(page, pageSize);
            return View(orders);
        }

        [Route("~/Admin/Dashboard/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return View(Order);
        }
    }
}
