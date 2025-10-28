using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Решетников
{
    public partial class MainPage : Page
    {
        private string _userRole;
        private string _fullName;

        public MainPage(string userRole, string fullName = "Гость")
        {
            InitializeComponent();
            _userRole = userRole;
            _fullName = fullName;

           
            UserInfoText.Text = $"ФИО: {_fullName}";
            RoleInfoText.Text = $"Роль: {_userRole}";

            
            if (_userRole == "Менеджер" || _userRole == "Админ")
            {
                OrdersButton.Visibility = Visibility.Visible;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutorizationPage());
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new ProductsPage(_userRole));
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new OrdersPage());
        }
    }
}