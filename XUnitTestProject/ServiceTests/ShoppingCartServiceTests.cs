using System.Linq;
using ShoppingCartProject.Models;
using ShoppingCartProject.Services;
using Xunit;

namespace ShoppingCartXUnitTestProject.ServiceTests
{
    public class ShoppingCartServiceTests
    {
        private ShoppingCartService _shoppingCartService;

        public ShoppingCartServiceTests()
        {
            _shoppingCartService = new ShoppingCartService();
        }

        [Fact]
        public void GetAllItemsReturnsAllItems()
        {
            var result = _shoppingCartService.GetAllItems();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByIdValidIdReturnsCorrectItem()
        {
            var result = _shoppingCartService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Shirt", result.Name);
        }

        [Fact]
        public void GetByIdInvalidIdReturnsNull()
        {
            var result = _shoppingCartService.GetById(999);

            Assert.Null(result);
        }

        [Fact]
        public void AddValidItemReturnsAddedItem()
        {
            var newItem = new ShoppingItem { Name = "Jeans", Price = 800, Manufacturer = "Levis" };

            var result = _shoppingCartService.Add(newItem);

            Assert.NotNull(result);
            Assert.Equal(3, _shoppingCartService.GetAllItems().Count());
        }

        [Fact]
        public void RemoveExistingIdReturnsTrueAndRemovesItem()
        {
            var result = _shoppingCartService.Remove(1);

            Assert.True(result);
            Assert.Single(_shoppingCartService.GetAllItems());
        }

        [Fact]
        public void RemoveNonExistingIdReturnsFalse()
        {
            var result = _shoppingCartService.Remove(999);

            Assert.False(result);
            Assert.Equal(2, _shoppingCartService.GetAllItems().Count());
        }
    }
}
