namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void TestWarriorConstructor()
        {
            Warrior warrior = new Warrior("Peter", 100, 100);
            
            Assert.AreEqual(warrior.Name, "Peter");
            Assert.AreEqual(warrior.Damage, 100);
            Assert.AreEqual(warrior.HP, 100);
        }

        [Test]
        [TestCase (null)]
        [TestCase ("")]
        [TestCase (" ")]
        public void IfNameIsNullOrEmpty(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior(name, 100, 100));
            Assert.That(exception.Message, Is.EqualTo("Name should not be empty or whitespace!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IfDamageIsZeroOrNegative(int damage)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior("Peter", damage, 100));
            Assert.That(exception.Message, Is.EqualTo("Damage value should be positive!"));
        }

        [Test]
        public void IfHPAreNegative()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Warrior("Peter", 100, -1));
            Assert.That(exception.Message, Is.EqualTo("HP should not be negative!"));
        }

        [Test]
        [TestCase (30)]
        [TestCase (20)]
        public void IfAttackerHPAreEqualOrUnderMInAttackinPoints(int hp)
        {
            Warrior attacker = new Warrior("Peter", 100, hp);
            Warrior defender = new Warrior("George", 10, 40);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(exception.Message, Is.EqualTo("Your HP is too low in order to attack other warriors!"));
        }

        [Test]
        [TestCase(30)]
        [TestCase(20)]
        public void IfDefenderHPAreEqualOrUnderMInAttackinPoints(int hp)
        {
            Warrior attacker = new Warrior("Peter", 100, 40);
            Warrior defender = new Warrior("George", 10, hp);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(exception.Message, Is.EqualTo($"Enemy HP must be greater than 30 in order to attack him!"));
        }

        [Test]
        public void IfAttackerHPAreUnderDefenderDamage()
        {
            Warrior attacker = new Warrior("Peter", 100, 40);
            Warrior defender = new Warrior("George", 100, 35);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(exception.Message, Is.EqualTo($"You are trying to attack too strong enemy"));
        }

        [Test]
        public void IfAttackerHPDecrementsCorrectly()
        {
            Warrior attacker = new Warrior("Peter", 100, 40);
            Warrior defender = new Warrior("George", 10, 35);

            attacker.Attack(defender);

            Assert.AreEqual(30, attacker.HP);
        }

        [Test]
        public void IfAttackerDamageIsGreaterThanDefenderHP()
        {
            Warrior attacker = new Warrior("Peter", 100, 40);
            Warrior defender = new Warrior("George", 10, 35);

            attacker.Attack(defender);

            Assert.AreEqual(0, defender.HP);
        }
        [Test]
        [TestCase(34)]
        [TestCase(35)]
        public void IfDefenderHPDecrementsCorrectly(int damage)
        {
            Warrior attacker = new Warrior("Peter", damage, 40);
            Warrior defender = new Warrior("George", 10, 35);
            int result = defender.HP - attacker.Damage;

            attacker.Attack(defender);

            Assert.AreEqual(result, defender.HP);
        }

    }
}