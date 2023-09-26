using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Shops
{
    public class GetShopDTO
    {
        public int shop_id { get; set; }
        public string shop_name { get; set; }

        public static GetShopDTO MapToDTO(Shop shop)
        {
            if (shop == null)
                return null;

            return new GetShopDTO
            {
                shop_id = shop.Id,
                shop_name = shop.ShopName
            };
        }
    }
}
