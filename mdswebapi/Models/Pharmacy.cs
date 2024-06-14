using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Pharmacy
{
    public int PharId { get; set; }

    public string? PharLogin { get; set; }

    public string? PharPass { get; set; }

    public string? PharName { get; set; }

    public string? PharPhone { get; set; }

    public string? PharEmail { get; set; }

    public string? PharAddress { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
