using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Orders;
using ShoppingSiteApi.DataAccess.Entities.Products;
using ShoppingSiteApi.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class OrderDetailService : BaseCRUD<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderService _orderService;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Product> _product;

        public OrderDetailService(IOrderService orderService,
                            IGenericRepository<OrderDetail> orderDetailRepository,
                            IGenericRepository<Product> product,
                            IGenericRepository<User> userRepository) : base(orderDetailRepository)
        {

            _orderDetailRepository = orderDetailRepository;
            _orderService = orderService;
            _product = product;
            _userRepository = userRepository;

        }

        public async Task<OrderDetail> AddProductToOrder(int userID, int productID, int count)
        {
            var user = await _userRepository.GetEntitiesAsyncById(userID);
            var product = _product.GetEntitiesQuery().Where(x => !x.IsDelete && x.Id == productID).SingleOrDefault();

            if (user != null && product != null)
            {
                if (count < 1 )
                {
                    count = 1;
                }
                var order = await _orderService.GetOpenUserOrder(userID);
                var orderDetail = _orderDetailRepository.GetEntitiesQuery()
                                        .Where(x => x.OrderID == order.Id && x.ProductID == productID)
                                        .SingleOrDefault();
                if (orderDetail != null)
                {
                    orderDetail.Count = count;
                    await Update(orderDetail);
                    return orderDetail;
                }
                else
                {
                    OrderDetail detail = new OrderDetail()
                    {
                        OrderID = order.Id,
                        ProductID = productID,
                        Count = count,
                        Price = product.Price
                    };
                    Create(detail);
                    return detail;
                }

            }
            return null;
        }
    }
}
