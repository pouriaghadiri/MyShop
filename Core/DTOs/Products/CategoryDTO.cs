using ShoppingSiteApi.DataAccess.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Products
{
    public class CategoryDTO
    {
        #region Properties

        public string Title { get; set; }

        public int ParentId { get; set; }


        #endregion 
    }
}
