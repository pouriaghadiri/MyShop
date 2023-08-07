using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Orders
{
    public class Order : BasicEntity
    {
        public int UserID { get; set; }
        public bool IsPayed { get; set; }
        public DateTime? PaymentDate { get; set; }

        #region Relations

        public User User{ get; set; }

        public ICollection<OrderDetail> OrderDetails{ get; set; }
        #endregion
    }
}
