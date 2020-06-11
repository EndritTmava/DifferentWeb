using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
    }
}