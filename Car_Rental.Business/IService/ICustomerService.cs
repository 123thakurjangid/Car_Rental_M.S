using Car_Rental.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface ICustomerService
    {
        bool AddCustomer(CustomerModel customer);
        bool Delete(int id);
        List<CustomerModel> GetCustomer();
    }
}
