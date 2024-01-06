using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballTeam footballTeam;

        [SetUp]
        public void Setup()
        {
            footballTeam = new FootballTeam("Liverpool", 16);
        }

        [Test]
        public void FootballTeamClassConstructorTest()
        {
            Assert.AreEqual("Liverpool", footballTeam.Name);
            Assert.AreEqual(16, footballTeam.Capacity);
            Assert.IsNotNull(footballTeam.Players);
            Assert.AreEqual(0, footballTeam.Players.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void FootballTeamClassNamePropertyTest(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam(name, 16));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [TestCase(14)]
        [TestCase(0)]
        [TestCase(-1)]
        public void FootballTeamClassCapacityPropertyTest(int capacity)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam("Liverpool", capacity));

            Assert.AreEqual("Capacity min value = 15", exception.Message);
        }

        [Test]
        public void FootballTeamClassAddNewPlayerMethodTestShouldWorkCorrectly()
        {
            Assert.AreEqual("Added player Edgar in position Forward with number 9", footballTeam.AddNewPlayer(new FootballPlayer("Edgar", 9, "Forward")));
            Assert.AreEqual(1, footballTeam.Players.Count);
        }

        [Test]
        public void FootballTeamClassAddNewPlayerMethodTestReturnCorrectMessageIfTryingToAddMoreFootballPlayersThenFootballTeamCapacity()
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Ivan", 1, "Goalkeeper");
            FootballPlayer footballPlayer2 = new FootballPlayer("Georgi", 2, "Goalkeeper");
            FootballPlayer footballPlayer3 = new FootballPlayer("Bojidar", 3, "Goalkeeper");
            FootballPlayer footballPlayer4 = new FootballPlayer("Dimitur", 4, "Midfielder");
            FootballPlayer footballPlayer5 = new FootballPlayer("Boris", 5, "Midfielder");
            FootballPlayer footballPlayer6 = new FootballPlayer("Ivailo", 6, "Midfielder");
            FootballPlayer footballPlayer7 = new FootballPlayer("Krum", 7, "Midfielder");
            FootballPlayer footballPlayer8 = new FootballPlayer("Krasimir", 8, "Midfielder");
            FootballPlayer footballPlayer9 = new FootballPlayer("Edgar", 9, "Forward");
            FootballPlayer footballPlayer10 = new FootballPlayer("Aleksandur", 10, "Forward");
            FootballPlayer footballPlayer11 = new FootballPlayer("Martin", 11, "Forward");
            FootballPlayer footballPlayer12 = new FootballPlayer("Petur", 12, "Midfielder");
            FootballPlayer footballPlayer13 = new FootballPlayer("Plamen", 13, "Midfielder");
            FootballPlayer footballPlayer14 = new FootballPlayer("Yordan", 14, "Midfielder");
            FootballPlayer footballPlayer15 = new FootballPlayer("Vladimir", 15, "Goalkeeper");
            FootballPlayer footballPlayer16 = new FootballPlayer("Atanas", 16, "Goalkeeper");
            FootballPlayer footballPlayer17 = new FootballPlayer("Rangel", 17, "Goalkeeper");

            footballTeam.AddNewPlayer(footballPlayer1);
            footballTeam.AddNewPlayer(footballPlayer2);
            footballTeam.AddNewPlayer(footballPlayer3);
            footballTeam.AddNewPlayer(footballPlayer4);
            footballTeam.AddNewPlayer(footballPlayer5);
            footballTeam.AddNewPlayer(footballPlayer6);
            footballTeam.AddNewPlayer(footballPlayer7);
            footballTeam.AddNewPlayer(footballPlayer8);
            footballTeam.AddNewPlayer(footballPlayer9);
            footballTeam.AddNewPlayer(footballPlayer10);
            footballTeam.AddNewPlayer(footballPlayer11);
            footballTeam.AddNewPlayer(footballPlayer12);
            footballTeam.AddNewPlayer(footballPlayer13);
            footballTeam.AddNewPlayer(footballPlayer14);
            footballTeam.AddNewPlayer(footballPlayer15);
            footballTeam.AddNewPlayer(footballPlayer16);

            Assert.AreEqual("No more positions available!", footballTeam.AddNewPlayer(footballPlayer17));
        }

        [Test]
        public void FootballTeamClassPickPlayerMethodTest()
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Edgar", 9, "Forward");
            FootballPlayer footballPlayer2 = new FootballPlayer("Ivan", 10, "Forward");

            footballTeam.AddNewPlayer(footballPlayer1);
            footballTeam.AddNewPlayer(footballPlayer2);

            FootballPlayer expectedSearchedPlayer = footballTeam.PickPlayer("Edgar");

            FootballPlayer actualSearchedPlayer = footballPlayer1;

            Assert.AreEqual(expectedSearchedPlayer, actualSearchedPlayer);
        }

        [Test]
        public void FootballTeamClassPlayerScoreMethodTest()
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Edgar", 9, "Forward");
            FootballPlayer footballPlayer2 = new FootballPlayer("Ivan", 10, "Forward");

            footballTeam.AddNewPlayer(footballPlayer1);
            footballTeam.AddNewPlayer(footballPlayer2);

            Assert.AreEqual("Edgar scored and now has 1 for this season!", footballTeam.PlayerScore(9));

            Assert.AreEqual(1, footballPlayer1.ScoredGoals);
        }
    }
}