using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Scala.Dofactory.Core.Entities;
using Scala.Dofactory.Core.Services;

namespace Scala.Dofactory.Wpf
{
    /// <summary>
    /// Interaction logic for winProducts.xaml
    /// </summary>
    public partial class winProducts : Window
    {
        ProductService productService;
        bool isNew;
        public winProducts()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            productService = new ProductService();
            ViewLeft();
            PopulateSuppliers();
            PopulateProducts();
        }
        private void ViewLeft()
        {
            grpProducts.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnProductSave.Visibility = Visibility.Hidden;
            btnProductCancel.Visibility = Visibility.Hidden;
        }
        private void ViewRight()
        {
            grpProducts.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnProductSave.Visibility = Visibility.Visible;
            btnProductCancel.Visibility = Visibility.Visible;
        }
        private void ClearControls()
        {
            txtProductName.Text = "";
            txtPackage.Text = "";
            txtUnitprice.Text = "";
            cmbSupplier.SelectedIndex = -1;
            chkIsDiscontinued.IsChecked = false;
        }
        private void PopulateProducts()
        {
            lstProducts.ItemsSource = null;
            lstProducts.ItemsSource = productService.GetAllProducts();
        }
        private void PopulateSuppliers()
        {
            cmbSupplier.ItemsSource = null;
            cmbSupplier.ItemsSource = productService.GetSuppliers();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstProducts.SelectedItem != null)
            {
                Product product = (Product)lstProducts.SelectedItem;
                txtProductName.Text = product.ProductName;
                txtPackage.Text = product.Package;
                txtUnitprice.Text = product.UnitPrice.ToString("#,##0.00");
                int row = -1;
                foreach(ProductSupplier productSupplier in cmbSupplier.Items)
                {
                    row++;
                    if (productSupplier.Id == product.SupplierId)
                        break;
                }
                cmbSupplier.SelectedIndex = row;
                chkIsDiscontinued.IsChecked = product.IsDisContinued;

            }
        }

        private void btnProductNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            ViewRight();
            txtProductName.Focus();
        }

        private void btnProductEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null)
            {
                isNew = false;
                ViewRight();
                txtProductName.Focus();
            }
        }

        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null)
            {
                if (MessageBox.Show("Zeker?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Product product = (Product)lstProducts.SelectedItem;
                    if (productService.ProductDelete(product))
                    {
                        ClearControls();
                        PopulateProducts();
                    }
                    else
                    {
                        MessageBox.Show("Niet verwijderd", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            string package = txtPackage.Text.Trim();
            decimal unitPrice;
            decimal.TryParse(txtUnitprice.Text, out unitPrice);
            bool isDiscontinued = (bool)chkIsDiscontinued.IsChecked;
            if(cmbSupplier.SelectedItem == null)
            {
                cmbSupplier.Focus();
                return;
            }
            int supplierId = ((ProductSupplier)cmbSupplier.SelectedItem).Id;

            Product product;
            int zoekId;
            if (isNew)
            {
                product = new Product(productName, supplierId, unitPrice, package, isDiscontinued);
                zoekId = productService.ProductAdd(product);
            }
            else
            {
                product = (Product)lstProducts.SelectedItem;
                zoekId = product.Id;
                product.ProductName = productName;
                product.SupplierId = supplierId;
                product.UnitPrice = unitPrice;
                product.Package = package;
                product.IsDisContinued = isDiscontinued;
                productService.ProductUpdate(product);
            }
            ViewLeft();
            PopulateProducts();
            SelectProduct(zoekId);
            lstProducts_SelectionChanged(null, null);

        }
        private void SelectProduct(int id)
        {
            int row = -1;
            foreach(Product product in lstProducts.Items)
            {
                row++;
                if (product.Id == id)
                    break;
            }
            lstProducts.SelectedIndex = row;
        }
        private void btnProductCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            ViewLeft();
            lstProducts_SelectionChanged(null, null);
        }
    }
}
