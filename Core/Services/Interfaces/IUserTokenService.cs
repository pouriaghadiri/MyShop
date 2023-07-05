using AngularMyApp.DataLayer.DTOs.Account;
using AngularMyApp.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Interfaces
{
    public interface IUserTokenService
    {

        public Task AddToken(UserToken userToken);
        public UserToken FindWithRefreshToken(string refreshToken);

    }
}
