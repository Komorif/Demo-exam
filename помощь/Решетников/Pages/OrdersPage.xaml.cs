using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using System.Collections.Generic;
using Решетников.Database;

namespace Решетников
{
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (var context = new ReshetnikovEntities1())
            {
                var orders = context.Orders.ToList();
                var orderViewModels = new List<OrderViewModel>();

                foreach (var order in orders)
                {
                    orderViewModels.Add(new OrderViewModel(order, context));
                }

                OrdersItemsControl.ItemsSource = orderViewModels;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    public class OrderViewModel
    {
        public string OrderInfo { get; set; }
        public string UserInfo { get; set; }
        public List<string> OrderItems { get; set; }

        public OrderViewModel(Orders order, ReshetnikovEntities1 context)
        {
            OrderInfo = $"Заказ №{order.OrderID}";

            
            var user = context.Users.FirstOrDefault(u => u.UserID == order.UserID);
            UserInfo = user != null ? $"Пользователь: {user.FullName} ({user.Username})" : "Пользователь: Неизвестен";

            
            OrderItems = new List<string>();
            var orderItems = context.OrderItems.Where(oi => oi.OrderID == order.OrderID).ToList();

            foreach (var orderItem in orderItems)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == orderItem.ProductID);
                if (product != null)
                {
                    OrderItems.Add($"{product.ProductName} - {orderItem.Quantity} шт. x {product.Price} руб");
                }
                else
                {
                    OrderItems.Add("Товар не найден");
                }
            }
        }
    }
}