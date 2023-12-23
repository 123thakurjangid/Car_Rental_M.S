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
        private readonly IRentals_History_Service _historyService;
        public Pending_RentalsController()
        {
            _pendingRentalsService = new Pending_Rental_Service();
            _loginDbContext = new LoginDbContext();
            _historyService = new Rentals_History_Service();
        }

        [HttpPost]
        public IActionResult SaveRental(Pending_RentalsModel rmodel,User_Home_SelectModel uhsm)
        {

            if (!ModelState.IsValid)
            {
                return View("Add_Pending_Rentals", uhsm);
            }
            else
            {
                try 
                {
                    bool result = _pendingRentalsService.AddRentals(rmodel);  // for saving rental
                    if (result)
                    {
                        bool history = _historyService.Add_Rentals_History(rmodel); //for Saving History

                        TempData["Message"] = "Car Rented";
                        return RedirectToAction("Specific_Customer_Pending_Rentals", "User");
                    }
                    else
                    {
                        TempData["Message"] = "Car Not Rented !";
                        return RedirectToAction("Customer_Home_Page", "Admin");
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Car Was already rented by another user You Can Choose Another Car";
                    return RedirectToAction("Customer_Home_Page", "Admin");
                }
            }

        }

        [HttpGet]
        public IActionResult Show_History()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form", "User");
            }

            List<Pending_RentalsModel> models = new List<Pending_RentalsModel>();
            models = _historyService.Get_History();
            return View(models);
        }

        [HttpPost]
        public IActionResult SaveRental_ForAdmin(Pending_RentalsModel rmodel)
        {

            if (!ModelState.IsValid)
            {
                return View("Renting_For_Offline_Customers", rmodel);
            }
            else
            {
                try
                {
                    bool result = _pendingRentalsService.AddRentals(rmodel);
                    if (result)
                    {
                        TempData["Message"] = "Car Rented";
                        return RedirectToAction("Customer_PendingRentals", "User");
                    }
                    else
                    {
                        TempData["Message"] = "Car Not Rented !";
                        return RedirectToAction("Renting_For_Offline_Customers");
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Car or Plate Number is Already Exist In Inventory !";
                    return RedirectToAction("Renting_For_Offline_Customers");
                }
            }

        }

        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delete !";
            }
            if (_pendingRentalsService.Delete(Id))
            {
                TempData["Message"] = "Rental Deleted";

            }
            else
            {
                TempData["Message"] = "Rental not Deleted !";
            }
            return RedirectToAction("Specific_Customer_Pending_Rentals", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Add_Pending_Rentals(int id)
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form", "User");
            }

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
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form", "User");
            }

            return View();
        }
    }
}
