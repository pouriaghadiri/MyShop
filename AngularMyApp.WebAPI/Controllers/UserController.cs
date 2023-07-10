using ShoppingSiteApi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    public class UserController : SiteBasicController
    {
        #region constructor

        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region users list

        [HttpGet("User")]
        public async Task<IActionResult> Users()
        {
            return new ObjectResult(await userService.GetAllUsers());
        }

        #endregion
    }
}
