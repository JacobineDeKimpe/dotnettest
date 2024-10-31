using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Users;

namespace Rise.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        var users = await userService.GetUsersAsync();
        return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
            return NotFound();
        return user;
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmail(email);
        if (user == null)
            return NotFound();
        return user;
    }


}