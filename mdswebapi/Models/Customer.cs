using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Customer : IdentityUser
{

    public string? CustomerName { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerAddress { get; set; }

    public string? CustomerLogin { get; set; }

    public string? CustomerPassword { get; set; }
    public int? PharmacyId { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
