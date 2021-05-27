using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;


namespace Scala.Dofactory.Core.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]  // [Key] gebruiken i.p.v. [ExplicitKey] wanneer het om een identity (autonummering) gaat
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public Customer()
        {
        }
        public Customer(string firstName, string lastName, string city, string country, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Country = country;
            Phone = phone;
        }
        public Customer(int id, string firstName, string lastName, string city, string country, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Country = country;
            Phone = phone;
        }
        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
