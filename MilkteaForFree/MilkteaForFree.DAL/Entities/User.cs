using System;
using System.Collections.Generic;

namespace MilkteaForFree.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public int UserRole { get; set; }

    public int Bonus { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
