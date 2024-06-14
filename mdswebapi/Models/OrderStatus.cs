using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class OrderStatus
{
    public int OsId { get; set; }

    public string? OsDesc { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
