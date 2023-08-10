using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ShoppingCartExternalAPIService : IExternalApiService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartExternalAPIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductModel> GetProductAsync(int productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                ProductModel product = JsonSerializer.Deserialize<ProductModel>(jsonResponse);
                return product;
            }
            else
            {
                throw new Exception($"External API call failed with status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error calling external API: " + ex.Message, ex);
        }
    }
}
