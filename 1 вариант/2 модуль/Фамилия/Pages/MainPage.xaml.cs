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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            if (Helpers.CurrentUser == null)
            {
                Helpers.label.Content = "Главная страница\nГость";
                AuthButton.Visibility = Visibility.Visible;
            }
 
            else if (Helpers.CurrentUser.Roles.Name == "Менеджер" || Helpers.CurrentUser.Roles.Name == "Админ")
            {
                LogoutButton.Visibility = Visibility.Visible;
                OrdersButton.Visibility = Visibility.Visible;
            }

            else
            {
                LogoutButton.Visibility = Visibility.Visible;
            }

            if (Helpers.CurrentUser != null)
            {
                Helpers.label.Content = $"Главная страница\n{Helpers.CurrentUser.LastName} {Helpers.CurrentUser.Name} {Helpers.CurrentUser.MiddleName}\n{Helpers.CurrentUser.Roles.Name}";
            }
        }

        // Кнопка (Выход из аккаунта)
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.label.Content = "Страница авторизации";
            Helpers.frame.Navigate(new AuthorizationPage());
            Helpers.CurrentUser = null;
        }

        // Кнопка (Авторизироватсья)
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.label.Content = "Страница авторизации";
            Helpers.frame.Navigate(new AuthorizationPage());
            Helpers.CurrentUser = null;
        }


        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.label.Content = "Страница продуктов";
            Helpers.frame.Navigate(new ProductPage());
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.label.Content = "Страница заказов";
            Helpers.frame.Navigate(new OrderPage());
        }
    }
}
