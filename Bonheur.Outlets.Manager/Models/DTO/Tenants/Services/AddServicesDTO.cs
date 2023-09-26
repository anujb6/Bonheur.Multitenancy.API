using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants.Services
{
    public class AddServicesDTO
    {
        public int service_id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int category_id { get; set; }
        public bool is_active { get; set; }
    }
}
