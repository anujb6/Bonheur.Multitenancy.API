using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants
{
    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly IShopsHelper _shopsHelper;
        public TenantDbContextFactory(IShopsHelper shopsHelper)
        {
            _shopsHelper = shopsHelper;
        }

        public TenantsContext Create(IHttpContextAccessor httpContextAccessor)
        {
            var tenantConnectionString = _shopsHelper.GetShopConnectionString(httpContextAccessor);
            var optionsBuilder = new DbContextOptionsBuilder<TenantsContext>()
                .UseMySql(tenantConnectionString, ServerVersion.Parse("8.0.32-mysql"));

            return new TenantsContext(optionsBuilder.Options);
        }
    }
}
