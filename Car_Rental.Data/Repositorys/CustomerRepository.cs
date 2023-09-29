﻿using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Repositorys
{
    public class CustomerRepository : ICustomerInterface
    {
        private readonly LoginDbContext _context;
        public CustomerRepository() 
        {
            _context = new LoginDbContext();
        }

        public List<Customer> GetCustomer()
        {
            return _context.Customer.ToList();
        }

        public bool SaveCustomer(Customer Customerli)
        {
            _context.Customer.Add(Customerli);
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
