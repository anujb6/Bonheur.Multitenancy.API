using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Tenants
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffAsync(string query);
        Task<Staff> GetStaffByIdAsync(int id);
        Task<ResponseModel> AddStaffAsync(Staff staff);
        Task<ResponseModel> UpdateSatffAsync(Staff staff);
        Task<ResponseModel> DeleteStaffAsync(int staffId);

    }
}
