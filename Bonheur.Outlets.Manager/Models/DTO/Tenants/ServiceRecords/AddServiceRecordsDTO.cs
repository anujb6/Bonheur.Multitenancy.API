using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants.ServiceRecords
{
    public class AddServiceRecordsDTO
    {
        public int Id { get; set; }
        public int service_id { get; set; }
        public int? customer_id { get; set; }
        public int staff_id { get; set; }

    }
}
