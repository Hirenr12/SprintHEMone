using System;
using System.Collections.Generic;

namespace SprintHEMone.Models;

public partial class Item
{
    public string ItemId { get; set; } = null!;

    public string Itemname { get; set; } = null!;

    public decimal ItemCost { get; set; }

    public string ItemDetails { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();
}
