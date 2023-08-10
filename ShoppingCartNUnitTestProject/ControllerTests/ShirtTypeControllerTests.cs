using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShirtType.Interface;
using ShirtType.Models;
using ShoppingCartProject.Controllers;

namespace ShoppingCartNUnitTestProject.ControllerTests
{
    [TestFixture]
    public class ShirtTypeControllerTests
    {
        private ShirtTypeController _controller;
        private Mock<IShirtTypeService> _shirtTypeServiceMock;

        [SetUp]
        public void Setup()
        {
            _shirtTypeServiceMock = new Mock<IShirtTypeService>();
            _controller = new ShirtTypeController(_shirtTypeServiceMock.Object);
        }

        [Test]
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
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
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
            Assert.AreEqual(200, result.StatusCode);

            var actualShirtType = result.Value as ShirtTypeModel;
            Assert.NotNull(actualShirtType);
            Assert.AreEqual(expectedShirtType.ShirtType, actualShirtType.ShirtType);
            Assert.AreEqual(expectedShirtType.Price, actualShirtType.Price);
        }

        [Test]
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
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void AddShirtTypeValidShirtTypeShouldReturnCreatedAtAction()
        {
            // Arrange
            var newShirtType = new ShirtTypeModel { ShirtType = "NewType", Price = 39.99 };

            // Act
            var result = _controller.AddShirtType(newShirtType) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(nameof(_controller.GetShirtType), result.ActionName);

            var createdShirtType = result.Value as ShirtTypeModel;
            Assert.NotNull(createdShirtType);
            Assert.AreEqual(newShirtType.ShirtType, createdShirtType.ShirtType);
            Assert.AreEqual(newShirtType.Price, createdShirtType.Price);
        }

        [Test]
        public void UpdateShirtTypeValidShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var shirtType = "Polo";
            var updatedShirtType = new ShirtTypeModel { ShirtType = "Polo", Price = 34.99 };

            // Act
            var result = _controller.UpdateShirtType(shirtType, updatedShirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void UpdateShirtTypeMismatchedShirtTypeShouldReturnBadRequest()
        {
            // Arrange
            var shirtType = "Polo";
            var mismatchedShirtType = new ShirtTypeModel { ShirtType = "T-Shirt", Price = 29.99 };

            // Act
            var result = _controller.UpdateShirtType(shirtType, mismatchedShirtType) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void DeleteShirtTypeExistingShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var shirtType = "Polo";

            // Act
            var result = _controller.DeleteShirtType(shirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void DeleteShirtTypeNonExistentShirtTypeShouldReturnNoContent()
        {
            // Arrange
            var nonExistentShirtType = "NonExistent";

            // Act
            var result = _controller.DeleteShirtType(nonExistentShirtType) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
