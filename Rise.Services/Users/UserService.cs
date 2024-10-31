using Microsoft.EntityFrameworkCore;
using Rise.Domain.Users;
using Rise.Persistence;
using Rise.Shared.Users;

namespace Rise.Services.Users;

public class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        IQueryable<UserDto> query = dbContext.Users.Select(x => new UserDto
        {
            Id = x.Id,
            Voornaam = x.Voornaam,
            Naam = x.Naam,
            Email = x.Email,
            TelNr = x.TelNr,
            Rol = x.Rol.ToString()
        });

        var users = await query.ToListAsync();
        return users;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await dbContext.Users
            .Where(x => x.Id == id)
            .Select(x => new UserDto
            {
                Id = x.Id,
                Voornaam = x.Voornaam,
                Naam = x.Naam,
                Email = x.Email,
                TelNr = x.TelNr,
                Rol = x.Rol.ToString() 
            })
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var user = await dbContext.Users
            .Where(x => x.Email == email)
            .Select(x => new UserDto
            {
                Id = x.Id,
                Voornaam = x.Voornaam,
                Naam = x.Naam,
                Email = x.Email,
                TelNr = x.TelNr,
                Rol = x.Rol.ToString()
            })
            .FirstOrDefaultAsync();

        return user;
    }
}
