using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Shops
{
    public class AddShopDTO
    {
        public int shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_email { get; set; }
        public string shop_phoneNo { get; set; }
        public string Address { get; set; }
    }
}
