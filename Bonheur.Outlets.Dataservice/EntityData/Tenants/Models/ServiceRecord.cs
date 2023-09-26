using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;

public partial class ServiceRecord
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int? CustomerId { get; set; }

    public int StaffId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Service Service { get; set; } = null!;
}
