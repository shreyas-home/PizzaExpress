using PizzaExpress.Entities;
using PizzaExpress.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int userId);
        PagingListModel<OrderModel> GetOrderList(int page, int pageSize);
        OrderModel GetOrderDetails(string id);
    }
}
