using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCartProject.Controllers;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;

namespace ShoppingCartNUnitTestProject.ControllerTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ShoppingCartExternalAPI _controller;
        private Mock<IExternalApiService> _externalApiServiceMock;

        [SetUp]
        public void Setup()
        {
            _externalApiServiceMock = new Mock<IExternalApiService>();
            _controller = new ShoppingCartExternalAPI(_externalApiServiceMock.Object);
        }

        [Test]
        public async Task GetProductSuccess()
        {
            // Arrange
            var expectedProduct = new ProductModel
            {
                Id = 1,
                Title = "Test Product",
                Price = 99.99m
            };

            _externalApiServiceMock.Setup(service => service.GetProductAsync(It.IsAny<int>()))
                                   .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetProduct(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);

            var product = result.Value as ProductModel;
            Assert.IsNotNull(product);
            Assert.AreEqual(expectedProduct.Id, product.Id);
            Assert.AreEqual(expectedProduct.Title, product.Title);
            Assert.AreEqual(expectedProduct.Price, product.Price);
        }

        [Test]
        public async Task GetProductApiReturnsNotFoundReturnsNotFound()
        {
            // Arrange
            _externalApiServiceMock.Setup(service => service.GetProductAsync(It.IsAny<int>()))
                                   .ReturnsAsync((ProductModel)null);

            // Act
            var result = await _controller.GetProduct(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public async Task GetProductServiceThrowsExceptionReturnsBadRequest()
        {
            // Arrange
            _externalApiServiceMock.Setup(service => service.GetProductAsync(It.IsAny<int>()))
                                   .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetProduct(1) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}


