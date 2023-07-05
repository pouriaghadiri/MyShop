using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.DTOs.Account;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> repository)
        {
            _userRepository = repository;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }
        

        public async Task<RegisterUserResult> RegisterUser(RegisterDTO userRegister)
        {
            if (isUserExistByEmail(userRegister.Email))
            {
                return RegisterUserResult.EmailExist;
            }
            var newUser = new User()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Address = userRegister.Address,
                EmailActiveCode = Guid.NewGuid().ToString(),
                Password = SecurityHelper.GetHashSha256(userRegister.Password)
            };
            await _userRepository.AddEntity(newUser);
             _userRepository.SaveChenges();

            return RegisterUserResult.Success;
        }

        public bool isUserExistByEmail(string email)
        {
           return _userRepository.GetEntitiesQuery().Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).Any();
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login)
        {
            var passwordHash = SecurityHelper.GetHashSha256(login.Password);
            var user = await _userRepository.GetEntitiesQuery()
                        .SingleOrDefaultAsync(x => x.Email == login.Email.ToLower().Trim()
                            && x.Password == passwordHash);
            if (user == null) return LoginUserResult.IncorrectData;
            if (!user.IsActivated) return LoginUserResult.NotActivated;


            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(x => x.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserByID(int id)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<User> GetUserByActiveCode(string code)
        {
            return _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(x => x.EmailActiveCode == code);
        }

        public void ActiveUser(User user)
        {
            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            _userRepository.UpdateEntity(user);
            _userRepository.SaveChenges();
        }
    }
}
