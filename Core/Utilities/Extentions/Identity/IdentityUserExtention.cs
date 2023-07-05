using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Utilities.Extentions.Identity
{
    public static class IdentityUserExtention
    {
        public static int GetUserID(this ClaimsPrincipal principal)
        {
            if (principal != null)
            {
                var result = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                return Convert.ToInt32(result);
            }
            return 0;
        }
    }
}
