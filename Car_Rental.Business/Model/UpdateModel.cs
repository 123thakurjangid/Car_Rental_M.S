using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    /*This model is for Updating customer Details*/
    public class UpdateModel
    {
        public int Id { get; set; }
        public string? User_Email { get; set; }
        public string? User_Password { get; set; }
    }

    public class UpdateOfflineCustomerModel
    {
        public int Customer_Id { get; set; }
        public string? Customer_Name { get; set; }
        public string? Customer_Address { get; set; }
        public string? Customer_Phone { get; set; }
        public string? Customer_City { get; set; }
    }
    public class UpdateCarModel
    {
        public int Car_Id { get; set; }
        public string? Plate_Number { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public string? Available { get; set; }
    }
}
