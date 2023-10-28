using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace Car_Rental.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPendingRentals_Service _pendingRentalsService;
        private readonly ICarService _carService;
        private readonly LoginDbContext _cardbcontext;
        public AdminController() 
        {
            _pendingRentalsService = new Pending_Rental_Service();
            _carService = new CarService();
            _cardbcontext = new LoginDbContext();
        }
        public IActionResult Admin_Pannel()
        {
            return View();
        }

        public IActionResult Customers_Car_Inventory()
        {
            return View();
        }

        public IActionResult Add_New_Car()
        {       
            CarModel model = new CarModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Save_Cars(CarModel Car)
        {          
            if(!ModelState.IsValid)
            {
                return View("Add_New_Car", Car);
            }
            else
            {
                try
                {
                    bool result = _carService.AddCar(Car);

                    if (result)
                    {
                        TempData["Message"] = "Car Added Successfully";
                        return RedirectToAction("AvailableCars");
                    }
                    else
                    {
                        TempData["Message"] = "Car  Not Added !";
                        return RedirectToAction("Add_New_Car", "Admin");
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Car Plate Number Exist Choose Another plate number !";
                    return RedirectToAction("Add_New_Car");
                }
            }

        }
        [HttpGet]
        public IActionResult AvailableCars()
        {
            List<CarModel> cars = new List<CarModel>();
            cars = _carService.getAllCars();
            return View(cars);
        }

        [HttpGet]
        public IActionResult Customer_Home_Page()
        {
            List<CarModel> cars = new List<CarModel>();
            cars = _carService.getAllCars();

            List<Pending_RentalsModel> Rentals = new List<Pending_RentalsModel>();
            Rentals = _pendingRentalsService.GetRentals();

            foreach (var item in cars)
            {
                int Search_id = item.Car_Id;
                if(Search_id != null)
                {
                    var data = Rentals.Where(Model => Model.Car_Id == Search_id).ToList();
                    int id = data.Count;
                    if (id>0)
                    {
                        item.Available = "Not Available";
                    }
                    else
                    {
                        item.Available = "Available";
                    }
                }

            }

            return View(cars);

        }

        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delete !";
            }
            if (_carService.Delete(Id))
            {
                TempData["Message"] = "Record Deleted Successfully";

            }
            else
            {
                TempData["Message"] = "Record not deleted !";
            }
            return RedirectToAction("AvailableCars", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = await _cardbcontext.Car.FirstOrDefaultAsync(x => x.CAR_ID == id);

            var viewmodel = new UpdateCarModel()
            {
                Car_Id = car.CAR_ID,
                Plate_Number = car.PLATE_NUMBER,
                Company = car.COMPANY,
                Model = car.MODEL,
                Price = car.PRICE,
                Color = car.COLOR,
                Available = car.AVAILABLE,
            };
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarModel carmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateCar", carmodel);
            }

            var car = await _cardbcontext.Car.FindAsync(carmodel.Car_Id);

            if (car != null)
            {
                car.CAR_ID = carmodel.Car_Id;
                car.PLATE_NUMBER = carmodel.Plate_Number;
                car.COMPANY = carmodel.Company;
                car.MODEL = carmodel.Model;
                car.PRICE = carmodel.Price;
                car.COLOR = carmodel.Color;
                car.AVAILABLE = carmodel.Available;

                await _cardbcontext.SaveChangesAsync();
                TempData["Message"] = "Information Updated Successfully";
                return RedirectToAction("AvailableCars", "Admin");
            }

            TempData["Message"] = "Information Not Update";
            return RedirectToAction("AvailableCars", "Admin");
        }
    }
}
