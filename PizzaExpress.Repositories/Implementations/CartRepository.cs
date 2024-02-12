using Microsoft.EntityFrameworkCore;
using PizzaExpress.Entities;
using PizzaExpress.Repositories.Interfaces;
using PizzaExpress.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDbContext appDBContext
        {
            get
            {
                return _dbContext as AppDbContext;
            }
        }


        public CartRepository(DbContext db) : base(db)
        {
            
        }
        public Cart GetCart(Guid cartId)
        {
            Cart? cart = new Cart();
            try
            {
                cart= appDBContext.Carts.Include("Items").Where(c => c.Id == cartId && c.IsActive == true).FirstOrDefault();
            }
            catch(Exception e)
            {

            }
          
            return cart;
        }

        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = appDBContext.CartItems.Where(c=>c.CartId==cartId && c.ItemId==itemId).FirstOrDefault();
            if(item != null)
            {
                appDBContext.CartItems.Remove(item);
                return appDBContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateQuantity(Guid cartID, int itemId, int quantity)
        {
            bool flag = false;
            var cart = GetCart(cartID);
            if (cart != null)
            {
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].Id == itemId)
                    {
                        flag = true;
                        //for minus quantity
                        if (quantity < 0 && cart.Items[i].Quantity > 1)
                            cart.Items[i].Quantity += (quantity);
                        else if (quantity > 0)
                            cart.Items[i].Quantity += (quantity);
                        break;
                    }
                }
                if (flag)
                    return appDBContext.SaveChanges();
            }
            return 0;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return appDBContext.SaveChanges();

        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in appDBContext.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Items = (from cartItem in appDBContext.CartItems
                                      join item in appDBContext.Items
                                      on cartItem.ItemId equals item.Id
                                      where cartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = cartItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = cartItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = cartItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }
    }
}
