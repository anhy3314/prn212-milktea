using MilkteaForFree.BLL.Response;
using MilkteaForFree.BLL.Utils;
using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;

public class OrderService
{
    private readonly MilkTeaContext _context;

    private readonly OrderRepository _orderRepository;
    private readonly OrderDetailRepository _orderDetailRepository;

    public OrderService()
    {
        _orderRepository = new OrderRepository();
        _orderDetailRepository = new OrderDetailRepository();
        _context = new MilkTeaContext();
    }

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

    public IEnumerable<Order> GetOrders(DateTime fromDate, DateTime toDate)
    {
        return _context.Orders
            .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
            .ToList();
    }

    public IEnumerable<Order> GetList()
    {
        return _orderRepository.GetListOrder();
    }

    public IEnumerable<OrderDetailResponse> GetOrderDetailsByOrderId(int id)
    {
        List<OrderDetailResponse> result = new();

        try
        {
            if (id == 0)
            {
                throw new InvalidDataException("Id invalid");
            }

            result = ConvertFunction.ConvertListToList<OrderDetailResponse, OrderDetail>(_orderDetailRepository.GetListOrderDetailByOrderId(id));

        }
        catch (Exception ex)
        {
            return result;
        }

        return result;
    }
}
