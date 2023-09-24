using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class Login
    {
        [Key]
        public int ID { get; set; }
        public string? USER_EMAIL { get; set; }
        public string? USER_PASSWORD { get; set; }
    }
}
