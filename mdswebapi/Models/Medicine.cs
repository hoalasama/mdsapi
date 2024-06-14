using System;
using System.Collections.Generic;

namespace mdswebapi.Models;

public partial class Medicine
{
    public int MedId { get; set; }

    public string? MedName { get; set; }

    public string? MedDesc { get; set; }

    public int? MedDiscount { get; set; }

    public int? MedRemain { get; set; }

    public decimal? MedPrice { get; set; }

    public string? MedPicture { get; set; }

    public int? CateId { get; set; }

    public int? PharId { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Pharmacy? Phar { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
