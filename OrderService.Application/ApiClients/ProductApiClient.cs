using System.Text.Json;
using ProductService.Contract.Responses;

namespace OrderService.Application.ApiClients;

public class ProductApiClient(HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public int GetProductStock(Guid productId)
    {
        return GetProductById(productId).Stock;
    }

    private ProductResponse GetProductById(Guid productId)
    {
        var response = httpClient.GetAsync(productId.ToString()).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return
            JsonSerializer.Deserialize<ProductResponse>(json, options)!;
    }
}