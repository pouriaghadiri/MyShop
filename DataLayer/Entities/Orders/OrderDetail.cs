using ShoppingSiteApi.DataAccess.Entities.Common;
using ShoppingSiteApi.DataAccess.Entities.Products;

namespace ShoppingSiteApi.DataAccess.Entities.Orders
{
    public class OrderDetail:BasicEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }

        #region Relations
        public Order Order { get; set; }
        public Product MyProperty { get; set; }
        #endregion
    }
}
