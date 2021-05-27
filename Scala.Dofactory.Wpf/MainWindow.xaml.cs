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


namespace Scala.Dofactory.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            winCustomers win = new winCustomers();
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            winSupplier win = new winSupplier();
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            winProducts win = new winProducts();
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }
    }
}
