using ShirtType.Interface;
using ShirtType.Models;
using System.Collections.Generic;

namespace ShirtType.Service
{
    public class ShirtTypeService : IShirtTypeService
    {
            private List<ShirtTypeModel> _shirtTypes;

            public ShirtTypeService()
            {
                _shirtTypes = new List<ShirtTypeModel>()
            {
                new ShirtTypeModel(){ ShirtType="Collar" ,Price=500 },
                new ShirtTypeModel(){ShirtType ="Slim Fit" ,Price=200}
            };
        }

            public IEnumerable<ShirtTypeModel> GetAllShirtTypes()
            {
                return _shirtTypes;
            }

            public ShirtTypeModel GetShirtType(string shirtType)
            {
                return _shirtTypes.Find(s => s.ShirtType == shirtType);
            }

            public void AddShirtType(ShirtTypeModel shirtType)
            {
                _shirtTypes.Add(shirtType);
            }

            public void UpdateShirtType(ShirtTypeModel shirtType)
            {
                var existingShirtType = _shirtTypes.Find(s => s.ShirtType == shirtType.ShirtType);
                if (existingShirtType != null)
                {
                    existingShirtType.Price = shirtType.Price;
                }
            }

            public void DeleteShirtType(string shirtType)
            {
                _shirtTypes.RemoveAll(s => s.ShirtType == shirtType);
            }
        }
    }
