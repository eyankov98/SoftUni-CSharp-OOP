namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        [Test]
        public void PrivateCarConstructorShouldWorkCorrectly()
        {
            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        public void PublicCarConstructorShouldWorkCorrectly()
        {
            string expectedMake = "Mercedes";
            string expectedModel = "G Class 63";
            double expectedFuelConsumption = 10.5;
            double expectedFuelCapacity = 105.5;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarMakeMethodShouldThrowExceptionIfIsNullOrEmpty(string make)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Car(make, "G Class 63", 10.5, 105.5));

            string expectedMessage = "Make cannot be null or empty!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarModelMethodShouldThrowExceptionIfIsNullOrEmpty(string model)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Car("Mercedes", model, 10.5, 105.5));

            string expectedMessage = "Model cannot be null or empty!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CarFuelConsumptionMethodShouldThrowExceptionIfIsZeroOrNegative(double fuelConsumption)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Car("Mercedes", "G Class 63", fuelConsumption, 105.5));

            string expectedMessage = "Fuel consumption cannot be zero or negative!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void CarFuelAmountMethodShouldThrowExceptionIfIsNegative()
        {
            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            Assert.Throws<InvalidOperationException>(() => car.Drive(15), "Fuel amount cannot be negative!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CarFuelCapacityMethodShouldThrowExceptionIfIsZeroOrNegative(double fuelCapacity)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Car("Mercedes", "G Class 63", 10.5, fuelCapacity));

            string expectedMessage = "Fuel capacity cannot be zero or negative!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CarRefuelMethodShouldThrowExceptionIfIsZeroOrNegative(double fuelToRefuel)
        {
            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            ArgumentException exception = Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel));

            string expectedMessage = "Fuel amount cannot be zero or negative!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void CarRefuelMethodShouldIncreaseFuelAmount()
        {
            int expectedResult = 20;

            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            car.Refuel(20);

            double actualResutl = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResutl);
        }
        [Test]
        public void CarFuelAmountShouldNotBeMoreThenFuelCapacity()
        {
            double expectedResult = 105.5;

            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            car.Refuel(115.5);

            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarDriveMethodShouldDecreaseFuelAmount()
        {
            double expectedResult = 17.9;

            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            car.Refuel(20);
            car.Drive(20);

            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarDriveMethodShouldThrowExceptionIfFuelNeededIsMoreThanFuelAmount()
        {
            Car car = new Car("Mercedes", "G Class 63", 10.5, 105.5);

            car.Refuel(5);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => car.Drive(50));

            string expectedMessage = "You don't have enough fuel to drive!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}