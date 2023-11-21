using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IVehicleFactory vehicleFactory;

        private readonly ICollection<IVehicle> vehicles;

        public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.vehicleFactory = vehicleFactory;

            vehicles = new List<IVehicle>();
        }

        public void Run()
        {
            IVehicle car = CreateVehicle();
            IVehicle truck = CreateVehicle();

            vehicles.Add(car);
            vehicles.Add(truck);

            int countOfCommands = int.Parse(reader.ReadLine());

            for (int i = 0; i < countOfCommands; i++)
            {
                try
                {
                    ProcessCommand();
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var vehicle in vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }
        }

        private IVehicle CreateVehicle()
        {
            string[] vehiclesInfo = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string typeOfVehicle = vehiclesInfo[0];
            double fuelQuantity = double.Parse(vehiclesInfo[1]);
            double fuelConsumption = double.Parse(vehiclesInfo[2]);

            IVehicle vehicle = vehicleFactory.Create(typeOfVehicle, fuelQuantity, fuelConsumption);

            return vehicle;
        }

        private void ProcessCommand()
        {
            string[] commandInfo = reader.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string typeOfCommand = commandInfo[0];
            string typeOfVehicle = commandInfo[1];

            IVehicle vehicle = vehicles.FirstOrDefault(v => v.GetType().Name == typeOfVehicle);

            if (vehicle == null)
            {
                throw new ArgumentException("Invalid vehicle type");
            }

            if (typeOfCommand == "Drive")
            {
                double distance = double.Parse(commandInfo[2]);
                writer.WriteLine(vehicle.Drive(distance));
            }
            else if (typeOfCommand == "Refuel")
            {
                double fuelAmount = double.Parse(commandInfo[2]);
                vehicle.Refuel(fuelAmount);
            }
        }
    }
}
