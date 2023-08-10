using ShirtType.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShirtType.Interface
{
    public interface IShirtTypeService
    {
        IEnumerable<ShirtTypeModel> GetAllShirtTypes();
        ShirtTypeModel GetShirtType(string shirtType);
        void AddShirtType(ShirtTypeModel shirtType);
        void UpdateShirtType(ShirtTypeModel shirtType);
        void DeleteShirtType(string shirtType);
    }
}
