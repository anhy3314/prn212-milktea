using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Response
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal? Total { get; set; }

        public string? OrderStatus { get; set; }

        public virtual User? User { get; set; }
    }
}
