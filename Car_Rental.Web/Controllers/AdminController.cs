﻿using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Car_Rental.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly LoginDbContext _cardbcontext;
        public AdminController() 
        {
            _carService = new CarService();
            _cardbcontext = new LoginDbContext();
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
            return View(cars);
        }

        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delte !";
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
