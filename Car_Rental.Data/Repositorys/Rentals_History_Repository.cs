using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Repositorys
{
    public class Rentals_History_Repository : IRentals_History_Interface
    {
        private readonly LoginDbContext _dbContext;

        public Rentals_History_Repository()
        {
            _dbContext = new LoginDbContext();
        }

        public List<Rentals_History> Get_History()
        {
            return _dbContext.Rentals_History.ToList();
        }

        public bool Save_Rentals_History(Rentals_History rentals_History)
        {
            _dbContext.Rentals_History.Add(rentals_History);
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
        
    }

}
