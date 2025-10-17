using System.Text.Json;
using UserService.Contract.Responses;

namespace OrderService.Application.ApiClients;

public class UserApiClient(HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public bool ValidateUser(Guid customerId)
    {
        var user = GetUserById(customerId);
        var isValid = user != null && !string.IsNullOrEmpty(user.Email);
        return isValid;
    }

    private UserResponse? GetUserById(Guid customerId)
    {
        var response = httpClient.GetAsync(customerId.ToString()).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<UserResponse>(json, options);
    }
}