using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.User
{
    public class CreateUserDTO
    {
        public int user_id { get; set; }
        public int shop_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone_no { get; set; }
        public string password { get; set; }
    }
}
