using NUnit.Framework;
using System.Linq;
using ShoppingCartProject.Models;
using ShoppingCartProject.Services;

namespace ShoppingCartNUnitTestProject.ServiceTests
{
    [TestFixture]
    public class ShoppingCartServiceTests
    {
        private ShoppingCartService _shoppingCartService;

        [SetUp]
        public void Setup()
        {
            _shoppingCartService = new ShoppingCartService();
        }

        [Test]
        public void GetAllItemsReturnsAllItems()
        {
            var result = _shoppingCartService.GetAllItems();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetByIdValidIdReturnsCorrectItem()
        {
            var result = _shoppingCartService.GetById(1);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("Shirt"));
        }

        [Test]
        public void GetByIdInvalidIdReturnsNull()
        {
            var result = _shoppingCartService.GetById(999);

            Assert.IsNull(result);
        }

        [Test]
        public void AddValidItemReturnsAddedItem()
        {
            var newItem = new ShoppingItem { Name = "Jeans", Price = 800, Manufacturer = "Levis" };

            var result = _shoppingCartService.Add(newItem);

            Assert.IsNotNull(result); ;
            Assert.That(_shoppingCartService.GetAllItems().Count(), Is.EqualTo(3));
        }

        [Test]
        public void RemoveExistingIdReturnsTrueAndRemovesItem()
        {
            var result = _shoppingCartService.Remove(1);

            Assert.IsTrue(result);
            Assert.That(_shoppingCartService.GetAllItems().Count(), Is.EqualTo(1));
        }

        [Test]
        public void RemoveNonExistingIdReturnsFalse()
        {
            var result = _shoppingCartService.Remove(999);

            Assert.IsFalse(result);
            Assert.That(_shoppingCartService.GetAllItems().Count(), Is.EqualTo(2));
        }
    }
}
