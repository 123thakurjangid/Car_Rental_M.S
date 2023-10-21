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
    public class Pending_Rental_Service : IPendingRentals_Service
    {
        private readonly IPendingRentalsInterface _pendingRentalsInterface;
        public Pending_Rental_Service()
        {
            _pendingRentalsInterface = new Pending_Rental_Repository();
        }
        public bool AddRentals(Pending_RentalsModel model)
        {
            var result = false;

            Pending_Rental rental = new Pending_Rental();

            rental.CAR_ID = model.Car_Id;
            rental.CUSTOMER_NAME = model.Customer_Name;
            rental.PLATE_NUMBER = model.Plate_Number;
            rental.MODEL = model.Model;
            rental.PRICE = model.Price;
            rental.RENT_DATE = model.Rent_Date;
            rental.RETURN_DATE = model.Return_Date;
            rental.CUSTOMER_EMAIL_ID = model.Customer_Email_Id;

            result = _pendingRentalsInterface.SaveRentals(rental);
            return result;
        }

        public bool Delete(int id)
        {
            return _pendingRentalsInterface.Delete(id);
        }

        public List<Pending_RentalsModel> GetRentals()
        {
            return _pendingRentalsInterface.GetRentals().Select(x => new Pending_RentalsModel
            {
                Car_Id = x.CAR_ID,
                Customer_Name = x.CUSTOMER_NAME,
                Plate_Number = x.PLATE_NUMBER,
                Model = x.MODEL,
                Price   = x.PRICE,
                Rent_Date = x.RENT_DATE,
                Return_Date = x.RETURN_DATE,
                Customer_Email_Id = x.CUSTOMER_EMAIL_ID,

            }).ToList();
        }
    }
}
