using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants.Customers
{
    public class AddCustomerDTO
    {
        public int customer_id { get; set; }

        public string customer_name { get; set; } = null!;

        public string? phone_no { get; set; }

        public string? email { get; set; }

        public int? service_Id { get; set; }
    }
}
