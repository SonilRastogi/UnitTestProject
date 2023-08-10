using ShoppingCartProject.Models;
using System.Threading.Tasks;

namespace ShoppingCartProject.Inteface
{
    public interface IExternalApiService
    {
        Task<ProductModel> GetProductAsync(int productId);
    }
}
