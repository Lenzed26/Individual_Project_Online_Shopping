using System;
using System.Collections.Generic;
using System.Linq;
using ShopBusiness;
using ShopData;
using ShopData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;

namespace ShopTests
{
    class HunterManagerShould
    {
        private HunterManager _sut;

        [Test]
        public void ThrowAnException_WhenHunterServiceIsNull()
        {
            Assert.That(() => new HunterManager(null), Throws.ArgumentException);
        }

        [Test]
        public void BeAbleToBeConstructed()
        {
            var mockHunterService = new Mock<IHunterService>();
            _sut = new HunterManager(mockHunterService.Object);
            Assert.That(_sut, Is.InstanceOf<HunterManager>());
        }

        [Test]
        public void ReturnTrue_WhenUpdateIsCalled_WithValidName()
        {
            var mockHunterService = new Mock<IHunterService>();
            var originalHunter = new Hunter()
            {
                Name = "Test Hunter",
                Location = "Debug Field"
            };
            mockHunterService.Setup(hs => hs.GetHunterByName("Test Hunter")).Returns(originalHunter);

            _sut = new HunterManager(mockHunterService.Object);

            var result = _sut.Update("Test Hunter", "Astera");

            Assert.That(result);
            
        }

        [Test]
        public void UpdateWithCorrectValues_WhenUpdateIsCalled_WithValidName()
        {
            var mockHunterService = new Mock<IHunterService>();
            var originalHunter = new Hunter()
            {
                Name = "Test Hunter",
                Location = "Debug Field"
            };
            mockHunterService.Setup(hs => hs.GetHunterByName("Test Hunter")).Returns(originalHunter);

            _sut = new HunterManager(mockHunterService.Object);

            var result = _sut.Update("Test Hunter", "Astera");
            Assert.That(_sut.SelectedHunter.Name, Is.EqualTo("Test Hunter"));
            Assert.That(_sut.SelectedHunter.Location, Is.EqualTo("Astera"));
        }

        [Test]
        public void UpdateTheSelectedCustomer_ReturnsFalse_WhenUpdateIsCalledWithInvalidName()
        {
            var mockHunterService = new Mock<IHunterService>();
            mockHunterService.Setup(hs => hs.GetHunterByName("Test Hunter")).Returns((Hunter)null);
            _sut = new HunterManager(mockHunterService.Object);

            var result = _sut.Update("Test Hunter", It.IsAny<string>());

            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateTheSelectedCustomer_ReturnsFalse_WhenADatabaseExceptionIsThrown()
        {
            var mockHunterService = new Mock<IHunterService>();
            mockHunterService.Setup(hs => hs.GetHunterByName(It.IsAny<string>())).Returns(new Hunter());
            mockHunterService.Setup(hs => hs.SaveHunterChanges()).Throws<DbUpdateConcurrencyException>();
            _sut = new HunterManager(mockHunterService.Object);

            var result = _sut.Update(It.IsAny<string>(), It.IsAny<string>());

            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateTheSelectedCustomer_CallsSaveCustomerChanges_WhenGivenAValidName()
        {
            var mockHunterService = new Mock<IHunterService>();
            mockHunterService.Setup(hs => hs.GetHunterByName(It.IsAny<string>())).Returns(new Hunter());

            _sut = new HunterManager(mockHunterService.Object);

            var result = _sut.Update(It.IsAny<string>(), It.IsAny<string>());

            mockHunterService.Verify(cs => cs.SaveHunterChanges(), Times.AtLeastOnce);
        }
    }
}
