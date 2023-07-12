using ShoppingSiteApi.DataAccess.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Products
{
    public class CategoryUpdateDTO
    {
        #region Properties

        public int Id{ get; set; }

        public string Title { get; set; }

        public int? ParentId { get; set; }

        #endregion
    }
}
