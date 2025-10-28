using System.Windows;
using System.Windows.Controls;

namespace Решетников
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new AutorizationPage());
        }
    }
}