using Car_Rental.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface IRentals_History_Service
    {
        bool Add_Rentals_History(Pending_RentalsModel model);
        List<Pending_RentalsModel> Get_History();
    }
}
