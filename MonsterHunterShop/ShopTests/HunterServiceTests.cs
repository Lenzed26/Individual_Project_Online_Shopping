using System;
using System.Collections.Generic;
using System.Linq;
using ShopData;
using ShopData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ShopTests
{
    class HunterServiceTests
    {
        private HunterService _sut;
        private MonsterHunterContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<MonsterHunterContext>().UseInMemoryDatabase(databaseName: "Test_DB").Options;
            _context = new MonsterHunterContext(options);
            _sut = new HunterService(_context);

            _sut.CreateHunter(new Hunter { Name = "Test Hunter", Location = "Debug Field" });
        }

        [Test]
        public void GivenAValidName_CorrectCustomerIsReturned()
        {
            var result = _sut.GetHunterByName("Test Hunter");
            Assert.That(result, Is.TypeOf<Hunter>());
            Assert.That(result.Name, Is.EqualTo("Test Hunter"));
            Assert.That(result.Location, Is.EqualTo("Debug Field"));
        }

        [Test]
        public void GivenANewHunter_CreateCustomerAddsItToTheDatabase()
        {
            var numberOfHuntersBefore = _context.Hunters.Count();
            var newHunter = new Hunter { Name = "Other Hunter", Location = "Debug Field" };

            _sut.CreateHunter(newHunter);
            var result = _sut.GetHunterByName("Other Hunter");

            Assert.That(result, Is.TypeOf<Hunter>());
            Assert.That(result.Name, Is.EqualTo("Other Hunter"));
            Assert.That(result.Location, Is.EqualTo("Debug Field"));

            Assert.That(_context.Hunters.Count(), Is.EqualTo(numberOfHuntersBefore + 1));

            _context.Hunters.Remove(newHunter);
            _context.SaveChanges();
        }

        [Test]
        public void DeletingACustomer_ListSizeDecreasesByOne()
        {
            var newHunter = new Hunter { Name = "Other Hunter", Location = "Debug Field" };
            _sut.CreateHunter(newHunter);
            var numberOfCustomersBefore = _context.Hunters.Count();
            _sut.RemoveHunter(newHunter);
            
            Assert.That(_context.Hunters.Count(), Is.EqualTo(numberOfCustomersBefore - 1));
        }
    }
}
