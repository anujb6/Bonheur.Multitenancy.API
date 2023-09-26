using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Helper;
using Bonheur.Outlets.Dataservice.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Services.TenantServices
{
    public class BuisnesServices : IBuisnesServiceHelper
    {
        private readonly TenantsContext _tenantsContext;
        public BuisnesServices(
            IHttpContextAccessor httpContextAccessor,
            ITenantDbContextFactory tenantDbContextFactory
            )
        {
            _tenantsContext = tenantDbContextFactory.Create(httpContextAccessor);
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            return await _tenantsContext.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _tenantsContext.Services.FindAsync(serviceId);
        }

        public async Task<ResponseModel> AddServiceAsync(Service service)
        {
            _tenantsContext.Services.Add(service);
            if (await _tenantsContext.SaveChangesAsync() > 0)
                return new ResponseModel
                {
                    Message = "Service Added Succesfully",
                    Status = true
                };
            else
                return new ResponseModel
                {
                    Message = "Error Adding Service Succesfully",
                    Status = false
                };
        }

        public async Task<ResponseModel> UpdateServiceAsync(Service service)
        {
            _tenantsContext.Services.Update(service);
            if (await _tenantsContext.SaveChangesAsync() > 0)
                return new ResponseModel
                {
                    Message = "Service Updated Succesfully",
                    Status = true
                };
            else
                return new ResponseModel
                {
                    Message = "Error Updated Service Succesfully",
                    Status = false
                };
        }

        public async Task<ResponseModel> DeleteServiceAsync(int serviceId)
        {
            var service = await _tenantsContext.Services.FindAsync(serviceId);
            if (service != null)
            {
                _tenantsContext.Services.Remove(service);
                if (await _tenantsContext.SaveChangesAsync() > 0)
                    return new ResponseModel
                    {
                        Message = "Service Deleted Succesfully",
                        Status = true
                    };
                else
                    return new ResponseModel
                    {
                        Message = "Error Deleting Service Succesfully",
                        Status = false
                    };
            }
            return new ResponseModel
            {
                Message = "Service not found",
                Status = false
            };
        }

    }
}
