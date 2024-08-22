using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MilkteaForFree.BLL.Response;
using static MilkteaForFree.Menu;

namespace MilkteaForFree
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public ObservableCollection<Drink> Drinks { get; set; }
        public ObservableCollection<Order> OrderHistory { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new ObservableCollection<OrderDetail>();

        private List<MilkteaForFree.DAL.Entities.Order> orders;

        public Menu()
        {
            InitializeComponent();
            DataContext = this;

            InitializeOrderHistory();

            Drinks = new ObservableCollection<Drink>();
            OrderHistory = new ObservableCollection<Order>();

            //Set default value for date picker
            FromDatePicker.SelectedDate = DateTime.Now;
            ToDatePicker.SelectedDate = DateTime.Now;
            FromDatePicker.DisplayDateEnd = ToDatePicker.DisplayDate;
            ToDatePicker.DisplayDateEnd = DateTime.Now;

            //LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Populate Drinks and OrderHistory collections with dummy data
            Drinks.Add(new Drink { DrinkId = 1, CategoryID = 1, Name = "Classic Milk Tea", UnitPrice = 35000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 2, CategoryID = 1, Name = "Thai Milk Tea", UnitPrice = 40000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 3, CategoryID = 2, Name = "Matcha Latte", UnitPrice = 45000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 4, CategoryID = 2, Name = "Taro Milk Tea", UnitPrice = 42000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 5, CategoryID = 3, Name = "Oolong Tea", UnitPrice = 30000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 6, CategoryID = 3, Name = "Jasmine Tea", UnitPrice = 32000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 7, CategoryID = 4, Name = "Coffee Milk Tea", UnitPrice = 38000m, DrinkStatus = 1 });
            Drinks.Add(new Drink { DrinkId = 8, CategoryID = 4, Name = "Black Sugar Milk Tea", UnitPrice = 40000m, DrinkStatus = 1 });

            //OrderHistory.Add(new Order
            //{
            //    OrderID = 1,
            //    UserID = 2,
            //    OrderDate = new DateTime(2024, 8, 1, 10, 30, 0),
            //    Total = 110000m,
            //    OrderStatus = "Payed",
            //    OrderDetails = new ObservableCollection<OrderDetail>
            //    {
            //        new OrderDetail { OrderDetailID = 1, OrderID = 1, DrinkID = 1, UnitPrice = 35000m, Quantity = 2, Discount = 0 },
            //        new OrderDetail { OrderDetailID = 2, OrderID = 1, DrinkID = 2, UnitPrice = 40000m, Quantity = 1, Discount = 0 }
            //    }
            //});
            // Add other orders similarly
        }

        public class Drink
        {
            public int DrinkId { get; set; }
            public int CategoryID { get; set; }
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
            public int DrinkStatus { get; set; }
        }

        public class Order
        {
            public int OrderID { get; set; }
            public int UserID { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal? Total { get; set; }
            public string OrderStatus { get; set; }
            public ObservableCollection<OrderDetail> OrderDetails { get; set; }
        }

        public class OrderDetail
        {
            public int OrderDetailID { get; set; }
            public int OrderID { get; set; }
            public int DrinkID { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal Discount { get; set; }
            public decimal CalculateTotalPrice()
            {
                return UnitPrice * Quantity * (1 - Discount);
            }
        }

        private void AddMilkTeaButton_Click(object sender, RoutedEventArgs e)
        {
            string name = MilkTeaNameTextBox.Text;
            if (decimal.TryParse(MilkTeaPriceTextBox.Text, out decimal price))
            {
                string selectedType = (MilkTeaTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Add the new milk tea to the list (implement logic to save this data as needed)
                Drinks.Add(new Drink
                {
                    DrinkId = Drinks.Count + 1, // Example ID assignment
                    CategoryID = 1, // Example category ID
                    Name = name,
                    UnitPrice = price,
                    DrinkStatus = 1 // Example status
                });

                MessageBox.Show("Milk Tea added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MilkTeaNameTextBox.Clear();
                MilkTeaPriceTextBox.Clear();
                MilkTeaTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Invalid price entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the button that was clicked
            Button button = sender as Button;
            if (button == null)
                return;

            // Retrieve the item data (assuming it's bound to the button's DataContext)
            var item = button.DataContext as Drink;
            if (item == null)
                return;

            // Add item to cart
            var existingOrderDetail = OrderDetails.OfType<OrderDetail>()
                .FirstOrDefault(od => od.DrinkID == item.DrinkId);

            if (existingOrderDetail != null)
            {
                // If item already exists in the cart, increase the quantity
                existingOrderDetail.Quantity++;
            }
            else
            {
                // Otherwise, create a new OrderDetail and add it to the cart
                var newOrderDetail = new OrderDetail
                {
                    OrderDetailID = OrderDetails.Count + 1, // Example ID assignment
                    OrderID = 1, // Example OrderID
                    DrinkID = item.DrinkId,
                    UnitPrice = item.UnitPrice,
                    Quantity = 1,
                    Discount = 0 // Example discount
                };

                OrderDetails.Add(newOrderDetail);
            }

            // Update the total price
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = OrderDetails.Sum(od => od.CalculateTotalPrice());
            TotalTextBlock.Text = total.ToString("C");
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            // For demonstration purposes, show a message box with a summary of the order
            decimal total = OrderDetails.Sum(od => od.CalculateTotalPrice());
            MessageBox.Show($"Proceeding to checkout. Total amount: {total:C}", "Checkout", MessageBoxButton.OK, MessageBoxImage.Information);

            // Here you would typically handle the checkout logic, such as saving the order to the database or processing the payment
        }

        private void SearchOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var fromDate = FromDatePicker.SelectedDate;
            var toDate = ToDatePicker.SelectedDate;

            OrderDetailListView.SelectedIndex = 0;
            OrderDetailListView.ItemsSource = null;

            if (fromDate == null || toDate == null)
            {
                MessageBox.Show("Please select both start and end dates.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ensure the end date is not earlier than the start date
            if (toDate < fromDate)
            {
                MessageBox.Show("End date cannot be earlier than start date.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Filter orders by the selected date range
            var filteredOrders = orders.Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate).ToList();

            // Update the ListView to display the filtered orders
            OrderHistoryListView.ItemsSource = filteredOrders;
        }

        private void InitializeOrderHistory()
        {
            OrderService orderService = new OrderService();
            orders = orderService.GetList().ToList();
            //MessageBox.Show("" + orders.Count, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            for (int i = 0; i < orders.Count; i++)
            {
                MilkteaForFree.DAL.Entities.Order item = orders[i];
                Order order = new Order
                {
                    OrderID = item.OrderId,
                    OrderDate = item.OrderDate,
                    Total = item.Total,
                    UserID = 2,
                };
                //OrderHistory.Add(order);
                
            }
            OrderHistoryListView.ItemsSource = orders;
        }

        private void FromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ToDatePicker.DisplayDateStart = FromDatePicker.SelectedDate;
        }

        private void ToDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FromDatePicker.DisplayDateEnd = ToDatePicker.SelectedDate;
        }

        public class OrderDetailView()
        {
            public string DrinkName { get; set; }
            public double Discount { get; set; }
            public decimal? UnitPrice { get; set; }
            public int Quantity { get; set; }
        }

        private void OrderHistoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MilkteaForFree.DAL.Entities.Order item = (MilkteaForFree.DAL.Entities.Order) OrderHistoryListView.SelectedItem;
            //MessageBox.Show("" + item.OrderId, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            List<OrderDetailView> listOrderDetailViews = new List<OrderDetailView>();

            OrderService orderService = new OrderService();
            try
            {
                if (OrderHistoryListView.SelectedItem == null)
                {
                    OrderDetailListView.SelectedIndex = 0;
                    OrderDetailListView.Items.Clear();
                    return;
                }


                List<OrderDetailResponse> listItems = orderService.GetOrderDetailsByOrderId(item.OrderId).ToList();

                foreach (OrderDetailResponse od in listItems)
                {
                    listOrderDetailViews.Add(new OrderDetailView
                    {
                        DrinkName = od.Drink.DrinkName,
                        Discount = od.Discount,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    });
                }
            }
            catch (Exception ex)
            {
                return;
            }
            

            
            OrderDetailListView.ItemsSource = listOrderDetailViews;
        }
    }
}
