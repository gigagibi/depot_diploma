namespace Depot.API.Users.Responses;

public class UserUpdateResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? Role { get; set; }
}