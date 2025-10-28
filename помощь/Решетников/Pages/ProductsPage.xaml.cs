using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using Решетников.Database;

namespace Решетников
{
    public partial class ProductsPage : Page
    {
        private List<Products> _allProducts;
        private string _userRole;

        public ProductsPage(string userRole = "Гость")
        {
            InitializeComponent();
            _userRole = userRole;

            
            FilterPanel.Visibility = (userRole == "Менеджер" || userRole == "Админ")
                ? Visibility.Visible : Visibility.Collapsed;

           
            if (_userRole == "Админ")
            {
                AddProductButton.Visibility = Visibility.Visible;
            }

            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var context = new ReshetnikovEntities1())
            {
                _allProducts = context.Products.ToList();
                ApplyFilters();

                
                var categories = _allProducts.Select(p => p.Category).Distinct().ToList();
                CategoryComboBox.ItemsSource = categories;
            }
        }

        private void ApplyFilters()
        {
            var filteredProducts = _allProducts.AsEnumerable();

            
            if (!string.IsNullOrEmpty(SearchTextBox?.Text))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.ProductName.ToLower().Contains(SearchTextBox.Text.ToLower()));
            }

            
            if (CategoryComboBox?.SelectedItem != null)
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.Category == CategoryComboBox.SelectedItem.ToString());
            }

            
            var productViewModels = filteredProducts
                .Select(p => new ProductViewModel(p, _userRole))
                .ToList();

            ProductsItemsControl.ItemsSource = productViewModels;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            CategoryComboBox.SelectedItem = null;
            ApplyFilters();
        }

       
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления товара будет реализована позже", "Информация",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int productId = (int)button.Tag;
            MessageBox.Show($"Редактирование товара ID: {productId}\nФункция будет реализована позже",
                          "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int productId = (int)button.Tag;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить товар ID: {productId}?",
                                       "Подтверждение удаления",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show($"Товар ID: {productId} удален\n(в реальном приложении здесь будет удаление из БД)",
                              "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string DiscountText { get; set; }
        public string FinalPriceText { get; set; }
        public Brush BackgroundColor { get; set; }
        public TextDecorationCollection PriceDecoration { get; set; }
        public Brush PriceColor { get; set; }
        public Visibility AdminButtonsVisibility { get; set; }

        public ProductViewModel(Products product, string userRole)
        {
            ProductId = product.ProductID;
            ProductName = product.ProductName;
            Category = product.Category;
            Price = product.Price;

            DiscountText = product.DiscountPercent.HasValue && product.DiscountPercent > 0 ?
                $"{product.DiscountPercent}%" : "0%";

            decimal finalPrice = product.DiscountPercent.HasValue ?
                Price * (1 - product.DiscountPercent.Value / 100) : Price;
            FinalPriceText = finalPrice.ToString("0 руб");

            BackgroundColor = Brushes.White;
            PriceDecoration = null;
            PriceColor = Brushes.Black;

            
            AdminButtonsVisibility = userRole == "Админ" ? Visibility.Visible : Visibility.Collapsed;

            if (product.DiscountPercent > 15)
            {
                BackgroundColor = Brushes.Orange; 
            }

            if (product.DiscountPercent > 0)
            {
                PriceDecoration = TextDecorations.Strikethrough;
                PriceColor = Brushes.Red;
            }
        }
    }
}