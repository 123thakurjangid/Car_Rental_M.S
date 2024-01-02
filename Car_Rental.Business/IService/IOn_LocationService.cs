﻿using Car_Rental.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.IService
{
    public interface IOn_LocationService
    {
        bool AddLocation(LocationModel location_Model);
        List<LocationModel> Get_location_lists();
    }
}
