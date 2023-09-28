using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class Customer
    {
        [Key]
        public int CUSTOMER_ID{ get; set; }
        public string? CUSTOMER_NAME{ get; set; }
        public string? CUSTOMER_ADDRESS { get; set; }
        public string? CUSTOMER_PHONE { get; set; }
        public string? CUSTOMER_CITY { get; set; }
    }
}
