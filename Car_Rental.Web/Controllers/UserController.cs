using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILoginService loginService;
        public UserController() 
        {
            loginService = new LoginService();
        }
        public IActionResult Registration_Page()
        {
            return View();
        }
        public IActionResult Login_Form()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login_Form(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login_Form", model);
            }

            var result = loginService.login(model);
            if (result != null && result.ID > 0)
            {
                HttpContext.Session.SetString("UserEmail", result.USER_EMAIL.ToString());
                HttpContext.Session.SetString("UserID", result.ID.ToString());

                return Json(result);
            }
            else
            {
                TempData["Message"] = "Login failed The user name or password are incorrect!";
                return RedirectToAction("Login_Form");
            }

        }

        [HttpPost]
        public ActionResult SaveUser(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration_Page", login);
            }

            bool result = loginService.AddUser(login);

            if(result)
            {
                TempData["Message"] = "Registration Successfully done";
                return RedirectToAction("Login_Form", "User");
            }
            else
            {
                return RedirectToAction("Registration_Page", "User");
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login_Form");
        }
    }
}
