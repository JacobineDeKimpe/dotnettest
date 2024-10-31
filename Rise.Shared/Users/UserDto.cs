namespace Rise.Shared.Users;

public class UserDto
{
    public required int Id { get; set; }
    public required string Naam { get; set; }
    public required string Voornaam { get; set; }
    public required string Email { get; set; }

    public required string TelNr { get; set; }
    public required string Rol { get; set; }
}
