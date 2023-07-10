using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Account
{
    public class LoginResultDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
