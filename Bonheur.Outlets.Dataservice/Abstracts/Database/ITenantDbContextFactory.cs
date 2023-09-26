using Bonheur.Outlets.Dataservice.EntityData.Tenants;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Database
{
    public interface ITenantDbContextFactory
    {
        TenantsContext Create(IHttpContextAccessor httpContextAccessor);
    }

}
