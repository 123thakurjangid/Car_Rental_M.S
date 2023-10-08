using Car_Rental.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface IPendingRentals_Service
    {
        public bool AddRentals(Pending_RentalsModel model);
        bool Delete(int id);
        List<Pending_RentalsModel> GetRentals();
    }
}
