using PizzaExpress.Entities;
using PizzaExpress.Repositories.Interfaces;
using PizzaExpress.Repositories.Models;
using PizzaExpress.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public OrderModel GetOrderDetails(string OrderId)
        {
            var model = _orderRepo.GetOrderDetails(OrderId);
            if (model != null && model.Items.Count > 0)
            {
                decimal subTotal = 0;
                foreach (var item in model.Items)
                {
                    item.Total = item.UnitPrice * item.Quantity;
                    subTotal += item.Total;
                }
                model.Total = subTotal;
                //5% tax
                model.Tax = Math.Round((model.Total * 5) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10)
        {
            return _orderRepo.GetOrderList(page, pageSize);
        }

        //public PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10)
        //{
        //    
        //}

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _orderRepo.GetUserOrders(UserId);
        }

        public int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address)
        {
            Order order = new Order
            {
                PaymentId = "",
                UserId = userId,

                CreatedDate = DateTime.Now,
                Id = orderId,
                Street = address.Street,
                Locality = address.Locality,
                City = address.City,
                ZipCode = address.ZipCode,
                PhoneNumber = address.PhoneNumber
            };

            foreach (var item in cart.Items)
            {
                OrderItem orderItem = new OrderItem(item.ItemId, item.UnitPrice, item.Quantity, item.Total);
                order.OrderItems.Add(orderItem);
            }

            _orderRepo.Add(order);
            return _orderRepo.SaveChanges();
        }
    }
}
