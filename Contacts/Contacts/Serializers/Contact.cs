using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Serializers
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
