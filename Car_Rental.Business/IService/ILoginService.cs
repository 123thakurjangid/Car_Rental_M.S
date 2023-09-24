using Car_Rental.Business.Model;
using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface ILoginService
    {
       bool AddUser(LoginModel loginModel);
       Login? login(LoginModel model);
    }
}
