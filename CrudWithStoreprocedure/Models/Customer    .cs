using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CrudWithStoreprocedure.Models
{
    public class Customer
    {
       
        public int id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public List<Customer> custlist { get; set; }
    }
}