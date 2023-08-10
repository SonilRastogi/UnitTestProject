using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;

namespace ShoppingCartProject.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private List<ShoppingItem> _shoppingList;
        public ShoppingCartService()
        {
            _shoppingList = new List<ShoppingItem>()
            {
                new ShoppingItem(){ Id =1 ,Name ="Shirt" ,Price=500 ,Manufacturer="Max"},
                new ShoppingItem(){ Id =2 ,Name ="Tshirt" ,Price=200 ,Manufacturer="Max"}
            };
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _shoppingList;
        }

        public ShoppingItem GetById(int id)
        {
            return _shoppingList.FirstOrDefault(x => x.Id == id);
        }

        public ShoppingItem Add(ShoppingItem newItem)
        {
            newItem.Id = _shoppingList.Max(e => e.Id) + 1;
            _shoppingList.Add(newItem);
            return newItem;
        }

        public bool Remove(int id)
        {
            ShoppingItem item = _shoppingList.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                _shoppingList.Remove(item);
                return true;
            }
            return false;
        }
    }
}
