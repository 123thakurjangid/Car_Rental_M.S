using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using Car_Rental.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Service
{
    public class On_Location_Service : IOn_LocationService
    {
        public readonly IOn_Location_Interface location_Interface;
        public On_Location_Service() 
        {
            location_Interface = new On_LocationRepository();
        }

        public bool AddLocation(LocationModel location_Model)
        {
            bool result = false;

            On_Location onlocation = new On_Location();

            onlocation.CUSTOMER_NAME = location_Model.Customer_Name;
            onlocation.MOBILE_NUMBER = location_Model.Mobile_Number;
            onlocation.AADHAAR_NUMBER = location_Model.Aadhaar_Number;
            onlocation.HOME_ADDRESS = location_Model.Home_Address;
            onlocation.CURRENT_DATE = location_Model.Current_date;
            onlocation.DISTANCE_KM = location_Model.Distance_Km;
            onlocation.CAR_NAME = location_Model.Car_Name;
            onlocation.CAR_PROBLEM = location_Model.Car_Problem;
            onlocation.LATITUDE = location_Model.latitude;
            onlocation.LONGITUDE = location_Model.longitude;

            result = location_Interface.SaveLocation(onlocation);

            return result;
        }

        public List<LocationModel> Get_location_lists()
        {
            return location_Interface.getlocation().Select(x => new LocationModel
            {
                Customer_Id = x.CUSTOMER_ID,
                Customer_Name = x.CUSTOMER_NAME,
                Mobile_Number = x.MOBILE_NUMBER,
                Aadhaar_Number = x.AADHAAR_NUMBER,
                Home_Address   = x.HOME_ADDRESS,
                Current_date = x.CURRENT_DATE,
                Distance_Km = x.DISTANCE_KM,
                Car_Name = x.CAR_NAME,
                Car_Problem = x.CAR_PROBLEM,
                latitude = x.LATITUDE,
                longitude = x.LONGITUDE,

            }).ToList();
        }
    }
}
