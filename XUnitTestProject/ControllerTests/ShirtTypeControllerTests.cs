using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShirtType.Interface;
using ShirtType.Models;
using ShoppingCartProject.Controllers;
using Xunit;

namespace ShoppingCartXUnitTestProject.ControllerTests
{
    public class ShirtTypeControllerTests
    {
        private ShirtTypeController _controller;
        private Mock<IShirtTypeService> _shirtTypeServiceMock;

        public ShirtTypeControllerTests()
        {
            _shirtTypeServiceMock = new Mock<IShirtTypeService>();
            _controller = new ShirtTypeController(_shirtTypeServiceMock.Object);
        }

        [Fact]
        public void GetAllShirtTypesShouldReturnOkResultWithShirtTypes()
        {
            // Arrange
            var expectedShirtTypes = new List<ShirtTypeModel>
            {
                new ShirtTypeModel { ShirtType = "Polo", Price = 29.99 },
                new ShirtTypeModel { ShirtType = "T-Shirt", Price = 19.99 }
            };

            _shirtTypeServiceMock.Setup(service => service.GetAllShirtTypes())
                                 .Returns(expectedShirtTypes);

            // Act
            var result = _controller.GetAllShirtTypes() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var shirtTypes = result.Value as IEnumerable<ShirtTypeModel>;
            Assert.NotNull(shirtTypes);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public void GetShirtTypeExistingShirtTypeShouldReturnOkResult()
        {
            // Arrange
            var shirtType = "Polo";
            var expectedShirtType = new ShirtTypeModel { ShirtType = "Polo", Price = 29.99 };

            _shirtTypeServiceMock.Setup(service => service.GetShirtType(shirtType))
                                 .Returns(expectedShirtType);

            // Act
            var result = _controller.GetShirtType(shirtType) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var actualShirtType = result.Value as ShirtTypeModel;
            Assert.NotNull(actualShirtType);
            Assert.Equal(expectedShirtType.ShirtType, actualShirtType.ShirtType);
            Assert.Equal(expectedShirtType.Price, actualShirtType.Price);
        }

        [Fact]
        public void GetShirtTypeNonExistentShirtTypeShouldReturnNotFound()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";
            _shirtTypeServiceMock.Setup(service => service.GetShirtType(nonExistentShirtType))
                                 .Returns((ShirtTypeModel)null);

            // Act
            var result = _controller.GetShirtType(nonExistentShirtType) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void AddShirtTypeValidShirtTypeShouldReturnCreatedAtAction()
        {
            // Arrange
            var newShirtType = new ShirtTypeModel { ShirtType = "NewType", Price = 39.99 };

            // Act
            var result = _controller.AddShirtType(newShirtType) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(_controller.GetShirtType), result.ActionName);

            var createdShirtType = result.Value as ShirtTypeModel;
            Assert.NotNull(createdShirtType);
            Assert.Equal(newShirtType.ShirtType, createdShirtType.ShirtType);
            Assert.Equal(newShirtType.Price, createdShirtType.Price);
        }

        [Fact]
        public void UpdateShirtTypeValidShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var shirtType = "Polo";
            var updatedShirtType = new ShirtTypeModel { ShirtType = "Polo", Price = 34.99 };

            // Act
            var result = _controller.UpdateShirtType(shirtType, updatedShirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void UpdateShirtTypeMismatchedShirtTypeShouldReturnBadRequest()
        {
            // Arrange
            var shirtType = "Polo";
            var mismatchedShirtType = new ShirtTypeModel { ShirtType = "T-Shirt", Price = 29.99 };

            // Act
            var result = _controller.UpdateShirtType(shirtType, mismatchedShirtType) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DeleteShirtTypeExistingShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var shirtType = "Polo";

            // Act
            var result = _controller.DeleteShirtType(shirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void DeleteShirtTypeNonExistentShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";

            // Act
            var result = _controller.DeleteShirtType(nonExistentShirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }
    }
}
