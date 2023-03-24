namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car megane;
        [SetUp]
        public void CreateBaseCar()
        {
            megane = new Car("Renault", "Megane", 4.4, 69);
        }

        [TearDown]
        public void DeleteCreatedCar()
        {
            megane = null;
        }

        [Test]
        public void TestConstructors()
        {
            megane = new Car("Renault", "Megane", 4.4, 69);

            Assert.AreEqual("Renault", megane.Make);
            Assert.AreEqual("Megane", megane.Model);
            Assert.AreEqual(4.4, megane.FuelConsumption);
            Assert.AreEqual(69, megane.FuelCapacity);
            Assert.AreEqual(0, megane.FuelAmount);
        }

        [Test]
        public void IfMakeIsNullOrEmpty()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => megane = new Car(null, "Megane", 4.4, 69));
            Assert.That(exception.Message, Is.EqualTo("Make cannot be null or empty!"));

            ArgumentException emptyEx = Assert.Throws<ArgumentException>(() => megane = new Car(string.Empty, "Megane", 4.4, 69));
            Assert.That(emptyEx.Message, Is.EqualTo("Make cannot be null or empty!"));
        }

        [Test]
        public void IfModelIsNullOrEmpty()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", null, 4.4, 69));
            Assert.That(exception.Message, Is.EqualTo("Model cannot be null or empty!"));

            ArgumentException emptyEx = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", string.Empty, 4.4, 69));
            Assert.That(emptyEx.Message, Is.EqualTo("Model cannot be null or empty!"));
        }


        [Test]
        public void IfConsumptionIsZeroOrNegative()
        {
            ArgumentException zeroException = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", "Megane", 0, 69));
            Assert.That(zeroException.Message, Is.EqualTo("Fuel consumption cannot be zero or negative!"));

            ArgumentException negativeEx = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", "Megane", -1, 69));
            Assert.That(negativeEx.Message, Is.EqualTo("Fuel consumption cannot be zero or negative!"));
        }

        //[Test]
        //public void IfFuelIsNegative()
        //{
        //    ArgumentException exception = Assert.Throws<ArgumentException>(() => megane.Drive(200));
        //    Assert.That(exception.Message, Is.EqualTo("Fuel amount cannot be negative!"));
        //}

        [Test]
        public void IfCapacityIsZeroOrNegative()
        {
            ArgumentException zeroException = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", "Megane", 4.4, 0));
            Assert.That(zeroException.Message, Is.EqualTo("Fuel capacity cannot be zero or negative!"));

            ArgumentException negativeEx = Assert.Throws<ArgumentException>(() => megane = new Car("Renault", "Megane", 4.4, -1));
            Assert.That(negativeEx.Message, Is.EqualTo("Fuel capacity cannot be zero or negative!"));
        }

        [Test]
        public void IfRefuelThrowsZeroOrNegativeMessage()
        {
            ArgumentException zeroException = Assert.Throws<ArgumentException>(() => megane.Refuel(0));
            Assert.That(zeroException.Message, Is.EqualTo("Fuel amount cannot be zero or negative!"));

            ArgumentException negativeEx = Assert.Throws<ArgumentException>(() => megane.Refuel(-1));
            Assert.That(negativeEx.Message, Is.EqualTo("Fuel amount cannot be zero or negative!"));
        }

        [Test]
        public void IfFuelAmountIncrements()
        {
            megane.Refuel(20);

            Assert.AreEqual(20, megane.FuelAmount);
        }

        [Test]
        public void IfFuelIsGreaterThanCapacity()
        {
            megane.Refuel(100);

            Assert.AreEqual(megane.FuelAmount, megane.FuelCapacity);
        }

        [Test]
        public void IfEnoughFuelToDrive()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => megane.Drive(100));
            Assert.That(exception.Message, Is.EqualTo("You don't have enough fuel to drive!"), "You can go");
        }

        [Test]
        public void IfFuelDecrementsCorrectly()
        {
            double distance = 200;
            double fuelToRefuel = 69;
            megane.Refuel(fuelToRefuel);
            double initialFuel = megane.FuelAmount;
            double fuelNeeded = (distance / 100) * megane.FuelConsumption;

            megane.Drive(distance);

            Assert.AreEqual(initialFuel - fuelNeeded, megane.FuelAmount);
        }
    }
}