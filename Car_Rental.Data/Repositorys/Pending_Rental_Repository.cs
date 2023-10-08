using Car_Rental.Data.DbContexts;
using Car_Rental.Data.Entities;
using Car_Rental.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Repositorys
{
    public class Pending_Rental_Repository : IPendingRentalsInterface
    {
        private readonly LoginDbContext _context;

        public Pending_Rental_Repository()
        {
            _context = new LoginDbContext();
        }

        public bool Delete(int id)
        {
            var Rental = _context.Pending_Rental.Find(id);
            if (Rental != null)
            {
                _context.Pending_Rental.Remove(Rental);
                return _context.SaveChanges() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public List<Pending_Rental> GetRentals()
        {
            return _context.Pending_Rental.ToList();
        }

        public bool SaveRentals(Pending_Rental rental)
        {
            _context.Pending_Rental.Add(rental);
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
