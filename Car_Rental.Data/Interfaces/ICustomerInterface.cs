﻿using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Interfaces
{
    public interface ICustomerInterface
    {
        public bool SaveCustomer(Customer customer);
    }
}
