using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Account
{
    public class UserToken:BasicEntity
    {
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshTokenHash { get; set; }
        public DateTime RefreshTokeExp { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

    }
}
