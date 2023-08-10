using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCartProject.Controllers;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;
using Xunit;

namespace ShoppingCartXUnitTestProject.ControllerTests
{
    public class ProductControllerTests
    {
        private ShoppingCartExternalAPI _controller;
        private Mock<IExternalApiService> _externalApiServiceMock;

        public ProductControllerTests()
        {
            _externalApiServiceMock = new Mock<IExternalApiService>();
            _controller = new ShoppingCartExternalAPI(_externalApiServiceMock.Object);
        }

        [Fact]
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
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            var product = result.Value as ProductModel;
            Assert.NotNull(product);
            Assert.Equal(expectedProduct.Id, product.Id);
            Assert.Equal(expectedProduct.Title, product.Title);
            Assert.Equal(expectedProduct.Price, product.Price);
        }

        [Fact]
        public async Task GetProductApiReturnsNotFound()
        {
            // Arrange
            _externalApiServiceMock.Setup(service => service.GetProductAsync(It.IsAny<int>()))
                                   .ReturnsAsync((ProductModel)null);

            // Act
            var result = await _controller.GetProduct(1) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetProductServiceThrowsExceptionReturnsBadRequest()
        {
            // Arrange
            _externalApiServiceMock.Setup(service => service.GetProductAsync(It.IsAny<int>()))
                                   .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetProduct(1) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
