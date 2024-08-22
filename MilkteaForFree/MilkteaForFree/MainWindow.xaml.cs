using MilkteaForFree.BLL.Services;
using MilkteaForFree.DAL.Entities;
using System.Linq;
using System.Windows;

namespace MilkteaForFree
{//done
    public partial class MainWindow : Window
    {
        private UserService _service = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            User? acc = _service.Authenticate(username, password);

            if (acc != null)
            {
                //MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Navigate to the next window, e.g., the Menu window
                Menu m = new();
                m.Account = acc;
                m.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
