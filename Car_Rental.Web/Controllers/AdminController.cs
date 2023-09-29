using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Car_Rental.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        public AdminController() 
        {
            _carService = new CarService();
        }
        public IActionResult Admin_Pannel()
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

            bool result = _carService.AddCar(Car);

            if (result)
            {
                TempData["Message"] = "Car Added Successfully";
                return Json("true");
            }
            else
            {
                TempData["Message"] = "Car  Not Added !";
                return RedirectToAction("Add_New_Car", "Admin");
            }
        }
        public IActionResult AvailableCars()
        {
            List<CarModel> cars = new List<CarModel>();
            cars = _carService.getAllCars();
            return View(cars);
        }
    }
}
