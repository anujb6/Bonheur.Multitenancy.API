using Bonheur.Outlets.API.Utils;
using Bonheur.Outlets.Manager.Managers;
using Bonheur.Outlets.Manager.Models.Authetication;
using Bonheur.Outlets.Manager.Models.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bonheur.Outlets.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;
        public UserController(
            UserManager userManager,
            IConfiguration configuration,
            IWebHostEnvironment environment)
            : base(configuration, environment)
        {
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Authenticated>> AuthenticateUser(Login model)
        {
            try
            {
                var newShopId = await _userManager.AuthenticateUser(model);
                return Ok(newShopId);
            }
            catch (Exception ex)
            {
                return this.ProcessAPIException(ex);

            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<Authenticated>> AddUser(CreateUserDTO model)
        {
            try
            {
                var newShopId = await _userManager.UpsertUserAsync(model);
                return Ok(newShopId);
            }
            catch (Exception ex)
            {
                return this.ProcessAPIException(ex);

            }
        }
    }
}
