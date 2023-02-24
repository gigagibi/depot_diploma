using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Depot.Models.Opts;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Depot.Helpers;

public class JwtHelper
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public JwtHelper(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    private async Task<User> ParseUserByJwtFromUserManager(string jwt)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.ReadJwtToken(jwt);
        var username = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        return await _userManager.FindByNameAsync(username);
    }
    
    public async Task<User> GetUserFromJwt(string jwt)
    {
        return await ParseUserByJwtFromUserManager(jwt);
    }
    
    public async Task<Employee> GetEmployeeFromJwt(string jwt)
    {
        return (await ParseUserByJwtFromUserManager(jwt)).Employee;
    }
    
    public JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var jwtOptions = new JwtOptions();
        _configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}