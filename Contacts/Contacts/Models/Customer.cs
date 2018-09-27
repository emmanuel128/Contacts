using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Models
{
    public class Customer
    {
        public string _id { get; set; }
        public string FirstName { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public object CustomerID { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
