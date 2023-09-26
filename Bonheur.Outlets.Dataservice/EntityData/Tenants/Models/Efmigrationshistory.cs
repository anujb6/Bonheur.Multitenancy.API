using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;

public partial class Efmigrationshistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
