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
    public class On_LocationRepository : IOn_Location_Interface
    {
        private readonly LoginDbContext _context;

        public On_LocationRepository()
        {
            _context = new LoginDbContext();
        }

        public List<On_Location> getlocation()
        {
            return _context.On_Location.ToList();
        }

        public bool SaveLocation(On_Location location)
        {
            _context.On_Location.Add(location);
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
