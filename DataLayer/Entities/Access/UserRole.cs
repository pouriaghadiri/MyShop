using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.DataLayer.Entities.Access
{
    public class UserRole:BasicEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations
        public User User{ get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
