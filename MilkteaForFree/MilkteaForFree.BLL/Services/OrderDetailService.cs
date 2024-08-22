using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Services
{
    public class OrderDetailService
    {
        private OrderDetailRepository _repo = new();
        public List<OrderDetail> GetAllOrderDetails() => _repo.GetAllOrderDetails();
        public List<OrderDetail> GetAllOrderDetailsWithDrink() => _repo.GetAllOrderDetailsWithDrink();
        public List<OrderDetail> GetAllOfAOrder(int currOrderId) => _repo.GetAllOfAOrder(currOrderId);

        public OrderDetail? GetOrder(int id) => _repo.GetOrder(id);
        public void AddOrderDetails(OrderDetail orderDetail) => _repo.AddOrderDetails(orderDetail);
        public void UpdateOrderDetails(OrderDetail orderDetail) => _repo.UpdateOrderDetails(orderDetail);
        public void DeleteOrderDetails(OrderDetail orderDetail) => _repo?.DeleteOrderDetails(orderDetail);

        public void DeleteAllOrderDetailsOfCurrOrder(int currOrderId) => _repo.DeleteAllOrderDetailsOfCurrOrder(currOrderId);
        public decimal GetPrice(int currOrderId) => _repo.GetPrice(currOrderId);

        public int CountDetailId() => _repo.CountId();

        public OrderDetail? CheckDetailOfCurrOrderByDrinkId(int currOrderId, int drinkId) => _repo.CheckDetailOfCurrOrderByDrinkId(currOrderId, drinkId);

    }
}
