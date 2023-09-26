using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Tenants
{
    public interface IBuisnesServiceHelper
    {
        Task<List<Service>> GetServicesAsync();
        Task<Service> GetServiceByIdAsync(int serviceId);
        Task<ResponseModel> AddServiceAsync(Service service);
        Task<ResponseModel> UpdateServiceAsync(Service service);
        Task<ResponseModel> DeleteServiceAsync(int serviceId);
    }
}
