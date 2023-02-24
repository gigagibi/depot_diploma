using System.IdentityModel.Tokens.Jwt;

namespace Depot.API.Authentication.Responses;

public record LoginResponse(
    string token,
    string roleName,
    DateTime expiration
);