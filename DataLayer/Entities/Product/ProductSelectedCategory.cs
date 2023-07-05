using AngularMyApp.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.DataLayer.Entities.Product
{
    public class ProductSelectedCategory : BasicEntity
    {
        #region Properties

        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }


        #endregion
        #region Relations
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        #endregion
    }
}
