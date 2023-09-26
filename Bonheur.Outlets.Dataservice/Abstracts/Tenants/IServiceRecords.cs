using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Tenants
{
    public interface IServiceRecords
    {
        Task<List<ServiceRecord>> GetServiceRecordsAsync(string query);
        Task<ServiceRecord> GetServiceRecordByIdAsync(int serviceRecordId);
        Task<ResponseModel> AddServiceRecordAsync(ServiceRecord serviceRecord);
        Task<ResponseModel> UpdateServiceRecordAsync(ServiceRecord serviceRecord);
        Task<ResponseModel> DeleteServiceRecordAsync(int serviceRecordId);
    }
}
