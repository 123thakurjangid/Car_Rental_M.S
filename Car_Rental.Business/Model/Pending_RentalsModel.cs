using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    public class Pending_RentalsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Car_Id { get; set; }
        public string? Customer_Name { get; set; }
        public string? Plate_Number { get; set; }
        public string? Model { get; set; }
        public int Price { get; set; }
        public DateTime Rent_Date { get; set; }
        public DateTime Return_Date { get; set; }
    }
}
