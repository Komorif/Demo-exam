using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Фамилия.Helper;

namespace Фамилия.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            Helpers.label.Content = "Страница авторизации";
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.label.Content = "Главная страница\nГость";
            Helpers.frame.Navigate(new MainPage());
        }

        private void LoginButton_Click_1(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            var client = Helpers.DB.Users.FirstOrDefault(u => u.Login == LoginTextBox.Text);

            if (client == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            if (LoginTextBox.Text != client.Password.Replace(" ", ""))
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }


            MessageBox.Show("Успешный вход");
            Helpers.CurrentUser = client;
            Helpers.label.Content = $"Главная страница\n{client.LastName} {client.Name} {client.MiddleName}\n{client.Roles.Name}";
            Helpers.frame.Navigate(new MainPage());
        }
    }
}
