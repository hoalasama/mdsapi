using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public string? CustomerId { get; set; }

    public int? MedId { get; set; }

    public string? ReviewContent { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Medicine? Med { get; set; }
}
