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
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    List<Product> products = connection.GetAll<Product>().ToList();
                    connection.Close();
                    products = products.OrderBy(p => p.ProductName).ToList();
                    return products;
                }
                catch
                {
                    return null;
                }
            }
        }
        public List<ProductSupplier> GetSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    List<ProductSupplier> productSuppliers = connection.GetAll<ProductSupplier>().ToList();
                    connection.Close();
                    productSuppliers = productSuppliers.OrderBy(p => p.CompanyName).ToList();
                    return productSuppliers;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int ProductAdd(Product product)  // we retourneren het id dat SQL gegenereerd heeft;
        {

            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    var newID = connection.Insert(product);
                    connection.Close();
                    return (int)newID;
                }
                catch (Exception fout)
                {
                    return -1;
                }
            }
        }
        public bool ProductUpdate(Product product)
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Update(product);
                    connection.Close();
                    return true;
                }
                catch (Exception fout)
                {
                    return false;
                }
            }
        }
        public bool ProductDelete(Product product)
        {
            int countProductsInOrders = 0;
            string sql = "select count(*) from OrderItem where ProductId = @id";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                connection.Open();
                countProductsInOrders = connection.ExecuteScalar<int>(sql, product);
                connection.Close();
            }
            if (countProductsInOrders != 0)
                return false;

            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Delete(product);
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
