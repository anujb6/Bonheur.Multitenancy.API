using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Bonheur.Outlets.Manager.Exceptions;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Services;
using Bonheur.Outlets.Manager.Models.DTO.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Category;

namespace Bonheur.Outlets.Manager.Managers.TenantManager
{
    public class ServiceCategoryManager
    {
        private readonly IServiceCategory _serviceCategory;
        public ServiceCategoryManager(IServiceCategory serviceCategory)
        {
            _serviceCategory = serviceCategory;
        }

        public async Task<List<GetCategoryDTO>> GetCategoryAsync(string query)
        {
            var category = await _serviceCategory.GetServiceCategorysAsync(query);
            return category.Select(x => GetCategoryDTO.MapToDTO(x)).ToList();
        }

        public async Task<GetCategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var dicValidation = new Dictionary<string, string>();

            if (categoryId <= 0)
                dicValidation.Add("CategoryId", "Invalid Catgory Id");

            if (dicValidation.Count == 0)
            {
                var category = await _serviceCategory.GetServiceCategoryByIdAsync(categoryId);
                return GetCategoryDTO.MapToDTO(category);
            }
            else
                throw new OutletsDataException(dicValidation);
        }

        public async Task<ResponseModel> UpsertCatgoryAsync(AddCategoryDTO model)
        {
            var dicValidation = new Dictionary<string, string>();

            if (model.category_id < 0)
                dicValidation.Add("Id", "Invalid CategoryId provided");

            if (string.IsNullOrWhiteSpace(model.category_name))
                dicValidation.Add("Name", "Invalid Name Provided");

            if (dicValidation.Count == 0)
            {
                var isNew = model.category_id == 0;
                var catgory = new Category();
                if (!isNew)
                    catgory = await _serviceCategory.GetServiceCategoryByIdAsync(model.category_id);

                catgory.Id = model.category_id;
                catgory.Type = model.category_name;

                if (!isNew)
                    return await _serviceCategory.UpdateServiceCategoryAsync(catgory);
                else
                    return await _serviceCategory.AddServiceCategoryAsync(catgory);
            }
            else
                throw new OutletsDataException(dicValidation);
        }

        public async Task<ResponseModel> DeleteCategoryAsync(int categoryId)
        {
            var dicValidation = new Dictionary<string, string>();

            if (categoryId <= 0)
                dicValidation.Add("ServiceId", "Invalid Service Id");

            if (dicValidation.Count == 0)
                return await _serviceCategory.DeleteServiceCategoryAsync(categoryId);
            else
                throw new OutletsDataException(dicValidation);
        }
    }
}
