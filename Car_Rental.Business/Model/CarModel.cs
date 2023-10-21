﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Car_Rental.Business.Model
{
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Car_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Please Enter Car Plate Num!")]
        public string? Plate_Number { get; set; }
        [Required(ErrorMessage = "Please Enter Car Company!")]
        public string? Company { get; set; }
        [Required(ErrorMessage = "Please Enter Car Model!")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Please Enter Car Price!")]
        [Range(500, 7000, ErrorMessage = "Price must be in 500 to 7000!")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Please Enter Car Color!")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Please Enter Car Available or Not!")]
        public string? Available { get; set; }

    }

    /*for passing data with the help of userhome -> select*/
    public class User_Home_SelectModel
    {
        [Required(ErrorMessage = "Please Enter id !")]
        public int Car_Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name !")]
        public string? Customer_Name { get; set; }
        [Required(ErrorMessage = "Please Enter Rent date !")]
        public DateTime Rent_Date { get; set; }
        [Required(ErrorMessage = "Please Enter Return Date !")]
        public DateTime Return_Date { get; set; }
        [Required(ErrorMessage = "Please Enter Plate number !")]
        public string? Plate_Number { get; set; }
        [Required(ErrorMessage = "Please Enter model !")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Please Enter Price !")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Email Id !")]
        public string Customer_Email_id { get; set; }
    }
}
