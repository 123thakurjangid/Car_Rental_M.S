using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Interfaces
{
    public interface IRentals_History_Interface
    {
        public List<Rentals_History> Get_History();
        public bool Save_Rentals_History(Rentals_History rentals_History);
    } 
}
