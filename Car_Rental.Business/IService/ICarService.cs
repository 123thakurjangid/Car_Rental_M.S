using Car_Rental.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface ICarService
    {
        bool AddCar(CarModel carModel);
        bool Delete(int Id);
        List<CarModel> getAllCars();
    }
}
