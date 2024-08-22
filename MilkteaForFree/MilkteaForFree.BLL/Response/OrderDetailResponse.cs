using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Response
{
    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }

        public virtual Drink Drink { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
    }
}
