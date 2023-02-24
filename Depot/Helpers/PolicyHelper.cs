using Depot.Models.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Depot.Helpers;

public static class PolicyHelper
{
    /// <summary>
    /// Admin and SuperAdmin
    /// </summary>
    public const string ADMIN_POLICY = nameof(ADMIN_POLICY);
    /// <summary>
    /// User, Admin, SuperAdmin
    /// </summary>
    public const string USER_POLICY = nameof(USER_POLICY);
    public static void AddDepotPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(ADMIN_POLICY, policy => policy
            .RequireAuthenticatedUser()
            .RequireRole(RoleNames.Admin)
        );
        options.AddPolicy(USER_POLICY, policy => policy
            .RequireAuthenticatedUser()
            .RequireRole(RoleNames.User, RoleNames.Admin)
        );
    }
}