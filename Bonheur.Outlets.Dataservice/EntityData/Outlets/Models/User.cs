using System;
using System.Collections.Generic;

namespace Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;

public partial class User
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
