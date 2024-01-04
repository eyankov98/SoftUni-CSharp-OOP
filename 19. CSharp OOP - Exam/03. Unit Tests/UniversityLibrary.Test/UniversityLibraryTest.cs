namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Linq;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextBookConstructorShouldWorkCorrectly()
        {
            TextBook textBook = new TextBook("The women", "Edgar", "Drama");

            string expectedName = "The women";
            string expectedAuthor = "Edgar";
            string expectedCategory = "Drama";
            int expectedInventoryNumber = 0;
            string expectedHolder = null;

            Assert.AreEqual(expectedName, textBook.Title);
            Assert.AreEqual(expectedAuthor, textBook.Author);
            Assert.AreEqual(expectedCategory, textBook.Category);
            Assert.AreEqual(expectedInventoryNumber, textBook.InventoryNumber);
            Assert.AreEqual(expectedHolder, textBook.Holder);
        }

        [Test]
        public void TextBookToStringMethodShouldWorkCorrectly()
        {
            string title = "The women";
            string author = "Edgar";
            string category = "Drama";
            int inventoryNumber = 25;

            TextBook textBook = new TextBook(title, author, category);
            textBook.InventoryNumber = inventoryNumber;

            StringBuilder expectedString = new StringBuilder();
            expectedString.AppendLine($"Book: {title} - {inventoryNumber}");
            expectedString.AppendLine($"Category: {category}");
            expectedString.AppendLine($"Author: {author}");

            string actualString = textBook.ToString();

            Assert.AreEqual(expectedString.ToString().TrimEnd(), actualString);
        }

        [Test]
        public void UniversityLibraryCatalogPropertyShouldWorkCorrectly()
        {
            TextBook textBook1 = new TextBook("The women", "Edgar", "Drama");
            TextBook textBook2 = new TextBook("The women2", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();
            
            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);

            Assert.IsTrue(library.Catalogue.Contains(textBook1));
        }


        [Test]
        public void AddTextBookToLibraryMethodCheckInventoryNumbersCounterWorksCorrectly()
        {
            TextBook textBook1 = new TextBook("The women", "Edgar", "Drama");
            TextBook textBook2 = new TextBook("The women2", "Edgar", "Drama");
            TextBook textBook3 = new TextBook("The women3", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            var expectedResult = textBook3.InventoryNumber;

            var actualResult = 3;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AddTextBookToLibraryMethodCheckAddBooksToCollectionCorrectly()
        {
            TextBook textBook = new TextBook("The women", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();

            string expectedString = library.AddTextBookToLibrary(textBook);

            string actualString = textBook.ToString();

            var expectedCount = library.Catalogue.Count;

            var actualCount = 1;

            Assert.AreEqual(expectedCount, actualCount);
            Assert.IsTrue(library.Catalogue.Contains(textBook));
            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void LoanTextBookMethodCheckWorkCorrectlyWhenHolderIsNotEqualToStudentName()
        {
            TextBook textBook1 = new TextBook("The women", "Edgar", "Drama");
            TextBook textBook2 = new TextBook("The women2", "Edgar", "Drama");
            TextBook textBook3 = new TextBook("The women3", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            var actualResult = library.LoanTextBook(3, "Ivan");

            var expectedResult = $"{textBook3.Title} loaned to Ivan.";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(textBook3.Holder, "Ivan");
        }

        [Test]
        public void LoanTextBookMethodCheckWorkCorrectlyWhenHolderIsEqualToStudentName()
        {
            TextBook textBook = new TextBook("The women", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBook);

            library.LoanTextBook(1, "Ivan");

            var actualResult = library.LoanTextBook(1, "Ivan");

            var expectedResult = $"Ivan still hasn't returned {textBook.Title}!";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ReturnTextBookMethodCheckWorkCorrectly()
        {
            TextBook textBook = new TextBook("The women", "Edgar", "Drama");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(textBook);

            library.LoanTextBook(1, "Ivan");

            var actualResult = library.ReturnTextBook(1);

            var expectedResult = $"{textBook.Title} is returned to the library.";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(textBook.Holder, string.Empty);
        }
    }
}