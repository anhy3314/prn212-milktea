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
        private MilkTeaContext _context;
        public List<OrderDetail> GetAllOrderDetails()
        {
            _context = new MilkTeaContext();
            return _context.OrderDetails.ToList();
        }

        public List<OrderDetail> GetAllOrderDetailsWithDrink()
        {
            _context = new MilkTeaContext();
            return _context.OrderDetails.Include("Drink").ToList();
        }

        public List<OrderDetail> GetAllOfAOrder(int currOrderId)
        {
            _context = new MilkTeaContext();
            return _context.OrderDetails.Include("Drink").Where(x => x.OrderId == currOrderId).ToList();
        }

        public void AddOrderDetails(OrderDetail orderDetail)
        {
            _context = new();
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public OrderDetail? GetOrder(int id)
        {
            _context = new();
            return _context.OrderDetails.FirstOrDefault(x => x.OrderDetailId == id);
        }

        public void UpdateOrderDetails(OrderDetail orderDetail)
        {
            _context = new();
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public void DeleteOrderDetails(OrderDetail orderDetail)
        {
            _context = new();
            _context.OrderDetails.Remove(orderDetail);
            _context.SaveChanges();
        }

        public void DeleteAllOrderDetailsOfCurrOrder(int currOrderId)
        {
            var orderDetailList = GetAllOfAOrder(currOrderId);
            if (orderDetailList != null)
            {
                foreach (var orderDetail in orderDetailList)
                {
                    _context = new();
                    _context.OrderDetails.Remove(orderDetail);
                    _context.SaveChanges();
                }
            }
        }

        public decimal GetPrice(int currOrderId)
        {
            _context = new MilkTeaContext();
            decimal total = 0;
            var list = GetAllOfAOrder(currOrderId);
            foreach (var item in list)
            {
                total += (item.UnitPrice * item.Quantity);
            }
            return total;
        }

        public int CountId()
        {
            _context = new();
            return _context.OrderDetails.Count();
        }

        public OrderDetail? CheckDetailOfCurrOrderByDrinkId(int currOrderId, int drinkId)
        {
            _context = new();
            return _context.OrderDetails.Where(x => x.OrderId == currOrderId && x.DrinkId == drinkId).FirstOrDefault();
        }

        public List<OrderDetail> GetListOrderDetailByOrderId(int id)
        {
            using (var context = new MilkTeaContext())
            {
                return context.OrderDetails.Where(od => od.OrderId.Equals(id))
                    .Include(od => od.Drink).ToList();
            }
        }
    }
}
