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
    public class LoginRepository : ILoginInterface
    {
        private readonly LoginDbContext _context;
        public LoginRepository()
        {
            _context = new LoginDbContext();
        }

        public Login? login(Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.USER_EMAIL) || string.IsNullOrEmpty(login.USER_PASSWORD))
            {
                return null;
            }
            var data = _context.Login.Where(x => x.USER_EMAIL == login.USER_EMAIL && x.USER_PASSWORD == login.USER_PASSWORD).FirstOrDefault();
            return data;
        }

        public bool SaveUser(Login login)
        {
            _context.Login.Add(login);
            return _context.SaveChanges()>0?true:false;
        }
    }
}