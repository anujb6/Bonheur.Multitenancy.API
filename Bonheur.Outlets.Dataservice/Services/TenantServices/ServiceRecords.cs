using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bonheur.Outlets.Dataservice.Services.TenantServices
{
    public class ServiceRecords : IServiceRecords
    {
        private readonly TenantsContext _tenantsContext;
        public ServiceRecords(
            IHttpContextAccessor httpContextAccessor,
            ITenantDbContextFactory tenantDbContextFactory
            )
        {
            _tenantsContext = tenantDbContextFactory.Create(httpContextAccessor);
        }

        public async Task<List<ServiceRecord>> GetServiceRecordsAsync(string query)
        {
            query = query.ToLower();

            var serviceRecords = _tenantsContext.ServiceRecords
                .Include(x => x.Service)
                .Include(x => x.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                serviceRecords = serviceRecords.Where(x =>
                    x.Customer.Name.ToLower().Contains(query) ||
                    x.Service.Name.ToLower().Contains(query) ||
                    x.Service.Category.Type.ToLower().Contains(query) ||
                    x.Service.Price.ToString().Equals(query));
            }

            return await serviceRecords.ToListAsync();
        }

        public async Task<ServiceRecord> GetServiceRecordByIdAsync(int serviceRecordId)
        {
            return await _tenantsContext.ServiceRecords.FindAsync(serviceRecordId);
        }

        public async Task<ResponseModel> AddServiceRecordAsync(ServiceRecord serviceRecord)
        {
            _tenantsContext.ServiceRecords.Add(serviceRecord);
            if (await _tenantsContext.SaveChangesAsync() > 0)
                return new ResponseModel
                {
                    Message = "Service Record Added Succesfully",
                    Status = true
                };
            else
                return new ResponseModel
                {
                    Message = "Error Adding Service Record Succesfully",
                    Status = false
                };
        }

        public async Task<ResponseModel> UpdateServiceRecordAsync(ServiceRecord serviceRecord)
        {
            _tenantsContext.ServiceRecords.Update(serviceRecord);
            if (await _tenantsContext.SaveChangesAsync() > 0)
                return new ResponseModel
                {
                    Message = "Service Record Updated Succesfully",
                    Status = true
                };
            else
                return new ResponseModel
                {
                    Message = "Error Updating Service Record Succesfully",
                    Status = false
                };
        }

        public async Task<ResponseModel> DeleteServiceRecordAsync(int serviceRecordId)
        {
            var serviceRecord = await _tenantsContext.ServiceRecords.FindAsync(serviceRecordId);
            if (serviceRecord != null)
            {
                _tenantsContext.ServiceRecords.Remove(serviceRecord);
                if (await _tenantsContext.SaveChangesAsync() > 0)
                    return new ResponseModel
                    {
                        Message = "Service Record Deleted Succesfully",
                        Status = true
                    };
                else
                    return new ResponseModel
                    {
                        Message = "Error Deleted Service Record",
                        Status = false
                    };
            }
            return new ResponseModel
            {
                Message = "Service Record not found",
                Status = false
            };
        }
 
    }
}
