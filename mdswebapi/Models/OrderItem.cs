using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? MedId { get; set; }

    public int? ItemQuantity { get; set; }

    public virtual Medicine? Med { get; set; }

    public virtual Order? Order { get; set; }
}
