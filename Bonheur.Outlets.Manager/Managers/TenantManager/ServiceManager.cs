using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Bonheur.Outlets.Manager.Exceptions;
using Bonheur.Outlets.Manager.Models.DTO.Tenants;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Managers.TenantManager
{
    public class ServiceManager
    {
        private readonly IBuisnesServiceHelper _businessServiceHelper;
        public ServiceManager(IBuisnesServiceHelper businessServiceHelper)
        {
            _businessServiceHelper = businessServiceHelper;
        }

        public async Task<List<GetServicesDTO>> GetServicesAsync()
        {
            var services = await _businessServiceHelper.GetServicesAsync();
            return services.Select(x => GetServicesDTO.MapToDTO(x)).ToList();
        }

        public async Task<GetServicesDTO> GetServiceByIdAsync(int serviceId)
        {
            var dicValidation = new Dictionary<string, string>();

            if (serviceId <= 0)
                dicValidation.Add("ServiceId", "Invalid Service Id");

            if (dicValidation.Count == 0)
            {
                var service = await _businessServiceHelper.GetServiceByIdAsync(serviceId);
                return GetServicesDTO.MapToDTO(service);
            }
            else
                throw new OutletsDataException(dicValidation);
        }

        public async Task<ResponseModel> UpsertServiceAsync(AddServicesDTO model)
        {
            var dicValidation = new Dictionary<string, string>();

            if (model.service_id < 0)
                dicValidation.Add("Id", "Invalid ServiceId provided");

            if (string.IsNullOrWhiteSpace(model.name))
                dicValidation.Add("Name", "Invalid Name Provided");

            if (model.price < 0)
                dicValidation.Add("Price", "Invalid Price Provided");

            if (model.category_id <= 0)
                dicValidation.Add("CategoryId", "Invalid CategoryId Provided");

            if (model.is_active == null)
                dicValidation.Add("IsActive", "Invalid IsActive field provided");

            if (dicValidation.Count == 0)
            {
                var isNew = model.service_id == 0;
                var service = new Service();
                if (!isNew)
                    service = await _businessServiceHelper.GetServiceByIdAsync(model.service_id);

                service.CategoryId = model.category_id;
                service.Name = model.name;
                service.Price = model.price;
                service.IsActive = model.is_active;

                if (!isNew)
                    return await _businessServiceHelper.UpdateServiceAsync(service);
                else
                    return await _businessServiceHelper.AddServiceAsync(service);
            }
            else
                throw new OutletsDataException(dicValidation);
        }

        public async Task<ResponseModel> DeleteServiceAsync(int serviceId)
        {
            var dicValidation = new Dictionary<string, string>();

            if (serviceId <= 0)
                dicValidation.Add("ServiceId", "Invalid Service Id");

            if(dicValidation.Count == 0)
                return await _businessServiceHelper.DeleteServiceAsync(serviceId);
            else
                throw new OutletsDataException(dicValidation);
        }

    }
}
