using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    /*This model is for Updating customer Details*/
    public class UpdateModel
    {
        [Required(ErrorMessage = "Please enter User Id !")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter User Email !")]
        public string? User_Email { get; set; }
        [Required(ErrorMessage = "Please enter User Password !")]
        public string? User_Password { get; set; }
        public IFormFile? Attachmentfile { get; set; }
    }

    public class UpdateOfflineCustomerModel
    {
        [Required(ErrorMessage = "Please enter User Id !")]
        public int Customer_Id { get; set; }
        [Required(ErrorMessage = "Please enter Name !")]
        public string? Customer_Name { get; set; }
        [Required(ErrorMessage = "Please enter Address !")]
        public string? Customer_Address { get; set; }
        [Required(ErrorMessage = "Please enter Phone Number !")]
        public string? Customer_Phone { get; set; }
        [Required(ErrorMessage = "Please enter City !")]
        public string? Customer_City { get; set; }
    }
    public class UpdateCarModel
    {
        public int Car_Id { get; set; }
        [Required(ErrorMessage = "Please enter Car Plate Number !")]
        public string? Plate_Number { get; set; }
        [Required(ErrorMessage = "Please Enter Car Company!")]
        public string? Company { get; set; }
        [Required(ErrorMessage = "Please Enter Car Model!")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Please Enter Car Color!")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Please Enter Car Color!")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Please Enter Car Available or Not!")]
        public string? Available { get; set; }
    }
}
