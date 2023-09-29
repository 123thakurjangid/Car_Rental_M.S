using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Interfaces
{
    public interface ILoginInterface
    {
        List<Login> GetCustomer();
        Login? login(Login login);
        public bool SaveUser(Login login);
    }
}
