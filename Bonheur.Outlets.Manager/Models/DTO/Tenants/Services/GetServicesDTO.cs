using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants
{
    public class GetServicesDTO
    {
        public int service_id { get; set; }
        public string service_name { get; set; }
        public bool is_active { get; set; }
        public DateTime created_on { get; set; }
        public GetCategoryDTO category { get; set; }       

        public static GetServicesDTO MapToDTO(Service service)
        {
            if (service == null)
                return null;

            var servicesDTO = new GetServicesDTO
            {
                service_id = service.Id,
                service_name = service.Name,
                is_active = service.IsActive,
                created_on = service.CreatedOn,
            };

            if (service.Category != null)
                servicesDTO.category = GetCategoryDTO.MapToDTO(service.Category);

            return servicesDTO;

        }

        public static Service MapToEntity(AddServicesDTO service)
        {
            if (service == null)
                return null;

            return new Service
            {
                Name = service.name,
                Price = service.price,
                CategoryId=service.category_id,
                IsActive = service.is_active
            };

        }
    }
}
