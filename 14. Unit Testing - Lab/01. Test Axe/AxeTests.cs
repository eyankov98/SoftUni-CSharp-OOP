using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeShouldInitlizeWithCorrectValues()
        {
            // Arrange and Act
            Axe axe = new Axe(100, 100);

            //Assert
            Assert.AreEqual(100, axe.DurabilityPoints);
            Assert.AreEqual(100, axe.AttackPoints);
        }

        [Test]
        public void AttackMethodShouldDecreaseDurabilityPoints()
        {
            //Arrange
            Dummy target = new Dummy(10, 10);
            Axe axe = new Axe(100, 10);

            //Act
            axe.Attack(target);

            //Assert
            Assert.AreEqual(9, axe.DurabilityPoints);
        }

        [Test]
        public void AttackMethodShouldThrowAnExceptionIfDurabilityIsZero()
        {
            //Arrange
            Dummy dummy = new Dummy(20, 100);
            Axe axe = new Axe(10, 1);

            //Act
            axe.Attack(dummy);

            //Assert
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy), "Axe is broken.");
        }
    }
}