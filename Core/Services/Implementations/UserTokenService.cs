using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Context;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Repository;

namespace AngularMyApp.Core.Services.Implementations
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IGenericRepository<UserToken> _userTokenRepository;
        public UserTokenService(IGenericRepository<UserToken> userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }
        public async Task AddToken(UserToken userToken)
        {
            await _userTokenRepository.AddEntity(userToken);
            await _userTokenRepository.SaveChenges();
        }

        public UserToken FindWithRefreshToken(string refreshToken)
        {
            var refreshTokenHash = SecurityHelper.GetHashSha256(refreshToken);
            return _userTokenRepository.GetEntitiesQuery().FirstOrDefault(x => x.RefreshTokenHash == refreshTokenHash);
        }

    }
}
