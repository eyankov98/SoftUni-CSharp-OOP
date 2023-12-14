namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        [SetUp]
        public void SetUp()
        {
            Person[] persons =
            {
                new Person(1, "Edgar"),
                new Person(2, "Georgi"),
                new Person(3, "Radi_Yankov"),
                new Person(4, "Martin_Ivanov"),
                new Person(5, "Olya_Sedmakova"),
                new Person(6, "Gabriela_Boneva"),
                new Person(7, "Ivan_Ivanov"),
                new Person(8, "Kalin_Petrov"),
                new Person(9, "Bojidar_Nikolov"),
                new Person(10, "Milka_Karamiteva"),
                new Person(11, "Todor"),
                new Person(12, "Jivko")
            };

            database = new Database(persons);
        }

        [Test]
        public void DatabaseCountShouldWorkCorrectly()
        {
            int expectedCount = 12;

            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test] 
        public void DatabaseAddRangeMethodShouldThrowExceptionIfLenghtIsMoreThen16()
        {
            Person[] persons =
            {
                new Person(1, "Edgar"),
                new Person(2, "Georgi"),
                new Person(3, "Radi_Yankov"),
                new Person(4, "Martin_Ivanov"),
                new Person(5, "Olya_Sedmakova"),
                new Person(6, "Gabriela_Boneva"),
                new Person(7, "Ivan_Ivanov"),
                new Person(8, "Kalin_Petrov"),
                new Person(9, "Bojidar_Nikolov"),
                new Person(10, "Milka_Karamiteva"),
                new Person(11, "Todor"),
                new Person(12, "Jivko"),
                new Person(13, "Atanas"),
                new Person(14, "Rangel"),
                new Person(15, "Spiridon"),
                new Person(16, "Aleksandur"),
                new Person(17, "Krasimir")
            };

            ArgumentException exception = Assert.Throws<ArgumentException>(() => database = new Database(persons));

            string expectedMessage = "Provided data length should be in range [0..16]!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void DatabaseAddRangeMethodCountShouldWorkCorrectly()
        {
            int expectedCount = 12;

            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void DatabaseAddMethodShouldThrowExceptionICountIsMoreThan16()
        {
            Person person1 = new Person(13, "Ivo");
            Person person2 = new Person(14, "Petur");
            Person person3 = new Person(15, "Hristo");
            Person person4 = new Person(16, "Nikolai");

            database.Add(person1);
            database.Add(person2);
            database.Add(person3);
            database.Add(person4);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(new Person(17, "Milen")));

            string expectedMessage = "Array's capacity must be exactly 16 integers!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test] 
        public void DatabaseAddMethodShouldThrowExceptionIfPersonWithSameUsernameIsAdded()
        {
            Person person = new Person(11, "Edgar");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(person));

            string expectedMessage = "There is already user with this username!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void DatabaseAddMethodShouldThrowExceptionIfPersonWithSameIdIsAdded()
        {
            Person person = new Person(1, "Antonio");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(person));

            string expectedMessage = "There is already user with this Id!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void DatabaseAddMethodShouldWorkCorrectly()
        {
            int expectedResult = 13;

            Person person = new Person(13, "Borislav");

            database.Add(person);

            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseRemoveMethodShouldThrowExceptionIfDatabaseIsEmpty()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void DatabaseRemoveMethodShouldWorkCorrectly()
        {
            int expectedResult = 11;

            database.Remove();

            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(null)]
        [TestCase("")]
        public void DatabaseFindByUsernameMethodShouldThrowExceptionIfUsernameIsNullOrEmpty(string username)
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(username));

            string expectedMessage = "Username parameter is null!";

            Assert.AreEqual(expectedMessage, exception.ParamName);
        }

        [TestCase("Kiril")]
        [TestCase("Plamen")]
        public void DatabaseFindByUsernameMethodShouldThrowExceptionIfUsernameIsNotFound(string username)
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.FindByUsername(username));

            string expectedMessage = "No user is present by this username!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void DatabaseFindByUsenameMethodShouldWorkCorrectly()
        {
            string expectedUsername = "Edgar";

            string actualUsername = database.FindByUsername("Edgar").UserName;

            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [TestCase(-1)]
        public void DatabaseFindByIdMethodShouldThrowExceptionIfIdIsNegative(int id)
        {
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(id));

            string expectedMessage = "Id should be a positive number!";

            Assert.AreEqual(expectedMessage, exception.ParamName);
        }

        [TestCase(20)]
        public void DatabaseFindByIdMethodShouldThrowExceptionIfIdIsNotFound(int id)
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.FindById(id));

            string expectedMessage = "No user is present by this ID!";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void DatabaseFindByIdMethodShouldWorkCorrectly()
        {
            string expectedResult = "Edgar";
            string actualResult = database.FindById(1).UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}