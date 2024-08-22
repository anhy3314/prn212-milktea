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
        private MilkTeaContext _context;
        public List<Order> GetAll()
        {
            _context = new();
            return _context.Orders.ToList();
        }

        public Order? GetOrder(int id)
        {
            _context = new();
            return _context.Orders.FirstOrDefault(x => x.OrderId == id);
        }
        public void AddOrder(Order newOrder)
        {
            _context = new();
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context = new();
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context = new();
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public int CountOrderID()
        {
            _context = new();
            return _context.Orders.Count();
        }

        public void UpdateTotalPrice(int id, decimal x)
        {
            _context = new();
            Order o = _context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (o != null)
            {
                o.Total = x;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("This order isn't exit");
            }
        }

        public List<Order> GetOrders(DateTime? from, DateTime? to)
        {
            _context = new();
            return _context.Orders
                    .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                    .ToList();
        }

        public int CountOrderByDate(DateTime? start, DateTime? end)
        {
            _context = new();
            return _context.Orders.Where(x => x.OrderDate >= start && x.OrderDate <= end).Count();
        }

        public decimal CountTotalByDate(DateTime? start, DateTime? end)
        {
            _context = new();
            decimal total = 0;
            var list = _context.Orders.Where(x => x.OrderDate >= start && x.OrderDate <= end).ToList();
            foreach (var x in list)
            {
                total += x.Total ?? 0;
            }
            return total;
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
