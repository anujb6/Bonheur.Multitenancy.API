using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Database
{
    public interface IDatabaseHelper
    {
        Task<bool> CreateShopDatabaseAsync(string connectionString, string shopName);
    }
}
