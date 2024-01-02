using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Entities
{
    public class On_Location
    {
        [Key]
        public int CUSTOMER_ID { get; set; }
        public string? CUSTOMER_NAME{ get; set; }
        public string? MOBILE_NUMBER{ get; set; }
        public string? AADHAAR_NUMBER{ get; set; }
        public string? HOME_ADDRESS{ get; set; }
        public DateTime CURRENT_DATE{ get; set; }
        public int DISTANCE_KM{ get; set; }
        public string? CAR_NAME{ get; set; }
        public string? CAR_PROBLEM{ get; set; }
        public string? LATITUDE{ get; set; }
        public string? LONGITUDE { get; set; }
    }
}
