using System;
using System.Collections.Generic;

namespace SprintHEMone.Models;

public partial class Customer
{
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();
}
