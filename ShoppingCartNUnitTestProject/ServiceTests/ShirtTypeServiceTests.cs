using NUnit.Framework;
using ShirtType.Interface;
using ShirtType.Models;
using ShirtType.Service;
using System;
using System.Linq;

namespace ShoppingCartNUnitTestProject.ServiceTests
{
    [TestFixture]
    public class ShirtTypeServiceTests
    {
        private IShirtTypeService _shirtTypeService;

        [SetUp]
        public void Setup()
        {
            _shirtTypeService = new ShirtTypeService();
        }

        [Test]
        public void GetAllShirtTypesShouldReturnAllShirtTypes()
        {
            // Act
            var shirtTypes = _shirtTypeService.GetAllShirtTypes();

            // Assert
            Assert.IsNotNull(shirtTypes);
            Assert.AreEqual(2, shirtTypes.Count());
        }

        [Test]
        public void GetShirtTypeExistingShirtTypeShouldReturnShirtType()
        {
            // Arrange
            var existingShirtType = "Collar";

            // Act
            var shirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.IsNotNull(shirtType);
            Assert.AreEqual(existingShirtType, shirtType.ShirtType);
        }

        [Test]
        public void GetShirtTypeNonExistentShirtTypeShouldReturnNull()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";

            // Act
            var shirtType = _shirtTypeService.GetShirtType(nonExistentShirtType);

            // Assert
            Assert.IsNull(shirtType);
        }

        [Test]
        public void AddShirtTypeShouldAddNewShirtType()
        {
            // Arrange
            var newShirtType = new ShirtTypeModel { ShirtType = "Casual", Price = 45.99 };

            // Act
            _shirtTypeService.AddShirtType(newShirtType);
            var addedShirtType = _shirtTypeService.GetShirtType(newShirtType.ShirtType);

            // Assert
            Assert.IsNotNull(addedShirtType);
            Assert.AreEqual(newShirtType.ShirtType, addedShirtType.ShirtType);
            Assert.AreEqual(newShirtType.Price, addedShirtType.Price);
        }

        [Test]
        public void UpdateShirtTypeExistingShirtTypeShouldUpdatePrice()
        {
            // Arrange
            var existingShirtType = "Slim Fit";
            var updatedShirtType = new ShirtTypeModel { ShirtType = "Slim Fit", Price = 250.00 };

            // Act
            _shirtTypeService.UpdateShirtType(updatedShirtType);
            var modifiedShirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.IsNotNull(modifiedShirtType);
            Assert.AreEqual(existingShirtType, modifiedShirtType.ShirtType);
            Assert.AreEqual(updatedShirtType.Price, modifiedShirtType.Price);
        }

        [Test]
        public void UpdateShirtTypeNonExistentShirtTypeShouldNotUpdate()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";
            var updatedShirtType = new ShirtTypeModel { ShirtType = nonExistentShirtType, Price = 100.00 };

            // Act
            _shirtTypeService.UpdateShirtType(updatedShirtType);
            var modifiedShirtType = _shirtTypeService.GetShirtType(nonExistentShirtType);

            // Assert
            Assert.IsNull(modifiedShirtType);
        }

        [Test]
        public void DeleteShirtTypeExistingShirtTypeShouldRemoveShirtType()
        {
            // Arrange
            var existingShirtType = "Collar";

            // Act
            _shirtTypeService.DeleteShirtType(existingShirtType);
            var deletedShirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.IsNull(deletedShirtType);
        }

        [Test]
        public void DeleteShirtTypeNonExistentShirtTypeShouldNotThrowException()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";

            // Act & Assert
            Assert.DoesNotThrow(() => _shirtTypeService.DeleteShirtType(nonExistentShirtType));
        }
    }
}
