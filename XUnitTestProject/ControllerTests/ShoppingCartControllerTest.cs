using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AutoFixture;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Controllers;
using ShoppingCartProject.Models;

namespace ShoppingCartXUnitTestProject.ControllerTests
{
    public class ShoppingCartControllerTestsXUnit
    {
        private Mock<IShoppingCartService> _mockService;
        private ShoppingCartController _controller;
        private Fixture fixture;

        public ShoppingCartControllerTestsXUnit()
        {
            fixture = new Fixture();
            _mockService = new Mock<IShoppingCartService>();
            _controller = new ShoppingCartController(_mockService.Object);
        }

        [Fact]
        public void GetReturnsOkResultWithItems()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllItems())
                        .Returns(fixture.CreateMany<ShoppingItem>(3));

            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetInvalidIdReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(fixture.Create<int>()))
                        .Returns((ShoppingItem)null);

            // Act
            var result = _controller.Get(It.IsAny<int>()) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void AddValidItemReturnsCreatedWithLocation()
        {
            var item = fixture.Create<ShoppingItem>();
            // Arrange
            _mockService.Setup(service => service.Add(It.IsAny<ShoppingItem>()))
                        .Returns(item);

            // Act
            var result = _controller.Post(item) as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.IsType<ShoppingItem>(result.Value);
                Assert.IsType<CreatedAtActionResult>(result);
                Assert.Equal(201, result.StatusCode);

            });
            _mockService.Verify(x => x.Add(It.IsAny<ShoppingItem>()), Times.Once);
        }

        [Fact]
        public void RemoveExistingItemReturnsNoContent()
        {
            // Arrange
            int itemId = 1;
            _mockService.Setup(service => service.GetById(itemId))
                        .Returns(fixture.Create<ShoppingItem>());
            _mockService.Setup(service => service.Remove(itemId))
                      .Returns(true);

            // Act
            var result = _controller.Remove(itemId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.IsType<NoContentResult>(result);
                Assert.Equal(204, ((NoContentResult)result).StatusCode);
            });
            _mockService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _mockService.Verify(x => x.Remove(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void RemoveNonExistingItemReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(It.IsAny<int>()))
                        .Returns((ShoppingItem)null);
            _mockService.Setup(service => service.Remove(It.IsAny<int>()))
                      .Returns(false);

            // Act
            var result = _controller.Remove(fixture.Create<int>()) as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal(404, result.StatusCode);
            });
            _mockService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _mockService.Verify(x => x.Remove(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void RemoveExceptionThrownReturnsInternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(It.IsAny<int>()))
                         .Returns((ShoppingItem)null);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _controller.Remove(0));
            Assert.Equal("Id can't be null", ex.Message);
        }
    }
}
