using Bonheur.Outlets.API.Utils;
using Bonheur.Outlets.Manager.Managers.TenantManager;
using Bonheur.Outlets.Manager.Models.DTO.Tenants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bonheur.Outlets.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ApiController
    {
        private readonly ServiceManager _serviceManager;
       public ServicesController(
           ServiceManager serviceManager,
            IConfiguration configuration,
            IWebHostEnvironment environment)
            : base(configuration, environment)
        {
            _configuration = configuration;
            _serviceManager = serviceManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<GetServicesDTO>>> GetServicesAsync()
        {
            try
            {
                return await _serviceManager.GetServicesAsync();
            }
            catch(Exception ex)
            {
                return this.ProcessAPIException(ex);
            }
        }


    }
}
