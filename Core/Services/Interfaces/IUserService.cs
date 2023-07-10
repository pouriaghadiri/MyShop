using ShoppingSiteApi.DataAccess.DTOs.Account;
using ShoppingSiteApi.DataAccess.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();

        public Task<RegisterUserResult> RegisterUser(RegisterDTO user);

        bool isUserExistByEmail(string email);

        public Task<LoginUserResult> LoginUser(LoginUserDTO login);

        public Task<User> GetUserByEmail(string email);

        public Task<User> GetUserByID(int id);

        public Task<User> GetUserByActiveCode(string code);

        public void ActiveUser(User user);
    }
}
