using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.ServiceRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants.Customers
{
    public class GetCustomerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? PhoneNo { get; set; }

        public string? Email { get; set; }

        public List<GetServiceRecordsDTO> service_records { get; set; }

        public static GetCustomerDTO MapToDTO(Customer customer)
        {
            if (customer == null)
                return null;

            var customerDTO = new GetCustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNo = customer.PhoneNo,
                Email = customer.Email,
            }; 

            if(customer.ServiceRecords != null)
                   customerDTO.service_records = customer.ServiceRecords.Select(x => GetServiceRecordsDTO.MapToDTO(x)).ToList();

            return customerDTO;
        }
    }
}
