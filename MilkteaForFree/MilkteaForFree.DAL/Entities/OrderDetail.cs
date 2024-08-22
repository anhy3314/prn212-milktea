using System;
using System.Collections.Generic;

namespace MilkteaForFree.DAL.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int DrinkId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Discount { get; set; }

    public virtual Drink Drink { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
