using Microsoft.EntityFrameworkCore;
using PizzaExpress.Entities;
using PizzaExpress.Repositories.Interfaces;
using PizzaExpress.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace PizzaExpress.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private AppDbContext appDBContext
        {
            get
            {
                return _dbContext as AppDbContext;
            }
        }


        public OrderRepository(DbContext db) : base(db)
        {

        }
        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return appDBContext.Orders.Include(o=>o.OrderItems).Where(o=>o.UserId==userId).ToList();
        }

        public OrderModel GetOrderDetails(string id)
        {
            var model = (from order in appDBContext.Orders
                         where order.Id == id
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = (int)order.UserId,
                             CreatedDate = order.CreatedDate,
                             Items = (from orderItem in appDBContext.OrderItems
                                      join item in appDBContext.Items
                                      on orderItem.ItemId equals item.Id
                                      where orderItem.OrderId == id
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in appDBContext.Orders
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = (int)order.UserId,
                            PaymentId = order.PaymentId,
                            CreatedDate = order.CreatedDate,
                            //GrandTotal = payment.GrandTotal,
                            Locality = order.Locality
                        });

            int itemCounts = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);

            var pagedListData = new StaticPagedList<OrderModel>(orders, page, pageSize, itemCounts);

            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = pageSize;
            pagingModel.TotalRows = itemCounts;
            return pagingModel;
        }
    }
}
