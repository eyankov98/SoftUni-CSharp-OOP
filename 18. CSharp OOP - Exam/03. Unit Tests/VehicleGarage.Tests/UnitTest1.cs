using NUnit.Framework;
using System.Collections.Generic;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Garage garage;

        [SetUp]
        public void Setup()
        {
            garage = new Garage(3);
        }

        [Test]
        public void VehicleClassConstructorTest()
        {
            Vehicle vehicle = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");

            Assert.IsNotNull(vehicle);
            Assert.AreEqual("Mercedes-Benz", vehicle.Brand);
            Assert.AreEqual("S Class", vehicle.Model);
            Assert.AreEqual("PB9706TX", vehicle.LicensePlateNumber);
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void GarageClassConstructorTest()
        {
            Assert.IsNotNull(garage);
            Assert.AreEqual(3, garage.Capacity);
            Assert.IsNotNull(garage.Vehicles);
            Assert.That(garage.Vehicles.GetType() == typeof(List<Vehicle>));
            Assert.AreEqual(0, garage.Vehicles.Count);
        }

        [Test]
        public void GarageClassAddVehicleMethodTest()
        {
            Assert.IsTrue(garage.AddVehicle(new Vehicle("Mercedes-Benz", "S Class", "PB9706TX")));
            Assert.IsTrue(garage.AddVehicle(new Vehicle("Audi", "A8", "PB9080HA")));
            Assert.IsTrue(garage.AddVehicle(new Vehicle("Audi", "A6", "PB7060KH")));
            Assert.IsFalse(garage.AddVehicle(new Vehicle("Mercedes-Benz", "E Class", "PB5040TH"))); // is false because thats the fourth car whos i need to added but capacity is only for 3 cars
            Assert.IsFalse(garage.AddVehicle(new Vehicle("VW", "Golf 6", "PB9706TX"))); // is false because ("VW", "Golf 6", "PB9080CA") LicensePlateNumber is exist in collection ("Mercedes-Benz", "E Class", "PB9080CA")
            Assert.AreEqual(3, garage.Vehicles.Count);
        }

        [Test]
        public void GarageClassChargeVehiclesMethodTest()
        {
            Vehicle vehicle1 = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");
            Vehicle vehicle2 = new Vehicle("Mercedes-Benz", "E Class", "PB5040TH");
            Vehicle vehicle3 = new Vehicle("Audi", "A8", "PB9080HA");

            vehicle1.BatteryLevel = 45;
            vehicle2.BatteryLevel = 45;
            vehicle3.BatteryLevel = 70;

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            Assert.AreEqual(2, garage.ChargeVehicles(55));
            Assert.AreEqual(100, vehicle1.BatteryLevel);
            Assert.AreEqual(100, vehicle2.BatteryLevel);
            Assert.AreEqual(70, vehicle3.BatteryLevel);
        }

        [Test]
        public void GarageClassDriveVehicleMethodTestShouldDecreaseTheCarsBatteryLevel()
        {
            Vehicle vehicle = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");

            garage.AddVehicle(vehicle);


            garage.DriveVehicle("PB9706TX", 30, false);

            Assert.AreEqual(70, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageClassDriveVehicleMethodTestIfAccidentHappend() // IsDamaged == true
        {
            Vehicle vehicle1 = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");
            Vehicle vehicle2 = new Vehicle("Mercedes-Benz", "E Class", "PB5040TH");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("PB9706TX", 20, true); // vehicle1
            garage.DriveVehicle("PB5040TH", 30, false); // vehicle2

            Assert.AreEqual(80, vehicle1.BatteryLevel);
            Assert.IsTrue(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]
        public void GarageClassDriveVehicleMethodShouldntDoAnythingWhenIsDamaged()
        {
            Vehicle vehicle1 = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");
            Vehicle vehicle2 = new Vehicle("Mercedes-Benz", "E Class", "PB5040TH");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("PB9706TX", 20, true); // vehicle1
            garage.DriveVehicle("PB5040TH", 30, false); // vehicle2

            Assert.AreEqual(70, vehicle2.BatteryLevel);
        }

        [Test]
        public void GarageClassDriveVehicleMethodShouldntDoAnythingWhenWhenBatteryDrainageIsOver100()
        {
            Vehicle vehicle = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");

            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB9706TX", 101, false);

            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageClassDriveVehicleMethodShouldntDoAnythingWhenBatteryLevelIsLessThanBatteryDrainage()
        {
            Vehicle vehicle = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");

            vehicle.BatteryLevel = 20;

            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB9706TX", 21, false);

            Assert.AreEqual(20, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageClassRepairVehiclesMethodTest()
        {
            Vehicle vehicle1 = new Vehicle("Mercedes-Benz", "S Class", "PB9706TX");
            Vehicle vehicle2 = new Vehicle("Mercedes-Benz", "E Class", "PB5040TH");
            Vehicle vehicle3 = new Vehicle("Audi", "A8", "PB9080HA");

            vehicle1.IsDamaged = true;
            vehicle2.IsDamaged = true;
            vehicle3.IsDamaged = false;

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            Assert.AreEqual("Vehicles repaired: 2", garage.RepairVehicles());
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
            Assert.IsFalse(vehicle3.IsDamaged);
        }
    }
}