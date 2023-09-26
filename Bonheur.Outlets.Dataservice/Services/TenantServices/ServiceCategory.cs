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
    public class ServiceCategory : IServiceCategory
    {
        private readonly TenantsContext _tenantsContext;
        public ServiceCategory(
            IHttpContextAccessor httpContextAccessor,
            ITenantDbContextFactory tenantDbContextFactory
            )
        {
            _tenantsContext = tenantDbContextFactory.Create(httpContextAccessor);
        }

        public async Task<List<Category>> GetServiceCategorysAsync(string query)
        {
            query = query.ToLower();
            var categorys = _tenantsContext.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                categorys = categorys.Where(x => x.Type.ToLower().Contains(query));

            return await categorys.ToListAsync();
        }

        public async Task<Category> GetServiceCategoryByIdAsync(int categoryId)
        {
            return await _tenantsContext.Categories.FindAsync(categoryId);
        }

        public async Task<ResponseModel> AddServiceCategoryAsync(Category category)
        {
            _tenantsContext.Categories.Add(category);
            var savedCount = await _tenantsContext.SaveChangesAsync();

            if (savedCount > 0)
            {
                return new ResponseModel
                {
                    Message = "Service Category Added Successfully",
                    Status = true
                };
            }
            else
            {
                return new ResponseModel
                {
                    Message = "Error Adding Service Category",
                    Status = false
                };
            }
        }

        public async Task<ResponseModel> UpdateServiceCategoryAsync(Category category)
        {
            _tenantsContext.Categories.Update(category);
            var savedCount = await _tenantsContext.SaveChangesAsync();

            if (savedCount > 0)
            {
                return new ResponseModel
                {
                    Message = "Service Category Updated Successfully",
                    Status = true
                };
            }
            else
            {
                return new ResponseModel
                {
                    Message = "Error Updating Service Category",
                    Status = false
                };
            }
        }

        public async Task<ResponseModel> DeleteServiceCategoryAsync(int categoryId)
        {
            var category = await _tenantsContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _tenantsContext.Categories.Remove(category);
                var savedCount = await _tenantsContext.SaveChangesAsync();

                if (savedCount > 0)
                {
                    return new ResponseModel
                    {
                        Message = "Service Category Deleted Successfully",
                        Status = true
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        Message = "Error Deleting Service Category",
                        Status = false
                    };
                }
            }

            return new ResponseModel
            {
                Message = "Service Category not found",
                Status = false
            };
        }

    }
}
