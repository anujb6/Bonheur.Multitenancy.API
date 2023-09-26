using Bonheur.Outlets.API.Utils;
using Bonheur.Outlets.Dataservice.Abstracts;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Bonheur.Outlets.Manager.Managers;
using Bonheur.Outlets.Manager.Models.Authetication;
using Bonheur.Outlets.Manager.Models.DTO.Shops;
using Bonheur.Outlets.Manager.Models.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace Bonheur.Outlets.API.Controllers
{
    [ApiController]
    [Route("api/shops")]
    public class ShopsController : ApiController
    {
        private readonly ShopManager _shopManager;

        public ShopsController(ShopManager shopManager, IConfiguration configuration,
            IWebHostEnvironment environment)
            : base(configuration, environment)
        {
            _shopManager = shopManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetShopDTO>>> GetShopsAsync(string? query)
        {
            try
            {
                var shops = await _shopManager.GetShopsAsync(query);
                return shops;
            }
            catch (Exception ex)
            {
                return this.ProcessAPIException(ex);

            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> AddShopAsync(AddShopDTO shop)
        {
            try
            {
                var newShopId = await _shopManager.UpsertShopAsync(shop);
                return Ok(newShopId);
            }
            catch (Exception ex)
            {
                return this.ProcessAPIException(ex);
            
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel>> UpdateShopAsync(AddShopDTO shop)
        {
            try
            {
                var newShopId = await _shopManager.UpsertShopAsync(shop);
                return Ok(newShopId);
            }
            catch (Exception ex)
            {
                return this.ProcessAPIException(ex);

            }
        }
    }

}
