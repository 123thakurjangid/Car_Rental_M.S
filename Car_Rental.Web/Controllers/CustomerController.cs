﻿using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Business.Service;
using Car_Rental.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController() 
        { 
            _customerService = new CustomerService();
        }
        public IActionResult Add_New_Customer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save_Customer(CustomerModel customers)
        {
            if(!ModelState.IsValid)
            {
                return View("Add_New_Customer", customers);
            }

            bool result = false;

            result = _customerService.AddCustomer(customers);

            if (result)
            {
                TempData["Message"] = "Customer Added Successfully";
                return Json("true");
            }
            else
            {
                TempData["Message"] = "Customer  Not Added !";
                return RedirectToAction("Add_New_Customer", "Admin");
            }
        }

        public IActionResult Edit_New_Customer()
        {
            return View();
        }
    }
}
