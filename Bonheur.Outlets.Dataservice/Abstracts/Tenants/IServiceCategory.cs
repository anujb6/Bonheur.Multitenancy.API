using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Tenants
{
    public interface IServiceCategory
    {
        Task<List<Category>> GetServiceCategorysAsync(string query);
        Task<Category> GetServiceCategoryByIdAsync(int categoryId);
        Task<ResponseModel> AddServiceCategoryAsync(Category category);
        Task<ResponseModel> UpdateServiceCategoryAsync(Category category);
        Task<ResponseModel> DeleteServiceCategoryAsync(int categoryId);
    }
}
