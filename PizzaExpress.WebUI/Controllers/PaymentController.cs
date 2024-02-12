using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Entities;
using PizzaExpress.Repositories.Models;
using PizzaExpress.Services.Interfaces;
using PizzaExpress.WebUI.Helpers;
using PizzaExpress.WebUI.Interfaces;
using PizzaExpress.WebUI.Models;

namespace PizzaExpress.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IOrderService _orderService;
        PaymentModel paymentModel = new PaymentModel();

        public PaymentController(IOrderService orderService,UserManager<User> userAccessor):base(userAccessor)
        {
                _orderService = orderService;
        }
        public IActionResult Index()
        {
            PaymentModel payment = new PaymentModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }
            payment.GrandTotal = Math.Round(cart.GrandTotal);
            payment.Currency = "INR";
            string items = "";
            foreach (var item in cart.Items)
            {
                items += item.Name + ",";
            }
            payment.Description = items;
            paymentModel = payment;

            return View(payment);
        }

        public IActionResult Order()
        {
            Address address = TempData.Get<Address>("Address");
            CartModel cart = TempData.Get<CartModel>("Cart");
            Guid id = Guid.NewGuid();

            _orderService.PlaceOrder(CurrentUser.Id, id.ToString(), "", cart, address);

            Response.Cookies.Append("CId", ""); //resettingg cartId in cookie

            TempData.Clear();
            
            return View();
        }
    }
}
