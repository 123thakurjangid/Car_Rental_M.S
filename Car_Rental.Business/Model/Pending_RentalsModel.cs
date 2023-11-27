using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Car_Rental.Business.Model
{
    public class Pending_RentalsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Please enter Car_Id !")]
        public int Car_Id { get; set; }
        [Required(ErrorMessage = "Please enter Customer Name !")]
        public string? Customer_Name { get; set; }
        [Required(ErrorMessage = "Please enter Car Plate Number !")]
        public string? Plate_Number { get; set; }
        [Required(ErrorMessage = "Please enter Car Model !")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Please enter Rent Prize !")]
        public int Price { get; set; }
        public DateTime Rent_Date { get; set; }
        [Required(ErrorMessage = "Please enter Return Date !")]
        public DateTime Return_Date { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Id !")]
        public string? Customer_Email_Id { get; set; }
    }
}
