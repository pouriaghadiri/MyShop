using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Products
{
    public class Comment : BasicEntity
    {

        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }


        public Product product { get; set; }
        public User User { get; set; }

    }
}
