using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class Rentals_History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAR_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public string? PLATE_NUMBER { get; set; }
        public string? MODEL { get; set; }
        public int PRICE { get; set; }
        public DateTime RENT_DATE { get; set; }
        public DateTime RETURN_DATE { get; set; }
    }
}
