using Microsoft.EntityFrameworkCore;
using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.DAL.Repositories
{
    public class OrderRepository
    {
        
        public void AddOrder(Order newOrder)
        {
            using (var context = new YourDbContext())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }
        }

        public List<Order> GetOrders(DateTime from, DateTime to)
        {
            using (var context = new YourDbContext())
            {
                return context.Orders
                    .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                    .Include(o => o.OrderDetails) // Include related OrderDetails
                    .ToList();
            }
        }

        public List<Order> GetListOrder()
        {
            using (var context = new MilkTeaContext())
            {
                return context.Orders.Include(o => o.OrderDetails).ToList();
            }
        }
    }

}
