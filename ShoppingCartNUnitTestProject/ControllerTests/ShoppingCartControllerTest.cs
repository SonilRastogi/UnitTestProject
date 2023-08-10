using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCartProject.Controllers;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;
using AutoFixture;

namespace ShoppingCartNUnitTestProject.ControllerTests
{
    [TestFixture]
    public class ShoppingCartControllerTests
    {
        private Mock<IShoppingCartService> _mockService;
        private ShoppingCartController _controller;
        public Fixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            _mockService = new Mock<IShoppingCartService>();
            _controller = new ShoppingCartController(_mockService.Object);
        }

        [Test]
        public void GetReturnsOkResultWithItems()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllItems())
                        .Returns(fixture.CreateMany<ShoppingItem>(3));

            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<OkObjectResult>(result);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            });
            _mockService.Verify(x => x.GetAllItems(), Times.Once);
        }

        [Test]
        public void GetInvalidIdReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(fixture.Create<int>()))
                        .Returns((ShoppingItem)null);

            // Act
            var result = _controller.Get(It.IsAny<int>()) as NotFoundResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<NotFoundResult>(result);
                Assert.That(result.StatusCode, Is.EqualTo(404));
            });
            _mockService.Verify(x => x.GetById(fixture.Create<int>()), Times.Never);
        }

        [Test]
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
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<ShoppingItem>(result.Value);
                Assert.IsInstanceOf<CreatedAtActionResult>(result);
                Assert.That(result.StatusCode, Is.EqualTo(201));

            });
            _mockService.Verify(x => x.Add(It.IsAny<ShoppingItem>()), Times.Once);
        }

        [Test]
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
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<NoContentResult>(result);
                Assert.That(((NoContentResult)result).StatusCode, Is.EqualTo(204));
            });
            _mockService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _mockService.Verify(x => x.Remove(It.IsAny<int>()), Times.Once);
        }

        [Test]
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
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<NotFoundObjectResult>(result);
                Assert.That(result.StatusCode, Is.EqualTo(404));
            });
            _mockService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _mockService.Verify(x => x.Remove(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void RemoveExceptionThrownReturnsInternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(It.IsAny<int>()))
                         .Returns((ShoppingItem)null);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => _controller.Remove(0));


            // Assert

            Assert.That(ex.Message, Is.EqualTo("Id can't be null"));
        }

    }
}
