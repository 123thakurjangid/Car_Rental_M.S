using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Web.Controllers
{
    public class On_Location_Service_Controller : Controller
    {
        private readonly IOn_LocationService _location_service;
        public On_Location_Service_Controller()
        {
            _location_service = new On_Location_Service();
        }
        public IActionResult Car_Service()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form", "User");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Save_Location(LocationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Car_Service", model);
            }
            else
            {
                try
                {
                    bool result = _location_service.AddLocation(model);

                    if (result)
                    {
                        TempData["Message"] = "Your Responce is Added Successfully.We reach on your location within 30mint.";
                        return RedirectToAction("Car_Service", "On_Location_Service_");
                    }
                    else
                    {
                        TempData["Message"] = "Oops Responce is  Not Added !";
                        return RedirectToAction("Car_Service", "On_Location_Service_");
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "First Step is incomplete,or Internet issue, try again !";
                    return RedirectToAction("Car_Service");
                }
            }

        }

        [HttpGet]
        public IActionResult On_Service_Requests()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form", "User");
            }

            List<LocationModel> models = new List<LocationModel>();
            models = _location_service.Get_location_lists();
            return View(models);
        }
    }
}
