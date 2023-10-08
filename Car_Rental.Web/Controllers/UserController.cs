using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILoginService loginService;
        private readonly ICustomerService _customerService;
        private readonly LoginDbContext _loginDbContext;
        public UserController() 
        {
            loginService = new LoginService();
            _customerService = new CustomerService();
            _loginDbContext = new LoginDbContext();
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
        public IActionResult Login_Form(LoginModel model,string Loginby)
        {
            if (!ModelState.IsValid)
            {
                return View("Login_Form", model);
            }

            var result = loginService.login(model);
            if (result != null && result.ID > 0)
            {

                if (Loginby == "Customer")
                {
                    HttpContext.Session.SetString("UserEmail", result.USER_EMAIL.ToString());
                    HttpContext.Session.SetString("UserID", result.ID.ToString());
                    HttpContext.Session.SetString("LoginBy", Loginby);

                    return RedirectToAction("Customer_Home_Page", "Admin");
                }
                else if(Loginby == "Admin" && result.USER_EMAIL == "123Thakurjangid@gmail.com")
                {
                    HttpContext.Session.SetString("UserEmail", result.USER_EMAIL.ToString());
                    HttpContext.Session.SetInt32("UserID", result.ID);
                    HttpContext.Session.SetString("LoginBy", Loginby);

                    return RedirectToAction("Admin_Pannel","Admin");
                }
                else
                {
                    TempData["Message"] = "Login failed The Admin Username or password are incorrect!";
                    return RedirectToAction("Login_Form");
                }
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

        [HttpGet]
        public IActionResult Customers()
        {
            List<LoginModel> user = new List<LoginModel>();
            user = loginService.GetUsers();
            return View(user);

        }
        /*Testing start*/

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var customer = await _loginDbContext.Login.FirstOrDefaultAsync(x => x.ID == id);

            var viewmodel = new UpdateModel()
            {
                Id = customer.ID,
                User_Email = customer.USER_EMAIL,
                User_Password = customer.USER_PASSWORD,
            };

            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateModel model)
        {
            var user = await _loginDbContext.Login.FindAsync(model.Id);

            if (user != null)
            {
                user.USER_EMAIL = model.User_Email;
                user.USER_PASSWORD = model.User_Password;

                await _loginDbContext.SaveChangesAsync();
                TempData["Message"] = "Information Updated Successfully";
                return RedirectToAction("Customers", "User");
            }

            TempData["Message"] = "Information Not Update";
            return RedirectToAction("Customers", "User");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOfflineCustomer(int id)
        {
            var customer = await _loginDbContext.Customer.FirstOrDefaultAsync(x => x.CUSTOMER_ID == id);

            var viewmodel = new UpdateOfflineCustomerModel()
            {
                Customer_Id = customer.CUSTOMER_ID,
                Customer_Name = customer.CUSTOMER_NAME,
                Customer_Address = customer.CUSTOMER_ADDRESS,
                Customer_Phone = customer.CUSTOMER_PHONE,
                Customer_City = customer.CUSTOMER_CITY,
                
            };

            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOfflineCustomer(UpdateOfflineCustomerModel model)
        {
            var user = await _loginDbContext.Customer.FindAsync(model.Customer_Id);

            if (user != null)
            {
                user.CUSTOMER_ID = model.Customer_Id;
                user.CUSTOMER_NAME = model.Customer_Name;
                user.CUSTOMER_ADDRESS = model.Customer_Address;
                user.CUSTOMER_PHONE = model.Customer_Phone;
                user.CUSTOMER_CITY = model.Customer_City;

                await _loginDbContext.SaveChangesAsync();
                TempData["Message"] = "Information Updated Successfully";
                return RedirectToAction("OfflineCustomers", "User");
            }

            TempData["Message"] = "Information Not Update";
            return RedirectToAction("OfflineCustomers", "User");
        }

        /*Testing ending*/

        public IActionResult Add_New_Customer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save_Customer(CustomerModel customers)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_New_Customer", customers);
            }

            bool result = _customerService.AddCustomer(customers);

            result = _customerService.AddCustomer(customers);

            if (result)
            {
                TempData["Message"] = "Customer Added Successfully";
                return Json("true");
            }
            else
            {
                TempData["Message"] = "Customer Not Added !";
                return RedirectToAction("Add_New_Customer", "Customer");
            }
        }

        [HttpGet]
        public IActionResult OfflineCustomers()
        {
            List<CustomerModel> custm = new List<CustomerModel>();
            custm = _customerService.GetCustomer();
            return View(custm);
        }

        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delte !";
            }
            if (_customerService.Delete(Id))
            {
                TempData["Message"] = "Record Deleted Successfully";

            }
            else
            {
                TempData["Message"] = "Record not deleted !";
            }
            return RedirectToAction("OfflineCustomers", "User");
        }
    }
}
