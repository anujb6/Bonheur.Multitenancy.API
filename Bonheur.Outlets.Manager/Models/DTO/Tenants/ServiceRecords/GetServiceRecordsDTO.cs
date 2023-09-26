using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants.ServiceRecords
{
    public class GetServiceRecordsDTO
    {
        public int service_record_id { get; set; } 
        public GetServicesDTO service { get; set; }

        public GetCustomerDTO customer { get; set; } 

        public static GetServiceRecordsDTO MapToDTO(ServiceRecord getServiceRecords)
        {
            if (getServiceRecords == null)
                return null;

            var serviceRecords = new GetServiceRecordsDTO
            {
                service_record_id = getServiceRecords.Id,
            };

            
            if(getServiceRecords.Customer != null)
                serviceRecords.customer = GetCustomerDTO.MapToDTO(getServiceRecords.Customer);

            if(getServiceRecords.Service != null)
                serviceRecords.service = GetServicesDTO.MapToDTO(getServiceRecords.Service);

            return serviceRecords;
        }


    }
}
