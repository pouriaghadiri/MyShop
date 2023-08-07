using ShoppingSiteApi.DataAccess.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Products
{
    public class ProductCategoryDTO
    {
        #region Properties

        public int ProductId { get; set; }

        public int CategoryId { get; set; }


        #endregion 
    }
}
