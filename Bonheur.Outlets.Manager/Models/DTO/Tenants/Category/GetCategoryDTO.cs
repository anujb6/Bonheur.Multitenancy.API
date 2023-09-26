using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Manager.Models.DTO.Tenants.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.DTO.Tenants
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public static GetCategoryDTO MapToDTO(Dataservice.EntityData.Tenants.Models.Category category)
        {
            if (category == null)
                return null;

            return new GetCategoryDTO
            {
                Id = category.Id,
                Type = category.Type,
                CreatedOn = category.CreatedOn,
            };
        }

        public static Dataservice.EntityData.Tenants.Models.Category MapToEntity(GetCategoryDTO catgory)
        {
            if (catgory == null)
                return null;

            return new Dataservice.EntityData.Tenants.Models.Category
            {
                Id = catgory.Id,
                Type = catgory.Type
            };

        }
    }

}
