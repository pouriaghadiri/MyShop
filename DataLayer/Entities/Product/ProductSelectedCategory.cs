using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Product
{
    public class ProductSelectedCategory : BasicEntity
    {
        #region Properties

        public int ProductId { get; set; }

        public int CategoryId { get; set; }


        #endregion
        #region Relations
        public Product Product { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}
