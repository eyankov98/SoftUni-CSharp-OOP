﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Factories.Interfaces
{
    public interface IVehicleFactory
    {
        IVehicle Create(string typeOfVehicle, double fuelQuantity, double fuelConsumption);
    }
}
