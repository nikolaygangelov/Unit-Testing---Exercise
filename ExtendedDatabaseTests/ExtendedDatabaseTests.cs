namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [Test]
        public void CreateDatabaseWith2People()
        {
            Person Peter = new Person(1, "Peter");
            Person George = new Person(2, "George");
            Person[] people = new Person[2];
            people[0] = Peter;
            people[1] = George;

            Database database = new Database(people);
            Person first = database.FindById(1);
            Person second = database.FindById(2);

            Assert.AreEqual(people.Length, database.Count, "Constructor doesn't create Database with given data");
            Assert.AreEqual("Peter", first.UserName);
            Assert.AreEqual("George", second.UserName);
        }

        [Test]
        public void TestAddRangeArrayLenght()
        {
            Person[] people = new Person[17];

            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Database(people));
            Assert.That(exception.Message, Is.EqualTo("Provided data length should be in range [0..16]!"));
        }

        [Test]
        public void IfMethodAddPutAPersonInTheNextFreeCell()
        {
            Person[] people = new Person[16];
            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, i.ToString());
            }

            Database database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(new Person(3, "Ivan")));
            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }



        [Test]
        public void IfMethodRemoveDeletesAPersonFromEmptyData()
        {
            Person[] people = new Person[0];

            Database database = new Database(people);

            Assert.Throws<InvalidOperationException>(() => database.Remove());
            
        }

        [Test]
        public void RemoveLastElementFromDatabase()
        {
            Person Peter = new Person(1, "Peter");
            Person George = new Person(2, "George");
            Person[] people = new Person[2];
            people[0] = Peter;
            people[1] = George;//=null
            Database database = new Database(people);
            Person first = database.FindById(1);

            database.Remove();

            Assert.AreEqual(1, database.Count, "The last element is not removed");
            Assert.AreEqual("Peter", first.UserName);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.FindByUsername("George"));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"), "User is not removed");
        }

        [Test]
        public void IfConstructorStoresPersonOnly()
        {
            Person[] people = new Person[0];

            Database database = new Database(people);

            Assert.AreEqual(people.GetType(), typeof(Person[]), "Constructor doesn't store only Person in the array");
        }

        [Test]
        public void IfThereIsAUserWithSameName()
        {
            Person Peter = new Person(1, "Peter");
            Person Peter1 = new Person(2, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;
            
            Database database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(Peter1));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this username!"));
        }

        [Test]
        public void IfThereIsAUserWithSameId()
        {
            Person Peter = new Person(1, "Peter");
            Person George = new Person(1, "George");
            Person[] people = new Person[1];
            people[0] = Peter;

            Database database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(George));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this Id!"));
        }

        [Test]
        public void IfNoMatchingUsernames()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;

            Database database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.FindByUsername("George"));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"), "There are duplicated usernames");
        }

        [Test]
        public void FindByUsernameReturnsCorrectUser()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;
            Database database = new Database(people);
            Person person = database.FindByUsername("Peter");

            Assert.AreEqual("Peter", person.UserName);
            Assert.AreEqual(1, person.Id);
        }

        [Test]
        public void IfUsernameNullOrEmpty()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;

            Database database = new Database(people);

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
            Assert.That(exception.ParamName, Is.EqualTo("Username parameter is null!"));

            ArgumentNullException emtyEx = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(string.Empty));
            Assert.That(emtyEx.ParamName, Is.EqualTo("Username parameter is null!"));
        }


        [Test]
        public void IfNoMatchingIDs()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;

            Database database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.FindById(2));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this ID!"), "There are duplicated IDs");
        }

        [Test]
        public void FindByIdReturnsCorrectUser()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;
            Database database = new Database(people);
            Person person = database.FindById(1);

            Assert.AreEqual("Peter", person.UserName);
            Assert.AreEqual(1, person.Id);
        }
        [Test]
        public void IfIdIsNegative()
        {
            Person Peter = new Person(1, "Peter");
            Person[] people = new Person[1];
            people[0] = Peter;

            Database database = new Database(people);

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-2));
            Assert.That(exception.ParamName, Is.EqualTo("Id should be a positive number!"),"There are negative IDs");
        }

        

        [Test]
        public void CreatePersonWith2Parameters()
        {
            Person Peter = new Person(1, "Peter");

            Assert.AreEqual(1, Peter.Id, "Constructor doesn't create Person with given Id");
            Assert.AreEqual("Peter", Peter.UserName, "Constructor doesn't create Person with given username");
        }
    }
}