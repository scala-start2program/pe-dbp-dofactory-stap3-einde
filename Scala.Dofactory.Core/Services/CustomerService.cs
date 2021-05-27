using System;
using System.Collections.Generic;
using System.Text;

using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Scala.Dofactory.Core.Entities;


namespace Scala.Dofactory.Core.Services
{
    public class CustomerService
    {

        public List<Customer> GetAllCustomers()
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    List<Customer> customers = connection.GetAll<Customer>().ToList();
                    connection.Close();
                    customers = customers.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
                    return customers;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int CustomerAdd(Customer customer)  // we retourneren het id dat SQL gegenereerd heeft;
        {

            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    var newID = connection.Insert(customer);
                    connection.Close();
                    return (int)newID;
                }
                catch (Exception fout)
                {
                    return -1;
                }
            }
        }
        public bool CustomerUpdate(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Update(customer);
                    connection.Close();
                    return true;
                }
                catch(Exception fout)
                {
                    return false;
                }
            }
        }
        public bool CustomerDelete(Customer customer)
        {
            int countCustomersInOrders = 0;
            string sql = "select count(*) from orders where CustomerId = @id";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                connection.Open();
                countCustomersInOrders = connection.ExecuteScalar<int>(sql,customer);
                connection.Close();                
            }
            if (countCustomersInOrders != 0)
                return false;

            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Delete(customer);
                    connection.Close();
                    return true;
                }
                catch (Exception fout)
                {
                    return false;
                }
            }
        }

    }
}
