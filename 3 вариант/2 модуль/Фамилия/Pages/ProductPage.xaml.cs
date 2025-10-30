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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            Helpers.label.Content = "Страница товаров";
            ListViewItems.ItemsSource = Helpers.DB.Goods.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.frame.Navigate(new MainPage());
        }
    }
}
