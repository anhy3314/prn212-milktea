using MilkteaForFree.DAL.Entities;
using System.Linq;
using System.Windows;

namespace MilkteaForFree
{//done
    public partial class MainWindow : Window
    {
        private MilkTeaContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new MilkTeaContext(); // Initialize the context
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            // Simple plain text password check (not secure for production)
            var user = _context.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                //MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Navigate to the next window, e.g., the Menu window
                var menuWindow = new Menu();
                menuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
