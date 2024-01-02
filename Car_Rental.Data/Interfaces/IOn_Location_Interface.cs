using Car_Rental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.Interfaces
{
    public interface IOn_Location_Interface
    {
        List<On_Location> getlocation();
        public bool SaveLocation(On_Location _Location);
    }
}
