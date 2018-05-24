using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebApi.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string streetAddress { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }

        public Contact() { }
    }
}
