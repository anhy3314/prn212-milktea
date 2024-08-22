using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;

public class OrderService
{
    private readonly MilkTeaContext _context;
    private OrderRepository _repo = new();

    public OrderService()
    {
        _context = new MilkTeaContext();
    }

    public void GetAllOrder() => _repo.GetAll();
    public Order? GetOrderById(int id) => _repo.GetOrder(id);
    public void AddOrder(Order newOrder) => _repo.AddOrder(newOrder);

    public void UpdateOrder(Order newOrder) => _repo.UpdateOrder(newOrder);
    public void DeleteOrder(Order newOrder) => _repo.DeleteOrder(newOrder);
    public int CountOrderID() => _repo.CountOrderID();
    public void UpdateTotalPrice(int id, decimal x) => _repo.UpdateTotalPrice(id, x);
    public List<Order> GetOrders(DateTime from, DateTime to) => _repo.GetOrders(from, to);

    public void AddOrder(Order order, List<OrderDetail> orderDetails)
    {
        using (var context = new YourDbContext())
        {
            // Add the order
            context.Orders.Add(order);

            // Add the order details
            foreach (var detail in orderDetails)
            {
                context.OrderDetails.Add(detail);
            }

            // Save changes
            context.SaveChanges();
        }
    }

}
