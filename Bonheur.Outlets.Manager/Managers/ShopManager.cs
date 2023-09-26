using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Bonheur.Outlets.Dataservice.Services;
using Bonheur.Outlets.Manager.Exceptions;
using Bonheur.Outlets.Manager.Models.Authetication;
using Bonheur.Outlets.Manager.Models.DTO.Shops;
using Bonheur.Outlets.Manager.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bonheur.Outlets.Manager.Managers
{
    public class ShopManager
    {
        private readonly IShopService _shopService;
        private readonly IShopsHelper _shopsHelper;
        public ShopManager(
            IShopService shopService,
            IShopsHelper shopsHelper) 
        {
            _shopService = shopService;
            _shopsHelper = shopsHelper;
        }
        public async Task<List<GetShopDTO>> GetShopsAsync(string? query)
        {
            var shops = await _shopService.GetShopsAsync(query);
            return shops.Select(x => GetShopDTO.MapToDTO(x)).ToList();
        }

        public async Task<ResponseModel> UpsertShopAsync(AddShopDTO shop)
        {
            var dicValidation = new Dictionary<string, string>();

            if (shop.shop_id < 0)
                dicValidation.Add("shop_id", "Invalid Shop Id");

            if (string.IsNullOrWhiteSpace(shop.shop_name))
                dicValidation.Add("shop_name", "Invalid Shop name remove whitespaces don't send null values");

            if (string.IsNullOrWhiteSpace(shop.shop_email))
                dicValidation.Add("shop_email", "Invalid shop email remove whitespaces don't send null values");

            if (string.IsNullOrWhiteSpace(shop.Address))
                dicValidation.Add("Address", "Invalid Address provided remove whitespaces or don't send null values");

            if (shop.shop_phoneNo.Count() != 10)
                dicValidation.Add("PhoneNumber", "Invalid Phone more than 10 numbers provided");

            if (dicValidation.Count == 0)
            {
                var isNew = shop.shop_id == 0;
                var userInstance = new Shop();

                if (!isNew)
                    userInstance = await _shopService.GetShopByIdAsync(shop.shop_id);

                userInstance.ShopName = shop.shop_name;
                userInstance.PhoneNo = shop.shop_phoneNo;
                userInstance.Address = shop.Address;
                userInstance.ConnectionString = $"server=localhost; port=3306; database={shop.shop_name}; user=root; password=asdf1234@#; Persist Security Info=False; Connect Timeout=300";

                if (isNew)
                    return await _shopService.AddShopAsync(userInstance);
                else
                    return await _shopService.UpdateShopAsync(userInstance);
            }
            else
                throw new OutletsDataException(dicValidation);
        }
    }
}
