using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
