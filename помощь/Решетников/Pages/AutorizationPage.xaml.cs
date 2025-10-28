using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using Решетников.Database;

namespace Решетников
{
    public partial class AutorizationPage : Page
    {
        public AutorizationPage()
        {
            InitializeComponent();
            LoginButton.Click += LoginButton_Click;
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage("Гость"));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
    
            using (var context = new ReshetnikovEntities1())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == login && u.PasswordHash == password);

                if (user != null)
                {
                    string roleName = user.Roles.RoleName;
                    string fullName = user.FullName;

                    NavigationService.Navigate(new MainPage(roleName, fullName));
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}