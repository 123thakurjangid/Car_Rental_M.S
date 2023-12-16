using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class Pending_Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAR_ID{ get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public string? CUSTOMER_ADDHAR_NO { get; set; }
        public string? CUSTOMER_MOBILE_NO { get; set; }
        public string? CUSTOMER_HOME_ADDRESS { get; set; }
        public string? CUSTOMER_HOME_CITY { get; set; }
        public string? PLATE_NUMBER{ get; set; }
        public string? MODEL{ get; set; }
        public int PRICE{ get; set; }
        public DateTime RENT_DATE{ get; set; }
        public DateTime RETURN_DATE{ get; set; }
        public string? CUSTOMER_EMAIL_ID { get; set; }
    }
}
