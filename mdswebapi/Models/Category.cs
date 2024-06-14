using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string? CateName { get; set; }

    public string? CateDesc { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
