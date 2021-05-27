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
    /// Interaction logic for winCustomers.xaml
    /// </summary>
    public partial class winCustomers : Window
    {
        CustomerService customerService;
        bool isNew;

        public winCustomers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerService = new CustomerService();
            ViewLeft();
            PopulateCustomers();
        }
        private void ViewLeft()
        {
            grpCustomers.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnCustomerSave.Visibility = Visibility.Hidden;
            btnCustomerCancel.Visibility = Visibility.Hidden;
        }
        private void ViewRight()
        {
            grpCustomers.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnCustomerSave.Visibility = Visibility.Visible;
            btnCustomerCancel.Visibility = Visibility.Visible;
        }
        private void ClearControls()
        {
            txtCity.Text = "";
            txtCountry.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
        }
        private void PopulateCustomers()
        {
            lstCustomers.ItemsSource = null;
            lstCustomers.ItemsSource = customerService.GetAllCustomers();
        }
        private void LstCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstCustomers.SelectedItem != null)
            {
                Customer customer = (Customer)lstCustomers.SelectedItem;
                txtCity.Text = customer.City;
                txtCountry.Text = customer.Country;
                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtPhone.Text = customer.Phone;
            }
        }
        private void BtnCustomerNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            ViewRight();
            txtLastName.Focus();
        }
        private void BtnCustomerEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem != null)
            {
                isNew = false;
                ViewRight();
                txtLastName.Focus();
            }
        }
        private void btnCustomerDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem != null)
            {
                if (MessageBox.Show("Zeker?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Customer customer = (Customer)lstCustomers.SelectedItem;
                    if (customerService.CustomerDelete(customer))
                    {
                        ClearControls();
                        PopulateCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Niet verwijderd", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void btnCustomerSave_Click(object sender, RoutedEventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string city = txtCity.Text.Trim();
            string country = txtCountry.Text.Trim();
            string phone = txtPhone.Text.Trim();

            // controler op het inhoudelijke zou hier moeten gebeuren

            Customer customer;
            int zoekId;
            if (isNew)
            {
                customer = new Customer(firstName, lastName, city, country, phone);
                zoekId = customerService.CustomerAdd(customer);
            }
            else
            {
                customer = (Customer)lstCustomers.SelectedItem;
                zoekId = customer.Id;
                customer.LastName = lastName;
                customer.FirstName = firstName;
                customer.City = city;
                customer.Country = country;
                customer.Phone = phone;
                customerService.CustomerUpdate(customer);
            }
            ViewLeft();
            PopulateCustomers();
            SelectCustomer(zoekId);
            LstCustomers_SelectionChanged(null, null);
        }
        private void SelectCustomer(int id)
        {
            for (int r = 0; r < lstCustomers.Items.Count; r++)
            {
                if (((Customer)lstCustomers.Items[r]).Id == id)
                {
                    lstCustomers.SelectedIndex = r;
                    lstCustomers.BringIntoView();
                    return;
                }

            }
        }
        private void btnCustomerCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            ViewLeft();
            LstCustomers_SelectionChanged(null, null);
        }
    }
}
