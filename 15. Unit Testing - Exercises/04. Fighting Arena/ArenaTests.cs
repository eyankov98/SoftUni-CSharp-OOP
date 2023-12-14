namespace FightingArena.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Internal.Execution;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            this.arena = new Arena();
        }

        [Test]
        public void ArenaConstructorShouldWorkCorrectly()
        {
            Assert.IsNotNull(arena);
            Assert.IsNotNull(arena.Warriors);
        }
        
        [Test]
        public void ArenaCountShouldWorkCorrectly()
        {
            int expectedResult = 1;

            Warrior warrior = new Warrior("Edgar", 5, 100);

            arena.Enroll(warrior);

            Assert.IsNotEmpty(arena.Warriors);

            Assert.AreEqual(expectedResult, arena.Count);
        }

        [Test]
        public void ArenaEnrollShouldWorkCorrectly()
        {
            Warrior warrior = new Warrior("Edgar", 5, 100);

            arena.Enroll(warrior);

            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(warrior, arena.Warriors.Single());
        }

        [Test]
        public void ArenaEnrollShouldThrowInvalidExceptionIfWarriorIsAlreadyEnrolled()
        {
            Warrior warrior = new Warrior("Edgar", 5, 100);

            arena.Enroll(warrior);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior));

            string expectedMessage = "Warrior is already enrolled for the fights!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void ArenaFightMethodShouldWorkCorrectly()
        {
            Warrior attacker = new Warrior("Edgar", 15, 100);
            Warrior defender = new Warrior("Pesho", 5, 50);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            int expectedAttackerHp = 95;
            int expectedDefenderHp = 35;

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }

        [Test]
        public void ArenaFightShouldThrowInvalidExceptionIfAttackerNotFound()
        {
            Warrior attacker = new Warrior("Edgar", 15, 100);
            Warrior defender = new Warrior("Pesho", 5, 50);

            arena.Enroll(defender);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker.Name, defender.Name));

            string expectedMessage = $"There is no fighter with name {attacker.Name} enrolled for the fights!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void ArenaFightShouldThrowInvalidExceptionIfDefenderNotFound()
        {
            Warrior attacker = new Warrior("Edgar", 15, 100);
            Warrior defender = new Warrior("Pesho", 5, 50);

            arena.Enroll(attacker);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker.Name, defender.Name));

            string expectedMessage = $"There is no fighter with name {defender.Name} enrolled for the fights!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}
