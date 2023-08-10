using ShirtType.Interface;
using ShirtType.Models;
using ShirtType.Service;
using System;
using System.Linq;
using Xunit;

namespace ShoppingCartXUnitTestProject.ServiceTests
{
    public class ShirtTypeServiceTests
    {
        private IShirtTypeService _shirtTypeService;

        public ShirtTypeServiceTests()
        {
            _shirtTypeService = new ShirtTypeService();
        }

        [Fact]
        public void GetAllShirtTypesShouldReturnAllShirtTypes()
        {
            // Act
            var shirtTypes = _shirtTypeService.GetAllShirtTypes();

            // Assert
            Assert.NotNull(shirtTypes);
            Assert.Equal(2, shirtTypes.Count());
        }

        [Fact]
        public void GetShirtTypeExistingShirtTypeShouldReturnShirtType()
        {
            // Arrange
            var existingShirtType = "Collar";

            // Act
            var shirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.NotNull(shirtType);
            Assert.Equal(existingShirtType, shirtType.ShirtType);
        }

        [Fact]
        public void GetShirtTypeNonExistentShirtTypeShouldReturnNull()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";

            // Act
            var shirtType = _shirtTypeService.GetShirtType(nonExistentShirtType);

            // Assert
            Assert.Null(shirtType);
        }

        [Fact]
        public void AddShirtType_ShouldAddNewShirtType()
        {
            // Arrange
            var newShirtType = new ShirtTypeModel { ShirtType = "Casual", Price = 45.99 };

            // Act
            _shirtTypeService.AddShirtType(newShirtType);
            var addedShirtType = _shirtTypeService.GetShirtType(newShirtType.ShirtType);

            // Assert
            Assert.NotNull(addedShirtType);
            Assert.Equal(newShirtType.ShirtType, addedShirtType.ShirtType);
            Assert.Equal(newShirtType.Price, addedShirtType.Price);
        }

        [Fact]
        public void UpdateShirtTypeExistingShirtTypeShouldUpdatePrice()
        {
            // Arrange
            var existingShirtType = "Slim Fit";
            var updatedShirtType = new ShirtTypeModel { ShirtType = "Slim Fit", Price = 250.00 };

            // Act
            _shirtTypeService.UpdateShirtType(updatedShirtType);
            var modifiedShirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.NotNull(modifiedShirtType);
            Assert.Equal(existingShirtType, modifiedShirtType.ShirtType);
            Assert.Equal(updatedShirtType.Price, modifiedShirtType.Price);
        }

        [Fact]
        public void UpdateShirtTypeNonExistentShirtTypeShouldNotUpdate()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";
            var updatedShirtType = new ShirtTypeModel { ShirtType = nonExistentShirtType, Price = 100.00 };

            // Act
            _shirtTypeService.UpdateShirtType(updatedShirtType);
            var modifiedShirtType = _shirtTypeService.GetShirtType(nonExistentShirtType);

            // Assert
            Assert.Null(modifiedShirtType);
        }

        [Fact]
        public void DeleteShirtTypeExistingShirtTypeShouldRemoveShirtType()
        {
            // Arrange
            var existingShirtType = "Collar";

            // Act
            _shirtTypeService.DeleteShirtType(existingShirtType);
            var deletedShirtType = _shirtTypeService.GetShirtType(existingShirtType);

            // Assert
            Assert.Null(deletedShirtType);
        }
    }
}
