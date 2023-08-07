using ShoppingSiteApi.DataAccess.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IOrderDetailService:IBaseCRUD<OrderDetail>
    { 
        Task<OrderDetail> AddProductToOrder(int userID, int productID, int count);
    }
}
