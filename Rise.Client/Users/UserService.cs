using Rise.Shared.Users;
using System.Net.Http.Json;

namespace Rise.Client.Users;

public class UserService : IUserService
{
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var gebruikers = await httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("user");
        return gebruikers!;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        
        var gebruiker = await httpClient.GetFromJsonAsync<UserDto>($"user/{id}");
        return gebruiker; 
    }

    
    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var gebruiker = await httpClient.GetFromJsonAsync<UserDto>($"user/email/{email}");
        return gebruiker;
    }
}
