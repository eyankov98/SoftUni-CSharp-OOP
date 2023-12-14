namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void WarriorConstructorShouldWorkCorrectly()
        {
            string expectedName = "Edgar";
            int expectedDamage = 15;
            int expectedHP = 100;

            Warrior warrior = new Warrior(expectedName, expectedDamage, expectedHP);

            Assert.AreEqual(expectedName, warrior.Name);
            Assert.AreEqual(expectedDamage, warrior.Damage);
            Assert.AreEqual(expectedHP, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void OurWarriorConstructorShouldThrowExceptionIfNameIsNullOrWhiteSpace(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior(name, 25, 50));

            string expectedMessage = "Name should not be empty or whitespace!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-20)]
        public void OurWarriorConstructorShouldThrowExceptionIfDamageIsNotPositive(int damage)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior("Edgar", damage, 50));

            string expectedMessage = "Damage value should be positive!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(-1)]
        [TestCase(-20)]
        public void OurWarriorConstructorShouldThrowExceptionIfHPIsNegative(int hp)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior("Edgar", 25, hp));

            string expectedMessage = "HP should not be negative!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(30)]
        [TestCase(10)]
        public void OurWarriorShouldThrowInvalidExceptionIfHisHPIsEqualOrLessThan30(int hp)
        {
            Warrior attacker = new Warrior("Edgar", 10, hp);
            Warrior defender = new Warrior("Pesho", 5, 90);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            string expectedMessage = "Your HP is too low in order to attack other warriors!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(30)]
        [TestCase(10)]
        public void OtherWarriorShouldThrowInvalidExceptionIfHisHPIsEqualOrLessThan30(int hp)
        {
            Warrior attacker = new Warrior("Edgar", 10, 90);
            Warrior defender = new Warrior("Pesho", 5, hp);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            string expectedMessage = $"Enemy HP must be greater than 30 in order to attack him!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void OurWarriorShouldThrowInvalidExceptionIfHisHPIsLessThanOtherWarrior()
        {
            Warrior attacker = new Warrior("Edgar", 10, 35);
            Warrior defender = new Warrior("Pesho", 45, 90);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));

            string expectedMessage = $"You are trying to attack too strong enemy";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void OurWarriorDamageIsMoreThanOtherWarriorHP()
        {
            Warrior attacker = new Warrior("Edgar", 50, 100);
            Warrior defender = new Warrior("Pesho", 45, 40);

            attacker.Attack(defender);

            int expectedAttackerHp = 55;
            int expectedDefenderHp = 0;

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }

        [Test]
        public void OurWarriorDamageIsLessThanOtherWarriorHP()
        {
            int expectedAttackerHp = 95;
            int expectedDefenderHp = 80;

            Warrior attacker = new Warrior("Edgar", 10, 100);
            Warrior defender = new Warrior("Pesho", 5, 90);

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        } 
    }
}