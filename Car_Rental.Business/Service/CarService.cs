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
    public class CarService : ICarService
    {
        public readonly ICarInterface carInterface;
        public CarService()
        {
            carInterface = new CarRepository();
        }

        public bool AddCar(CarModel model)
        {
            bool result = false;

            Car car = new Car();

            car.PLATE_NUMBER = model.Plate_Number;
            car.COMPANY = model.Company;
            car.MODEL = model.Model;
            car.PRICE = model.Price;
            car.COLOR = model.Color;
            car.AVAILABLE = model.Available;

            result = carInterface.SaveCar(car);

            return result;
        }
        public List<CarModel> getAllCars()
        {
            return carInterface.getcars().Select(x => new CarModel
            {
                Plate_Number = x.PLATE_NUMBER,
                Company = x.COMPANY,
                Model = x.MODEL,
                Price = x.PRICE,
                Color = x.COLOR,
                Available = x.AVAILABLE

            }).ToList();
        }
    }
}
