using Microsoft.EntityFrameworkCore;
using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.DAL.Repositories
{
    public class OrderDetailRepository
    {
        public List<OrderDetail> GetListOrderDetailByOrderId(int id)
        {
            using (var context = new MilkTeaContext())
            {
                return context.OrderDetails.Where( od => od.OrderId.Equals(id))
                    .Include(od => od.Drink).ToList();
            }
        }
    }
}
