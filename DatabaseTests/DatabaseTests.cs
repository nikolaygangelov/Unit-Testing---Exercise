namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
    //    private Database database;
    //    [SetUp]
    //    public void Setup()
    //    {
    //        database = new Database();
    //    }

    //    [TearDown]
    //    public void TearDown()
    //    {
    //        database = null;
    //    }

        [Test]
        public void IfArrayIs16IntegersLong()
        {
            int[] numbers = new int[17];

            Assert.Throws<InvalidOperationException>(() => new Database(numbers));
        }

        [Test]
        public void IfMethodAddPutAnElementInTheNextFreeCell()
        {
            int[] numbers = new int[16];

            Database database = new Database(numbers);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(5));
            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        [Test]
        public void IfMethodRemoveDeletesAnElementFromEmptyData()
        {
            int[] numbers = new int[0];

            Database database = new Database(numbers);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Remove());
            Assert.That(exception.Message, Is.EqualTo("The collection is empty!"), "Collection is not empty");
        }

        [Test]
        public void RemoveLastElementFromDatabase()
        {
            int[] numbers = new int[2] { 5, 15};
            Database database = new Database(numbers);

            database.Remove();
            int[] result = database.Fetch();
            
            Assert.AreEqual(1, database.Count,"The last element is not removed");
            Assert.AreEqual(1, result.Length,"The last element is not removed");
            Assert.AreEqual(5, result[0],"The last element is not removed");

            
            

        }

        [Test]
        public void IfConstructorStoresIntegersOnly()
        {
            int[] numbers = new int[16];

            Database database = new Database(numbers);

            Assert.AreEqual(numbers.GetType(), typeof(int[]),"Constructor doesn't take only integers in the array");
        }

        [Test]
        public void CreateDatabaseWith10Elements()
        {
            Database database = new Database(1,2,3,4,5,6,7,8,9,10);

            Assert.AreEqual(10, database.Count,"Constructor doesn't create Database with given data");
        }

        [Test]
        public void IfFetchMethodReturnsAnArray()
        {
            Database database = new Database(5,12);

            var result = database.Fetch();

            Assert.AreEqual(result.GetType(), typeof(int[]), "The return type is not System.Int32[]");
        }
    }
}
