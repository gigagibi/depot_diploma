namespace Depot.API.Users.Requests;

public class UserPatchRequest
{
    public string UserName { get; set; }
    public string Role { get; set; }
}