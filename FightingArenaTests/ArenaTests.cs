namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void TestWArenaConstructor()
        {
            Arena arena = new Arena();

            Assert.AreEqual(0, arena.Warriors.Count);
            Assert.AreEqual(arena.Warriors.GetType(), typeof(List<Warrior>));
        }

        [Test]
        public void TestPropertyCount()
        {
            Arena arena = new Arena();

            Assert.AreEqual(arena.Count, arena.Warriors.Count);
        }

        [Test]
        public void EnrollShouldAddWarrior()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("George", 10, 40);

            arena.Enroll(warrior);

            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void TestingEnrollForDuplicatedNames()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("George", 10, 40);

            arena.Enroll(warrior); 
            
            Assert.AreEqual(1, arena.Count);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior));
            Assert.That(exception.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        [Test]
        public void TestIfMissingNameIsSetCorrectly()
        {
            Warrior attacker = new Warrior("Peter", 100, 35);
            Warrior defender = new Warrior("George", 10, 40);
            Arena arena = new Arena();
            arena.Enroll(attacker);
            arena.Enroll(defender);

            InvalidOperationException exceptionAttacker = Assert.Throws<InvalidOperationException>(() => arena.Fight("Ivan", "George"));
            Assert.That(exceptionAttacker.Message, Is.EqualTo($"There is no fighter with name Ivan enrolled for the fights!"));

            InvalidOperationException exceptionDefender = Assert.Throws<InvalidOperationException>(() => arena.Fight("Peter", "Ivan"));
            Assert.That(exceptionDefender.Message, Is.EqualTo($"There is no fighter with name Ivan enrolled for the fights!"));

            InvalidOperationException exceptionAttackDeff = Assert.Throws<InvalidOperationException>(() => arena.Fight("Ivo", "Ivan"));
            Assert.That(exceptionAttackDeff.Message, Is.EqualTo($"There is no fighter with name Ivan enrolled for the fights!"));
        }

        [Test]
        public void TestAttackInEnroll()
        {
            Arena arena = new Arena();
            Warrior attacker = new Warrior("Peter", 15, 35);
            Warrior defender = new Warrior("George", 15, 45);
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(20, attacker.HP);
            Assert.AreEqual(30, defender.HP);
        }
    }
}
