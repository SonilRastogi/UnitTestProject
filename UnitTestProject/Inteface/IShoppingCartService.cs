using System.Collections.Generic;
using System;
using ShoppingCartProject.Models;

namespace ShoppingCartProject.Inteface
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem GetById(int id);
        bool Remove(int id);
    }
}
