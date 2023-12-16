using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using Car_Rental.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Service
{
    public class Rentals_History_Service : IRentals_History_Service
    {
        private readonly IRentals_History_Interface _interface;

        public Rentals_History_Service()
        {
            _interface = new Rentals_History_Repository();
        }
        public bool Add_Rentals_History(Pending_RentalsModel model)
        {
            var result = false;

            Rentals_History rental = new Rentals_History();

            
            rental.CUSTOMER_EMAIL_ID = model.Customer_Email_Id;
            rental.CUSTOMER_NAME = model.Customer_Name;
            rental.CUSTOMER_MOBILE_NO = model.Customer_Mobile_No;
            rental.CUSTOMER_ADDHAR_NO = model.Customer_Addhar_No;
            rental.CUSTOMER_HOME_CITY = model.Customer_Home_City;
            rental.CUSTOMER_HOME_ADDRESS = model.Customer_Home_Address;
            rental.PLATE_NUMBER = model.Plate_Number;
            rental.MODEL = model.Model;
            rental.PRICE = model.Price;
            rental.RENT_DATE = model.Rent_Date;
            rental.RETURN_DATE = model.Return_Date;

            result = _interface.Save_Rentals_History(rental);
            return result;
        }

        public List<Pending_RentalsModel> Get_History()
        {
            return _interface.Get_History().Select(x => new Pending_RentalsModel
            {
                Car_Id = x.CAR_ID,
                Customer_Name = x.CUSTOMER_NAME,
                Customer_Email_Id = x.CUSTOMER_EMAIL_ID,
                Customer_Mobile_No = x.CUSTOMER_MOBILE_NO,
                Customer_Addhar_No = x.CUSTOMER_ADDHAR_NO,
                Customer_Home_City = x.CUSTOMER_HOME_CITY,
                Customer_Home_Address = x.CUSTOMER_HOME_ADDRESS,
                Plate_Number = x.PLATE_NUMBER,
                Model = x.MODEL,
                Price = x.PRICE,
                Rent_Date = x.RENT_DATE,
                Return_Date = x.RETURN_DATE,
            }).ToList();
        }
    }
}
