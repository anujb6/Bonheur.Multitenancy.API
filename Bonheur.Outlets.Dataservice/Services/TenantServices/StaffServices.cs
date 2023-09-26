using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Services.TenantServices
{
    public class StaffServices : IStaffService
    {
        private readonly TenantsContext _tenantsContext;
        public StaffServices(
            IHttpContextAccessor httpContextAccessor,
            ITenantDbContextFactory tenantDbContextFactory
            )
        {
            _tenantsContext = tenantDbContextFactory.Create(httpContextAccessor);
        }

        public async Task<List<Staff>> GetStaffAsync(string query)
        {
            query = query.ToLower();
            var staff = _tenantsContext.Staff.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                staff = staff.Where(x =>
                x.Name.ToLower().Contains(query) ||
                x.PhoneNo.Contains(query)
                );
            }

            return await staff.ToListAsync();
        }

        public async Task<Staff> GetStaffByIdAsync(int id)
        {
            return await _tenantsContext.Staff.FindAsync(id);
        }

        public async Task<ResponseModel> AddStaffAsync(Staff staff)
        {
            _tenantsContext.Staff.Add(staff);
            var savedCount = await _tenantsContext.SaveChangesAsync();

            if (savedCount > 0)
            {
                return new ResponseModel
                {
                    Message = "Staff Added Successfully",
                    Status = true
                };
            }
            else
            {
                return new ResponseModel
                {
                    Message = "Error Adding Staff",
                    Status = false
                };
            }
        }

        public async Task<ResponseModel> UpdateSatffAsync(Staff staff)
        {
            _tenantsContext.Staff.Update(staff);
            var savedCount = await _tenantsContext.SaveChangesAsync();

            if (savedCount > 0)
            {
                return new ResponseModel
                {
                    Message = "Staff Updated Successfully",
                    Status = true
                };
            }
            else
            {
                return new ResponseModel
                {
                    Message = "Error Updating Staff",
                    Status = false
                };
            }
        }

        public async Task<ResponseModel> DeleteStaffAsync(int staffId)
        {
            var staff = await _tenantsContext.Staff.FindAsync(staffId);
            if (staff != null)
            {
                _tenantsContext.Staff.Remove(staff);
                var savedCount = await _tenantsContext.SaveChangesAsync();

                if (savedCount > 0)
                {
                    return new ResponseModel
                    {
                        Message = "Staff Deleted Successfully",
                        Status = true
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        Message = "Error Deleting Staff",
                        Status = false
                    };
                }
            }

            return new ResponseModel
            {
                Message = "Staff not found",
                Status = false
            };
        }
    }
}
