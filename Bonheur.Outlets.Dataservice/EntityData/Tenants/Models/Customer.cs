using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }
    public int? ServiceId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }


    public virtual Service? Service { get; set; }

    public virtual ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
}
