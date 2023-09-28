using Car_Rental.Business.IService;
using Car_Rental.Business.Model;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using Car_Rental.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerInterface customerInterface;
        public CustomerService() 
        {
            customerInterface = new CustomerRepository();
        }

        public bool AddCustomer(CustomerModel customer)
        {
            var result = false;

            Customer model = new Customer();

            model.CUSTOMER_ID = customer.Customer_Id;
            model.CUSTOMER_NAME = customer.Customer_Name;
            model.CUSTOMER_ADDRESS = customer.Customer_Address;
            model.CUSTOMER_PHONE = customer.Customer_Phone;
            model.CUSTOMER_CITY = customer.Customer_City;

            customerInterface.SaveCustomer(model);
            return result;
        }
    }
}
