using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Interfaces
{
    public interface IPendingRentalsInterface
    {
        bool Delete(int id);
        List<Pending_Rental> GetRentals();
        public bool SaveRentals(Pending_Rental pending_Rental);
    }
}
