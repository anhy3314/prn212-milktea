using MilkteaForFree.BLL.Services;
using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MilkteaForFree.Menu;

namespace MilkteaForFree
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private DrinkService drinkService = new();
        private CategoryService categoryService = new();
        private OrderService orderService = new();
        private OrderDetailService detailService = new();
        private int tempOrderId = new();
        private bool payFlag = false;
        public Drink EditedDrinks { get; set; }
        public User Account { get; set; }
        public ObservableCollection<Drink> Drinks { get; set; }
        public ObservableCollection<Order> OrderHistory { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new ObservableCollection<OrderDetail>();

        public Menu()
        {
            InitializeComponent();
            DataContext = this;

            Drinks = new ObservableCollection<Drink>();
            OrderHistory = new ObservableCollection<Order>();
            OrderDetails = new ObservableCollection<OrderDetail>();
        }

        public void FillDrinkDataGrid()
        {
            try
            {
                // Fetch the drink menu from the service
                var drinkMenu = drinkService.GetAllDrinks();

                // Check if the retrieved data is null
                if (drinkMenu == null)
                {
                    throw new Exception("The drink menu returned null.");
                }

                // Update the ListView's item source
                TeaListView.ItemsSource = null; // Clear the previous binding
                TeaListView.ItemsSource = drinkMenu; // Set the new data source
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the drink menu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void FillCartDataGrid()
        {
            try
            {
                // Fetch the drink menu from the service
                var cartMenu = detailService.GetAllOfAOrder(tempOrderId);

                // Check if the retrieved data is null
                if (cartMenu == null)
                {
                    throw new Exception("Cart is empty, are you want to put some drinks!");
                }

                CartListView.ItemsSource = null;
                CartListView.ItemsSource = cartMenu; // Set the new data source
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the drink menu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AddMilkTeaButton_Click(object sender, RoutedEventArgs e)
        {
            //string name = MilkTeaNameTextBox.Text;
            //if (decimal.TryParse(MilkTeaPriceTextBox.Text, out decimal price))
            //{
            //    string selectedType = (MilkTeaTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            //    // Add the new milk tea to the list (implement logic to save this data as needed)
            //    Drinks.Add(new Drink
            //    {
            //        DrinkId = Drinks.Count + 1, // Example ID assignment
            //        CategoryID = 1, // Example category ID
            //        Name = name,
            //        UnitPrice = price,
            //        DrinkStatus = 1 // Example status
            //    });

            //    MessageBox.Show("Milk Tea added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //    MilkTeaNameTextBox.Clear();
            //    MilkTeaPriceTextBox.Clear();
            //    MilkTeaTypeComboBox.SelectedIndex = -1;
            //}
            //else
            //{
            //    MessageBox.Show("Invalid price entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (payFlag == false)
                {
                    MessageBox.Show("Please create a bill before add to cart", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // Retrieve the button that was clicked
                Button button = sender as Button;
                if (button == null)
                    throw new InvalidOperationException("Button was null");

                // retrieve the item data (assuming it's bound to the button's DataContext)
                var item = button.DataContext as Drink;
                if (item == null)
                    throw new InvalidOperationException("Drink item is null");

                // Add item to cart
                var existingOrderDetail = detailService.CheckDetailOfCurrOrderByDrinkId(tempOrderId, item.DrinkId);

                if (existingOrderDetail != null)
                {
                    existingOrderDetail.Quantity++;
                    detailService.UpdateOrderDetails(existingOrderDetail);
                }
                else
                {
                    int tmpId = detailService.CountDetailId() + 1;
                    while (detailService.GetOrder(tmpId) != null)
                    {
                        tmpId++;
                    }

                    // Otherwise, create a new OrderDetail and add it to the cart
                    OrderDetail x = new OrderDetail
                    {
                        OrderDetailId = tmpId,
                        OrderId = tempOrderId,
                        DrinkId = item.DrinkId,
                        UnitPrice = item.UnitPrice,
                        Quantity = 1,
                        Discount = 0
                    };

                    detailService.AddOrderDetails(x);
                }

                // Update the total price
                UpdateTotal();
                FillCartDataGrid();
            }
            catch (Exception ex)
            {
                // Check if there is an inner exception
                var baseException = ex.GetBaseException();
                MessageBox.Show($"Error adding to cart: {baseException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTotal()
        {
            decimal total = detailService.GetPrice(tempOrderId);
            TotalTextBlock.Text = total.ToString("C");
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            // For demonstration purposes, show a message box with a summary of the order
            decimal total = detailService.GetPrice(tempOrderId);
            MessageBoxResult answer = MessageBox.Show($"Proceeding to checkout. Total amount: {total:C}", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                payFlag = false;
                CreateBillButton.IsEnabled = true;
                orderService.UpdateTotalPrice(tempOrderId, total);
                tempOrderId = orderService.CountOrderID() + 1;
                FillDataGrid();
            }

            // Here you would typically handle the checkout logic, such as saving the order to the database or processing the payment
        }

        private void SearchOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            //var fromDate = FromDatePicker.SelectedDate;
            //var toDate = ToDatePicker.SelectedDate;

            //if (fromDate == null || toDate == null)
            //{
            //    MessageBox.Show("Please select both start and end dates.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //// Ensure the end date is not earlier than the start date
            //if (toDate < fromDate)
            //{
            //    MessageBox.Show("End date cannot be earlier than start date.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //// Filter orders by the selected date range
            //var filteredOrders = OrderHistory.Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate).ToList();

            //// Update the ListView to display the filtered orders
            //OrderHistoryListView.ItemsSource = filteredOrders;
        }

        private void OrderTab_Loaded(object sender, RoutedEventArgs e)
        {
            CancelButton.IsEnabled = false;
            FillDataGrid();
        }

        private void CreateBillButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A Bill is ready!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);

            payFlag = true;
            CreateBillButton.IsEnabled = false;
            CancelButton.IsEnabled = true;

            tempOrderId = orderService.CountOrderID() + 1;
            int orderId = tempOrderId;
            int userId = Account.UserId;
            DateTime orderDate = DateTime.Now;
            decimal? total = 0;
            string? status = "1";


            Order x = new();
            x.OrderId = tempOrderId;
            x.UserId = userId;
            x.OrderDate = orderDate;
            x.Total = total;
            x.OrderStatus = status;

            orderService.AddOrder(x);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you really want to cancel?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                payFlag = false;
                CreateBillButton.IsEnabled = true;

                var deletedOrder = orderService.GetOrderById(tempOrderId);
                if (deletedOrder != null)
                {
                    orderService.DeleteOrder(deletedOrder);
                }

                var deletedOrderDetail = detailService.GetAllOfAOrder(tempOrderId);
                if (deletedOrderDetail != null)
                {
                    detailService.DeleteAllOrderDetailsOfCurrOrder(tempOrderId);
                }
            }

            FillDataGrid();
        }

        public void FillDataGrid()
        {
            FillDrinkDataGrid();
            FillCartDataGrid();
        }

        private void DeleteFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve the button that was clicked
                Button? button = sender as Button;
                if (button == null)
                    throw new InvalidOperationException("Button was null");

                // retrieve the item data (assuming it's bound to the button's DataContext)
                var item = button.DataContext as OrderDetail;
                if (item == null)
                    throw new InvalidOperationException("OrderDetail item is null");

                // Delete item to cart
                OrderDetail x = new();
                x.OrderDetailId = item.OrderDetailId;
                x.OrderId = item.OrderId;
                x.DrinkId = item.DrinkId;
                x.UnitPrice = item.UnitPrice;
                x.Quantity = item.Quantity;
                x.Discount = item.Discount;
                //x = item;

                detailService.DeleteOrderDetails(x);

                // Update the total price
                UpdateTotal();
                FillCartDataGrid();
            }
            catch (Exception ex)
            {
                // Check if there is an inner exception
                var baseException = ex.GetBaseException();
                MessageBox.Show($"Error adding to cart: {baseException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CountId()
        {
            tempOrderId = orderService.CountOrderID() + 1;
        }

        public void FillDrinkManagementDataGrid()
        {
            MilkTeaDataGrid.ItemsSource = null;
            MilkTeaDataGrid.ItemsSource = drinkService.GetAllDrinksWithCategories();
        }

        public void FillCategoryCombobox()
        {
            MilkTeaTypeComboBox.ItemsSource = null;
            MilkTeaTypeComboBox.ItemsSource = categoryService.GetAllCategories();
            MilkTeaTypeComboBox.DisplayMemberPath = "CategoryName";
            MilkTeaTypeComboBox.SelectedValuePath = "CategoryId";
        }

        private void DrinkManagementTab_Loaded(object sender, RoutedEventArgs e)
        {
            FillAllInDrinkManagement();
        }

        public void FillAllInDrinkManagement()
        {
            FillDrinkManagementDataGrid();
            FillCategoryCombobox();
            //FillElemetInDrinkManage();
        }

        private void AddDrinkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you really want to add new drink?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {

                int tmpId = drinkService.CountDrink() + 1;
                while (drinkService.GetById(tmpId) != null)
                {
                    tmpId++;
                }
                Drink x = new();
                x.DrinkId = tmpId;
                x.DrinkName = MilkTeaNameTextBox.Text;
                x.UnitPrice = decimal.Parse(MilkTeaPriceTextBox.Text);
                x.CategoryId = (int)MilkTeaTypeComboBox.SelectedValue;
                x.DrinkStatus = 1;

                drinkService.AddDrink(x);
            }

            FillDrinkManagementDataGrid();
        }

        public void FillElemetInDrinkManage()
        {
            Drink? selected = MilkTeaDataGrid.SelectedItem as Drink;
            MilkTeaNameTextBox.Text = selected.DrinkName;
            MilkTeaPriceTextBox.Text = selected.UnitPrice.ToString();
            MilkTeaTypeComboBox.SelectedValue = selected.CategoryId;
        }

        private void UpdateDrinkButton_Click(object sender, RoutedEventArgs e)
        {
            Drink? selected = MilkTeaDataGrid.SelectedItem as Drink;
            if (selected == null)
            {
                MessageBox.Show("Please select a row (air con) before editing", "Select?", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("You Select: " + selected.ToString());

            Drink x = new();
            x.DrinkId = selected.DrinkId;
            x.DrinkName = MilkTeaNameTextBox.Text;
            x.UnitPrice = decimal.Parse(MilkTeaPriceTextBox.Text);
            x.CategoryId = (int)MilkTeaTypeComboBox.SelectedValue;
            x.DrinkStatus = 1;

            drinkService.UpdateDrink(x);
            FillDrinkManagementDataGrid();
        }
    }
}
