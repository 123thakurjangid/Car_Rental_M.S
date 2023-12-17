using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    public class CustomerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Customer_Id { get; set; }
        [Required(ErrorMessage = "* Please Enter the name !")]
        public string? Customer_Name { get; set;}
        [Required(ErrorMessage = "* Please Enter the Address !")]
        public string? Customer_Address { get; set;}
        [Required(ErrorMessage = "* Please Enter the Phone number !")]
        [StringLength(10,MinimumLength = 10,ErrorMessage = "* numbers must be equals to 10 in length.")]
        public string? Customer_Phone { get; set;}
        [Required(ErrorMessage = "* Please Enter the City !")]
        public string? Customer_City { get; set; }
    }
}
