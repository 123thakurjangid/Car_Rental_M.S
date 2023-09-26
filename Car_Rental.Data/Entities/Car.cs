using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class Car
    {
        [Key]
        public string? PLATE_NUMBER { get; set; }
        public string? COMPANY { get; set; }
        public string? MODEL { get; set; }
        public int PRICE { get; set; }
        public string? COLOR { get; set; }
        public string? AVAILABLE { get; set; }
    }
}
