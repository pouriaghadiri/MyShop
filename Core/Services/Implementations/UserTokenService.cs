using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Context;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Repository;

namespace ShoppingSiteApi.Core.Services.Implementations
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
