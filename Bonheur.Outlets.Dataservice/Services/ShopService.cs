using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.EntityData.Outlets;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bonheur.Outlets.Dataservice.Services
{
    public class ShopService : IShopService
    {
        private readonly OutletsContext _outletContext;
        private readonly IDatabaseHelper _dbHelper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;

        public ShopService(
            OutletsContext outletContext,
            IDatabaseHelper dbHelper,
            IConfiguration configuration,
            IHttpContextAccessor httpContext
            )
        {
            _outletContext = outletContext;
            _dbHelper = dbHelper;
            _httpContext = httpContext;
            _configuration = configuration;
        }

        public async Task<string> GetConnectionStringAsync()
        {
            var shopId = _httpContext?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals("Shop_Id"))?.Value;
            var connectionstring = await _outletContext.Shops.Where(x => x.Id == Int32.Parse(shopId)).Select(x => x.ConnectionString).FirstOrDefaultAsync();
            return connectionstring;
        }

        public async Task<List<EntityData.Outlets.Models.Shop>> GetShopsAsync(string? query)
        {
            query = query.ToLower();
            var shops =  _outletContext.Shops.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                shops = shops.Where(x => x.ShopName.ToLower().Contains(query));

            return await shops.ToListAsync();
        }

        public async Task<EntityData.Outlets.Models.Shop> GetShopByIdAsync(int shopId)
        {
            var shop = await _outletContext.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
            return shop;
        }

        public async Task<ResponseModel> AddShopAsync(EntityData.Outlets.Models.Shop shop)
        {
            var connectionString = _configuration.GetConnectionString("CentralDatabase");
            shop.CreatedOn = DateTime.UtcNow;
            var responseModel = new ResponseModel();
            await _outletContext.AddAsync(shop);
            await _dbHelper.CreateShopDatabaseAsync(connectionString, shop.ShopName);

            var migrationConnectionString = connectionString.Replace("database=outlets", $"Database={shop.ShopName}");
            var optionsBuilder = new DbContextOptionsBuilder<TenantsContext>()
                .UseMySql(migrationConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

            using (var dbContext = new TenantsContext(optionsBuilder.Options))
            {
                dbContext.Database.Migrate();
            }

            if (_outletContext.SaveChanges() > 0)
            {       
                responseModel.Message = "Added Shop Succesfully";
                responseModel.Status = true;
            }
            else
            {
                responseModel.Message = "Error Adding Shop";
                responseModel.Status = false;
            }
            return responseModel;
        }

        public async Task<ResponseModel> UpdateShopAsync(EntityData.Outlets.Models.Shop shop)
        {
            shop.UpdatedOn = DateTime.UtcNow;
            var responseModel = new ResponseModel();
            var newShop = new Dataservice.EntityData.Outlets.Models.Shop
            {
                ShopName = shop.ShopName,
            };

             _outletContext.Update(newShop);
            if (await _outletContext.SaveChangesAsync() > 0)
            {
                responseModel.Message = "Shop Updated Succesfully";
                responseModel.Status = true;
            }
            else
            {
                responseModel.Message = "Error Updating Shop";
                responseModel.Status = false;
            }
            return responseModel;
        }

    }
}
