using System;
using System.Collections.Generic;

namespace SprintHEMone.Models;

public partial class OrderList
{
    public int OrderId { get; set; }

    public string? Email { get; set; }

    public string? ItemId { get; set; }

    public decimal Price { get; set; }

    public virtual Customer? EmailNavigation { get; set; }

    public virtual Item? Item { get; set; }
}
