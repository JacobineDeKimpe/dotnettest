using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rise.Shared.Users;

namespace Rise.Client.Users;

    public class FakeUserService: IUserService
    {
    private readonly List<UserDto> _users;
    public FakeUserService()
    {

        _users = Enumerable.Range(1, 10)
                              .Select(i => new UserDto
                              {
                                  Id = i,
                                  Voornaam = $"Voornaam {i}",
                                  Naam = $"Naam {i}",
                                  Email = $"user{i}@example.com",
                                  TelNr = $"12345",
                                  Rol = $"ADMIN"
                              })
                              .ToList();
    }
    public Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task<UserDto> GetUserById(int id)  
    {
        var user = _users.FirstOrDefault(u => u.Id == id);  
        return Task.FromResult(user);
    }

    public Task<UserDto> GetUserByEmail(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }
}

