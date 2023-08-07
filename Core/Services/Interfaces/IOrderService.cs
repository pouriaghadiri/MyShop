using ShoppingSiteApi.DataAccess.Entities.Orders;
using ShoppingSiteApi.DataAccess.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IOrderService: IBaseCRUD<Order>
    {
        Task<Order> CreateOrderUser(int userID);
        Task<Order> GetOpenUserOrder(int userID);
    }
}
