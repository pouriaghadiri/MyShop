using ShoppingSiteApi.Core;
using ShoppingSiteApi.Core.DTOs.Account;
using ShoppingSiteApi.Core.Services.Implementations;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.Core.Utilities.Extentions.Identity;
using ShoppingSiteApi.DataAccess.DTOs.Account;
using ShoppingSiteApi.DataAccess.Entities.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    /// <summary>
    /// API endpoints for user account management.
    /// </summary>
    public class AccountController : SiteBasicController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IUserTokenService _userTokenService;

        /// <summary>
        /// Creates a new instance of AccountController.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="userTokenService">The user token service.</param>
        public AccountController(IUserService userService, IConfiguration configuration, IUserTokenService userTokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _userTokenService = userTokenService;
        }

        #endregion

        #region Register

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">The user registration data.</param>
        /// <returns>Returns 200 OK if the user is registered successfully, 
        /// 400 Bad Request if the request data is invalid, or 
        /// 409 Conflict if the email is already registered.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(register);
                switch (res)
                {
                    case RegisterUserResult.EmailExist:
                        return Conflict();
                    default:
                        return Ok();
                }
            }

            return BadRequest();
        }

        #endregion

        #region Login

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="login">The user login data.</param>
        /// <returns>Returns 200 OK with the JWT token and the refresh token if the user is logged in successfully, 
        /// 400 Bad Request if the request data is invalid, 404 Not Found if the user is not found, or 
        /// 401 Unauthorized if the user is not activated.</returns>
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
                        return Unauthorized();
                    case LoginUserResult.Success:
                        {
                            var user = await _userService.GetUserByEmail(login.Email);
                            return Ok(CreateToken(user));
                        }
                    default:
                        break;
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Refreshes the JWT token for a user.
        /// </summary>
        /// <param name="refreshToken">The refresh token associated with the user.</param>
        /// <returns>Returns 200 OK with the new JWT token and refresh token if the refresh token is valid,
        /// 400 Bad Request if the refresh token is invalid,
        /// 404 Not Found if the user is not found, or 
        /// 401 Unauthorized if the refresh token has expired.</returns>
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

        /// <summary>
        /// Refreshes the JWT token for a user.
        /// </summary>
        /// <param name="refreshToken">The refresh token associated with the user.</param>
        /// <returns>Returns 200 OK with the new JWT token and refresh token if the refresh token is valid, 
        /// 400 Bad Request if the refresh token is invalid,
        /// 404 Not Found if the user is not found, or 
        /// 401 Unauthorized if the refresh token has expired.</returns>
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

        /// <summary>
        /// Checks if the user is authenticated and returns user information.
        /// </summary>
        /// <returns>Returns 200 OK with the user information if the user is authenticated, or 
        /// 401 Unauthorized if the user is not authenticated.</returns>
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


        /// <summary>
        /// Activates a user's account.
        /// </summary>
        /// <param name="activeCode">The activation code associated with the user's account.</param>
        /// <returns>Returns 200 OK if the user's account is activated successfully, or 404 Not Found if the user is not found.</returns>
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
