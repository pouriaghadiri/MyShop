using ShoppingSiteApi.DataAccess.DTOs.Account;
using ShoppingSiteApi.DataAccess.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IUserTokenService
    {

        public Task AddToken(UserToken userToken);
        public UserToken FindWithRefreshToken(string refreshToken);

    }
}
