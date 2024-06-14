using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class CartDetail
{
    public int CdId { get; set; }

    public int? CartId { get; set; }

    public int? MedId { get; set; }

    public int? Quantity { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Medicine? Med { get; set; }
}
