using System;
using System.Collections.Generic;

namespace SprintHEMone.Models;

public partial class Cart
{
    public string CartId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Customer EmailNavigation { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
