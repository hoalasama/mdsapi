using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OsId { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? OrderPlacedAt { get; set; }

    public DateOnly? OrderDeliveredAt { get; set; }

    public decimal? ShippingFees { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus? Os { get; set; }
}
