using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Web.Controllers
{
    public class Pending_RentalsController : Controller
    {
        private readonly IPendingRentals_Service _pendingRentalsService;
        private readonly LoginDbContext _loginDbContext;
        public Pending_RentalsController()
        {
            _pendingRentalsService = new Pending_Rental_Service();
            _loginDbContext = new LoginDbContext();
        }

        [HttpPost]
        public IActionResult SaveRental(Pending_RentalsModel rmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("Renting_For_Offline_Customers", rmodel);
            }
            bool result = _pendingRentalsService.AddRentals(rmodel);
            if (result)
            {
                TempData["Message"] = "Car Rented";
                return RedirectToAction("Customer_PendingRentals", "Pending_Rentals");
            }
            else
            {
                TempData["Message"] = "Car Not Rented !";
                return RedirectToAction("Customer_Home_Page", "Admin");
            }

        }

        [HttpGet]
        public IActionResult Customer_PendingRentals()
        {
            List<Pending_RentalsModel> Rentals = new List<Pending_RentalsModel>();
            Rentals = _pendingRentalsService.GetRentals();
            return View(Rentals);

        }

        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delte !";
            }
            if (_pendingRentalsService.Delete(Id))
            {
                TempData["Message"] = "Rental Removed";

            }
            else
            {
                TempData["Message"] = "Rental not Remove !";
            }
            return RedirectToAction("Customer_PendingRentals", "Pending_Rentals");
        }

        [HttpGet]
        public async Task<IActionResult> Add_Pending_Rentals(int id)
        {
            var RentalDetails = await _loginDbContext.Car.FirstOrDefaultAsync(x => x.CAR_ID == id);

            var viewmodel = new User_Home_SelectModel()
            {
                Car_Id = RentalDetails.CAR_ID,
                Plate_Number = RentalDetails.PLATE_NUMBER,
                Model = RentalDetails.MODEL,
                Price = RentalDetails.PRICE,
            };

            return View(viewmodel);
        }

        public IActionResult Renting_For_Offline_Customers()
        {
            return View();
        }
    }
}
