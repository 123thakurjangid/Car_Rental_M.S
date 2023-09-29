using Azure.Core;
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
    public class LoginService : ILoginService
    {
        private readonly ILoginInterface _loginInterface;
        public LoginService() 
        {
            _loginInterface = new LoginRepository();
        }

        public bool AddUser(LoginModel loginModel)
        {
            bool result = false;

            Login login = new Login();

            login.ID = loginModel.Id;
            login.USER_EMAIL = loginModel.User_Email;
            login.USER_PASSWORD = loginModel.User_Password;

            result = _loginInterface.SaveUser(login);

            return result;
        }

        public List<LoginModel> GetUsers()
        {
            return _loginInterface.GetCustomer().Select(x => new LoginModel
            {
                Id = x.ID,
                User_Email = x.USER_EMAIL,
                User_Password = x.USER_PASSWORD
            }).ToList();
        }

        public Login? login(LoginModel loginModel)
        {
            Login? result = null;
            Login login = new Login();

            login.ID = loginModel.Id;
            login.USER_EMAIL = loginModel.User_Email;
            login.USER_PASSWORD = loginModel.User_Password;

            result = _loginInterface.login(login);
            return result;
        }
    }
}
