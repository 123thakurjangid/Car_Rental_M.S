using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Car_Rental.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IPendingRentals_Service _pendingRentalsService;
        private readonly ILoginService loginService;
        private readonly ICustomerService _customerService;
        private readonly LoginDbContext _loginDbContext;
        private IHostingEnvironment hostingEnv;
        public UserController(IHostingEnvironment env) 
        {
            _pendingRentalsService = new Pending_Rental_Service();
            loginService = new LoginService();
            _customerService = new CustomerService();
            _loginDbContext = new LoginDbContext();
            this.hostingEnv = env;
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
                    HttpContext.Session.SetString("UserImg", result.ATTACHMENTURL.ToString());

                    return RedirectToAction("Customers_Car_Inventory", "Admin");
                }
                else if(Loginby == "Admin" && result.USER_EMAIL == "123Thakurjangid@gmail.com")
                {
                    HttpContext.Session.SetString("UserEmail", result.USER_EMAIL.ToString());
                    HttpContext.Session.SetInt32("UserID", result.ID);
                    HttpContext.Session.SetString("LoginBy", Loginby);
                    HttpContext.Session.SetString("UserImg", result.ATTACHMENTURL.ToString());

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
        public async Task<IActionResult> SaveUser(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration_Page", login);
            }

            if(login.Attachmentfile == null)
            {
                login.AttachmentUrl = "/AdminPanelCars/User.png";
            }

            /*start*/
            if (login != null && login.Attachmentfile != null)  // it is for save image url in database
            {
                var filePath = "/image/";

                login.AttachmentUrl = filePath + login.Attachmentfile.FileName;

            }

            if (login != null && login.Attachmentfile != null)  // IT IS FOR SAVE IMAGE INTO  PROJECT FOLDER  ~/WWW.ROOT/IMAGE;
            {
                var FileDic = "Image";
                string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                var fileName = login.Attachmentfile.FileName;
                var filePath = Path.Combine(FilePath, fileName);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    login.Attachmentfile.CopyTo(fs);
                }
            }

            /*end*/

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
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            List<LoginModel> user = new List<LoginModel>();
            user = loginService.GetUsers();
            return View(user);

        }
        /*Update Actions*/

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

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
            if (!ModelState.IsValid)
            {
                return View("UpdateCustomer", model);
            }

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
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

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
            if(!ModelState.IsValid)
            {
                return View("UpdateOfflineCustomer", model);
            }

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

        /* ending*/

        public IActionResult Add_New_Customer()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

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

            if (result)
            {
                TempData["Message"] = "Customer Added Successfully";
                return RedirectToAction("OfflineCustomers");
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
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            List<CustomerModel> custm = new List<CustomerModel>();
            custm = _customerService.GetCustomer();
            return View(custm);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delete !";
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
        public IActionResult DeleteOnlineCustomer(int Id)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Record not found to delete !";
            }
            if (loginService.DeleteOnlineCustomer(Id))
            {
                TempData["Message"] = "Record Deleted Successfully";

            }
            else
            {
                TempData["Message"] = "Record not deleted !";
            }
            return RedirectToAction("Customers", "User");
        }
        public IActionResult MailSendform()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            return View();  
        }
        public IActionResult ContectUs(String ReciverMail)
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            try
            {
                string fromMail = "123thakurjangid@gmail.com";
                string fromPassword = "fdxbcnpxstughuml";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "Test Console Web";
                message.To.Add(new MailAddress(ReciverMail));
                message.Body = "<html><body> We got your request we will contect you soon ! </body></html>";
                message.IsBodyHtml = true;

                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpclient.Send(message);
                TempData["Message"] = "We got Your request successfully, We will contect you soon !";
                return RedirectToAction("Customer_Home_Page", "Admin");
            }
            catch (Exception)
            {
                TempData["Message"] = "Request fail to send! Check Your Internet Connection ! Or field Value should be null";
                return RedirectToAction("MailSendform", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserProfileUpdate(int id)
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

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
        public async Task<IActionResult> UserProfileUpdate(UpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateCustomer", model);
            }

            var user = await _loginDbContext.Login.FindAsync(model.Id);

            if (user != null)
            {
                if(model.Attachmentfile != null)
                {
                    var FileDic = "Image";
                    string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);
                    if (!Directory.Exists(FilePath))
                        Directory.CreateDirectory(FilePath);
                    var fileName = model.Attachmentfile.FileName;
                    var filePath = Path.Combine(FilePath, fileName);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        model.Attachmentfile.CopyTo(fs);
                    }
                }

                user.USER_EMAIL = model.User_Email;
                user.USER_PASSWORD = model.User_Password;
                var filepath = "/image/";
                user.ATTACHMENTURL = filepath + model.Attachmentfile.FileName;

                await _loginDbContext.SaveChangesAsync();
                TempData["Message"] = "Profile Updated Successfully , its shows when you Login Next Time";
                return RedirectToAction("Customers_Car_Inventory", "Admin");
            }

            TempData["Message"] = "Details Not Update";
            return RedirectToAction("UserProfileUpdate", "User");
        }

        [HttpGet]
        public IActionResult Customer_PendingRentals()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            List<Pending_RentalsModel> Rentals = new List<Pending_RentalsModel>();
            Rentals = _pendingRentalsService.GetRentals();
            return View(Rentals);

        }

        public IActionResult Footer_About()
        {
            string? UserName = HttpContext.Session.GetString("UserEmail");
            string? UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Login_Form");
            }

            return View();
        }

        public IActionResult Forget_Password()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget_Password1(String ReciverMail)
        {

            if (ReciverMail != null)
            {
                TempData["usermail"] = ReciverMail;
                var time = DateTime.Now.Minute.ToString();
                TempData["time"] = time;
                var MAIL = _loginDbContext.Login.Where(x => x.USER_EMAIL == ReciverMail).FirstOrDefault();
                try
                {
                    if (MAIL != null)
                    {
                        string fromMail = "123thakurjangid@gmail.com";
                        string fromPassword = "fdxbcnpxstughuml";

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(fromMail);
                        message.Subject = "Car Rental Web Application.";
                        message.To.Add(new MailAddress(ReciverMail));
                        message.Body = "<html><body> <h1>One time link</h1> <p> For reset your passw.. simply click on this link <p/> <p> https://localhost:7243/user/Reset_Passw <p/> <p> This link expire after 5minutes.<p/> </body></html>";
                        message.IsBodyHtml = true;

                        var smtpclient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };

                        smtpclient.Send(message);
                        TempData["Message"] = "Check yout email to reset your password";
                        return RedirectToAction("Customer_Home_Page", "Admin");
                    }
                    else
                    {
                        TempData["Message"] = "Mail Does not exist in database !";
                        return RedirectToAction("Forget_Password", "User");
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Request fail to send! Check Your Internet Connection ! Or field Value should be null";
                    return RedirectToAction("Forget_Password", "User");
                }
            }

            return View();
        }

        public IActionResult Reset_Passw()
        {
            int Expire_time = int.Parse(TempData["time"].ToString())+2;
            var updated_time = DateTime.Now.Minute.ToString();
            int now_time = int.Parse(updated_time);
            if (now_time > Expire_time)
            {
                TempData["Message"] = "Link expire ";
                return RedirectToAction("Forget_Password", "User");
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reset_Passw1(string newpassword1, string newpassword2)
        {
            if (newpassword1 == newpassword2)
            {
                var mail = TempData["usermail"];

                var db = _loginDbContext.Login.Where(x => x.USER_EMAIL == mail).FirstOrDefault();

                db.USER_PASSWORD = newpassword1;
                _loginDbContext.SaveChanges();
                TempData["Message"] = "Password Successfully Changed";
                return RedirectToAction("Login_Form");
            }
            else
            {
                TempData["Message"] = "Both filds value must be same";
                return RedirectToAction("Reset_Passw");
            }
        }
    }
}
