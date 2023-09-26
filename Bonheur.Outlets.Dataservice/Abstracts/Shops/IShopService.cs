using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Shops
{
    public interface IShopService
    {
        Task<List<EntityData.Outlets.Models.Shop>> GetShopsAsync(string? query);
        Task<string> GetConnectionStringAsync();
        Task<Shop> GetShopByIdAsync(int shopId);
        Task<ResponseModel> AddShopAsync(Shop shop);
        Task<ResponseModel> UpdateShopAsync(Shop shop);
    }
}
