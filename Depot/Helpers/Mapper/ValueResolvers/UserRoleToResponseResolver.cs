using AutoMapper;
using Depot.API.Users.Responses;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Depot.Helpers.Mapper.ValueResolvers;

public class UserRoleToResponseResolver : IValueResolver<User, UserGetResponse, string>
{
    private readonly UserManager<User> _userManager;

    public UserRoleToResponseResolver(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public string Resolve(User user, UserGetResponse userGetResponse, string destMember, ResolutionContext context)
    {
        return _userManager.GetRolesAsync(user).Result.FirstOrDefault();
    }
}