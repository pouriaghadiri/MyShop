using ShoppingSiteApi.DataAccess.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Products
{
    public class CommentDTO
    {
        #region Properties

        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedWhen { get; set; }

        #endregion 
    }
}
