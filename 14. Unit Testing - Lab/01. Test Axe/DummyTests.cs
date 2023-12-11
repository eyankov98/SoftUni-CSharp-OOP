using NUnit.Framework;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void ConstructorShouldInitilizeCorrectly()
        {
            //Arrange and Act
            Dummy dummy = new Dummy(100, 100);

            //Assert
            Assert.AreEqual(100, dummy.Health);
        }

        [Test]
        public void TakeAttackShouldDecreaseHealth()
        {
            //Arrange
            Dummy dummy = new Dummy(100, 100);

            //Act
            dummy.TakeAttack(50);

            //Assert
            Assert.AreEqual(50, dummy.Health);
        }

        [Test]
        public void TakeAttackShouldThrowExceptionIfDummyIsDead()
        {
            //Arrange
            Dummy dummy = new Dummy(100, 100);

            //Act
            dummy.TakeAttack(50);
            dummy.TakeAttack(50);

            //Assert
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(50), "Dummy is dead.");
        }

        [Test]
        public void GiveExpShouldReturnCurrentExperienceIfDummyIsDead()
        {
            //Arrange
            Dummy dummy = new Dummy(100, 100);

            //Act
            dummy.TakeAttack(50);
            dummy.TakeAttack(50);

            //Assert
            Assert.AreEqual(100, dummy.GiveExperience());
        }

        [Test]
        public void GiveExpShouldThrowAnExceptionIfDummyIsNotDead()
        {
            //Arrange
            Dummy dummy = new Dummy(100, 100);

            //Act
            dummy.TakeAttack(50);

            //Assert
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Target is not dead.");
        }

        [Test]
        public void IsDeadShouldCheckIfHealthIsBelowOrEqualToZero()
        {
            //Arrange
            Dummy dummy = new Dummy(50, 100);

            //Act
            dummy.TakeAttack(50);

            //Assert
            Assert.IsTrue(dummy.IsDead());
        }
    }
}