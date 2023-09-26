using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;

public partial class Shop
{
    public int Id { get; set; }

    public string ShopName { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? Address { get; set; }

    public string ConnectionString { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
