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
        [Required(ErrorMessage = "* Please enter Car_Id !")]
        public int Car_Id { get; set; }
        [Required(ErrorMessage = "* Please enter your Name !")]
        public string? Customer_Name { get; set; }
        [Required(ErrorMessage = "* Please enter your aadhaar number !")]
        [StringLength(12, ErrorMessage = "* Addhar degits must be 12 !")]
        public string? Customer_Addhar_No { get; set; }
        [Required(ErrorMessage = "* Please Enter the Phone number !")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "* numbers must be equals to 10 in length.")]
        public string? Customer_Mobile_No { get; set; }
        [Required(ErrorMessage = "* Please enter Home address !")]
        public string? Customer_Home_Address { get; set; }
        [Required(ErrorMessage = "* Please enter your home city !")]
        public string? Customer_Home_City { get; set; }
        [Required(ErrorMessage = "* Please enter Car Plate Number !")]
        public string? Plate_Number { get; set; }
        [Required(ErrorMessage = "* Please enter Car Model !")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "* Please enter Rent Prize !")]
        public int Price { get; set; }
        public DateTime Rent_Date { get; set; }
        [Required(ErrorMessage = "* Please enter Return Date !")]
        public DateTime Return_Date { get; set; }
        [Required(ErrorMessage = "* Please Enter Customer Email Id !")]
        [EmailAddress]
        public string? Customer_Email_Id { get; set; }
    }
}
