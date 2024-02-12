using PizzaExpress.Entities;
using PizzaExpress.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(Guid cartId);
        int DeleteItem(Guid cartId, int itemId);
        int UpdateQuantity(Guid cartID,int itemId,int quantity);
        int UpdateCart(Guid cartId, int userId);

        CartModel GetCartDetails(Guid CartId);
    }
}
