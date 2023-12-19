using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;
        
        [SetUp]
        public void Setup()
        {
            coffeeMat = new CoffeeMat(100, 3);
        }

        [Test]
        public void CoffeeMatConstructorTest()
        {
            Assert.AreEqual(100, coffeeMat.WaterCapacity);
            Assert.AreEqual(3, coffeeMat.ButtonsCount);
            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void CoffeeMatFillWaterTankMethodTest()
        {
            Assert.AreEqual($"Water tank is filled with {coffeeMat.WaterCapacity}ml", coffeeMat.FillWaterTank());

            Assert.AreEqual("Water tank is already full!", coffeeMat.FillWaterTank());
        }

        [Test]
        public void CoffeeMatAddDrinkMethodTest()
        {
            Assert.IsTrue(coffeeMat.AddDrink("Espresso", 2.50));
            Assert.IsTrue(coffeeMat.AddDrink("Cappuccino", 3.50));
            Assert.IsTrue(coffeeMat.AddDrink("Latte", 4.50));
            Assert.IsFalse(coffeeMat.AddDrink("Frappe", 4.50));
            Assert.IsFalse(coffeeMat.AddDrink("Espresso", 2.50));
        }

        [Test]
        public void CoffeeMatBuyDrinkMethodTest()
        {
            new CoffeeMat(75, 3);
            coffeeMat.AddDrink("Espresso", 2.50);
            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("Espresso"));
            
            coffeeMat.FillWaterTank();
            Assert.AreEqual("Cappuccino is not available!", coffeeMat.BuyDrink("Cappuccino"));

            Assert.AreEqual("Your bill is 2.50$", coffeeMat.BuyDrink("Espresso"));
            
            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("Milk"));

            Assert.AreEqual(2.50, coffeeMat.Income);
        }

        [Test]
        public void CoffeeMatCollectIncomeMethod()
        {
            new CoffeeMat(85, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Espresso", 2.50);
            coffeeMat.BuyDrink("Espresso");

            Assert.AreEqual(coffeeMat.CollectIncome(), 2.50);

            Assert.AreEqual(0, coffeeMat.Income);
        }
    }
}