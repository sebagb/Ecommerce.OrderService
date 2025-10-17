using System.Text.Json;
using UserService.Contract.Responses;

namespace OrderService.Application.ApiClients;

public class UserApiClient(HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public bool ValidateUser(Guid customerId)
    {
        var response = httpClient.GetAsync(customerId.ToString()).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        var user = JsonSerializer.Deserialize<UserResponse>(json);
        return user != null && !string.IsNullOrEmpty(user.Email);
    }
}