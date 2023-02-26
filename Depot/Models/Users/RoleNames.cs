using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models.Users;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Operator = "Operator";

    public static IEnumerable<string> AllRoles
    {
        get
        {
            yield return Admin;
            yield return User;
            yield return Operator;
        }
    }
}