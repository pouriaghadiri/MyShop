using AngularMyApp.Core;
using AngularMyApp.Core.DTOs.Account;
using AngularMyApp.Core.Services.Implementations;
using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.Core.Utilities.Extentions.Identity;
using AngularMyApp.DataLayer.DTOs.Account;
using AngularMyApp.DataLayer.Entities.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularMyApp.WebAPI.Controllers
{

    public class AccountController : SiteBasicController
    {
        #region cunstractor
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IUserTokenService _userTokenService;
        public AccountController(IUserService userService, IConfiguration configuration, IUserTokenService userTokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _userTokenService = userTokenService;
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(register);
                switch (res)
                {
                    case RegisterUserResult.EmailExist:
                        return BadRequest();
                    default:
                        break;
                }
            }


            return Ok();
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(login);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return NotFound();
                    case LoginUserResult.NotActivated:
                        return BadRequest();
                    case LoginUserResult.Success:
                        {
                            var user = await _userService.GetUserByEmail(login.Email);
                            return (Ok(CreateToken(user)));

                        }
                    default:
                        break;
                }
            }

            return BadRequest();
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            try
            {
                var userToken = _userTokenService.FindWithRefreshToken(refreshToken);
                var user = await _userService.GetUserByID(userToken.UserID);

                if (user == null) return NotFound();
                if (userToken.RefreshTokeExp < DateTime.Now) return Unauthorized();
                return Ok(CreateToken(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        private LoginResultDTO CreateToken(User user)
        {
            var tokenExp = DateTime.Now.AddMinutes(5);
            var refreshTokenExp = DateTime.Now.AddDays(30);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.Email),
                new Claim("UserFullName" , user.FirstName +  " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            };
            string key = _configuration["JwtConfig:key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credetioal = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtConfig:issuer"],
                    audience: _configuration["JwtConfig:audience"],
                    claims: claims,
                    expires: tokenExp,
                    notBefore: DateTime.Now,
                    signingCredentials: credetioal
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();


            UserToken userToken = new UserToken()
            {
                TokenHash = SecurityHelper.GetHashSha256(jwtToken),
                TokenExp = tokenExp,
                RefreshTokenHash = SecurityHelper.GetHashSha256(refreshToken),
                RefreshTokeExp = refreshTokenExp,
                UserID = user.Id,
                User = user
            };
            _userTokenService.AddToken(userToken);

            return new LoginResultDTO()
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }

        #endregion

        #region Check Authentication

        [HttpPost("CheckAuthentication")]
        public async Task<IActionResult> CheckAuthentication()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.GetUserID();
                var currentUser = await _userService.GetUserByID(userID);
                UserInfoDTO userInfo = new UserInfoDTO()
                {
                    UserID = currentUser.Id,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Address = currentUser.Address,
                    Email = currentUser.Email,
                };
                return Ok(userInfo);
            }
            return Unauthorized();
        }

        #endregion

        #region Activate Account

        [HttpGet("ActivateAccount/{activeCode}")]
        public async Task<IActionResult> ActivateAccount( string activeCode)
        {
            var user = await _userService.GetUserByActiveCode(activeCode);
            if (user != null)
            {
                _userService.ActiveUser(user);
                return Ok();
            }
            return NotFound();
        }

        #endregion

        #region Sign Out
        //[HttpGet("Sign-Out")]
        //public async Task<IActionResult> SignOut()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await HttpContext.SignOutAsync();
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
        #endregion
    }
}
