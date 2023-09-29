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
    public class CarRepository : ICarInterface
    {
        private readonly LoginDbContext _context;
        public CarRepository() 
        {
            _context = new LoginDbContext();
        }
        public bool SaveCar(Car car)
        {
            _context.Car.Add(car);
            return _context.SaveChanges() > 0?true:false;
        }
        public List<Car> getcars( )
        {
            return _context.Car.ToList();
        }

        public bool Delete(int Id)
        {
            var Car = _context.Car.Find(Id);

            if( Car != null )
            {
                _context.Car.Remove(Car);
                return _context.SaveChanges()>0?true:false;
            }
            else
            {
                return false;
            }
        }
    }
}
