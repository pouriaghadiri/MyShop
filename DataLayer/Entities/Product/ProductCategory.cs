using AngularMyApp.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.DataLayer.Entities.Product
{
    public class ProductCategory:BasicEntity
    {
        #region Properties

        public string Title { get; set; }

        public int? ParentId { get; set; }


        #endregion
        #region Relations

        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }
        public ICollection<ProductCategory> ProductSelectedCategories { get; set; }

        #endregion
    }
}
