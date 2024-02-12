﻿using PizzaExpress.Entities;
using PizzaExpress.Repositories.Models;

namespace PizzaExpress.WebUI.Interfaces
{
    public interface ICartService
    {
        int GetCartCount(Guid cartId);
        CartModel GetCartDetails(Guid cartId);
        Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity);
        int DeleteItem(Guid cartId, int ItemId);
        int UpdateQuantity(Guid cartId, int id, int quantity);
        int UpdateCart(Guid CartId, int UserId);
    }
}