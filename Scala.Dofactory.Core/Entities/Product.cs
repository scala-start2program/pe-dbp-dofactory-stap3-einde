using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;



namespace Scala.Dofactory.Core.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]  // [Key] gebruiken i.p.v. [ExplicitKey] wanneer het om een identity (autonummering) gaat
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDisContinued { get; set; }

        public Product()
        { }
        public Product(string productName, int supplierId, decimal unitPrice, string package, bool isDisContinued)
        {
            ProductName = productName;
            SupplierId = supplierId;
            UnitPrice = unitPrice;
            Package = package;
            IsDisContinued = isDisContinued;
        }
        public Product(int id, string productName, int supplierId, decimal unitPrice, string package, bool isDisContinued)
        {
            Id = id;
            ProductName = productName;
            SupplierId = supplierId;
            UnitPrice = unitPrice;
            Package = package;
            IsDisContinued = isDisContinued;
        }
        public override string ToString()
        {
            return ProductName;
        }
    }

    [Table("Supplier")]
    public class ProductSupplier
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return CompanyName;
        }
    }
    
}
