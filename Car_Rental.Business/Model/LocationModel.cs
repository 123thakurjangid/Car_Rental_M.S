using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Customer_Id { get; set; }
        [Required(ErrorMessage = "* Please enter your name !")]
        public string? Customer_Name { get; set; }
        [Required(ErrorMessage = "* Please Enter the Phone number !")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "* numbers must be equals to 10 in length.")]
        public string? Mobile_Number { get; set; }
        [Required(ErrorMessage = "* Please enter your aadhaar number !")]
        [StringLength(12, ErrorMessage = "* aadhaar degits must be 12 !")]
        public string? Aadhaar_Number { get;set; }
        [Required(ErrorMessage = "* Please enter your home address !")]
        public string? Home_Address { get; set; }
        public DateTime Current_date { get; set; }
        public int Distance_Km { get; set; }
        [Required(ErrorMessage = "* Please enter your car name !")]
        public string? Car_Name { get; set; }
        [Required(ErrorMessage = "* Please enter your car problem !")]
        public string? Car_Problem { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }

    }
}
