using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Orders;
using ShoppingSiteApi.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class OrderService: BaseCRUD<Order> , IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<User> _userRepository;
        public OrderService(IGenericRepository<Order> orderRepository ,
                            IGenericRepository<OrderDetail> orderDetailRepository ,
                            IGenericRepository<User> userRepository) :base(orderRepository)
        {

            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;

        }

        public async Task<Order> CreateOrderUser(int userID)
        {
            var user = _userRepository.GetEntitiesAsyncById(userID);
            if (user != null ) 
            {
                Order order = new Order()
                {
                    UserID = userID,
                };
                await Create(order);

                return order;
            }
            return null;
        }

        public async Task<Order> GetOpenUserOrder(int userID)
        {
            var order = _orderRepository.GetEntitiesQuery().Where(x => x.UserID == userID && !x.IsPayed && !x.IsDelete).SingleOrDefault();
            if (order == null) 
            {
                order = await CreateOrderUser(userID);
            }
            return order;
        }




    }
}
