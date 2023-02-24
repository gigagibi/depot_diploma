using Depot.API.Authentication.Requests;
using Depot.API.Authentication.Responses;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;
using AutoMapper;
using Depot.API;
using Depot.API.Users.Requests;
using Depot.API.Users.Responses;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Repositories.Employees;
using Depot.Repositories.Users;

namespace Depot.Services.Users;

public class UsersService : IUsersService
{
    private readonly UserManager<User> _userManager;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;
    private readonly JwtHelper _jwtHelper;
        
    public UsersService(UserManager<User> userManager, IUsersRepository usersRepository, IMapper mapper, IEmployeesRepository employeesRepository, JwtHelper jwtHelper)
    {
        _userManager = userManager;
        _usersRepository = usersRepository;
        _mapper = mapper;
        _employeesRepository = employeesRepository;
        _jwtHelper = jwtHelper;
    }

    public async Task<LoginResponse?> Authenticate(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password) ||
            user.Archived) return null;
        var userRole = (await _userManager.GetRolesAsync(user)).First();

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userRole)
        };

        var token = _jwtHelper.GetToken(authClaims);

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            userRole,
            token.ValidTo
        );
    }

    public async Task<UserGetResponse> GetAsync(int id)
    {
        var user = await _usersRepository.GetAsync(id);
        return _mapper.Map<UserGetResponse>(user);
    }

    public async Task<UserGetResponse> GetAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return _mapper.Map<UserGetResponse>(user);
    }

    public async Task<IEnumerable<UserGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null)
    {
        var users = await _usersRepository.GetAllAsync(paginationParameters);
        return _mapper.Map<IEnumerable<UserGetResponse>>(users);
    }

    public async Task DeleteAsync(string jwt, int id)
    {
        var userFromJwt = await _jwtHelper.GetUserFromJwt(jwt);
        if (userFromJwt.Id == id)
        {
            throw new UserSelfRemoveException("Can't delete yourself");
        }
        await _usersRepository.DeleteAsync(id);
    }

    public async Task ArchiveAsync(string jwt, int id)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var user = await _jwtHelper.GetUserFromJwt(jwt);
        if (user.Id == id)
        {
            throw new UserSelfRemoveException("Can't archive yourself");
        }
        await _usersRepository.ArchiveAsync(id);
        await _employeesRepository.ArchiveAsync(user.EmployeeId);
        scope.Complete();
    }

    // TODO: костыль для создания employee при создании user, нужно сделать нормально
    public async Task<UserCreateResponse> CreateAsync(UserCreateRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new Exception("User creation failed");
        await _userManager.AddToRoleAsync(user, request.Role);
        var savedUser = await _userManager.FindByNameAsync(user.UserName);
        var savedEmployee = savedUser.Employee;
        savedUser.EmployeeId = savedEmployee.Id;
        await _userManager.UpdateAsync(savedUser);
        savedUser = _userManager.FindByNameAsync(user.UserName).Result;
        scope.Complete();
        return _mapper.Map<UserCreateResponse>(savedUser);
        
    }
        
    public async Task<UserUpdateResponse> PatchAsync(int id, UserPatchRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var oldUser = await _usersRepository.GetAsync(id);
        var oldRole = (await _userManager.GetRolesAsync(oldUser)).FirstOrDefault();
        var mappedOldUser = _mapper.Map(request, oldUser);
        var updatedUser = await _usersRepository.UpdateAsync(mappedOldUser);
        await _userManager.AddToRoleAsync(updatedUser, request.Role);
        if (oldRole != request.Role)
        {
            await _userManager.RemoveFromRoleAsync(updatedUser, oldRole);
        }
        var mappedUpdatedUser = _mapper.Map<UserUpdateResponse>(updatedUser);
        mappedUpdatedUser.Role = (await _userManager.GetRolesAsync(updatedUser)).FirstOrDefault();
        scope.Complete();
        return mappedUpdatedUser;
    }

    public async Task EditPassword(string jwt, string newPassword)
    {
        var user = await _jwtHelper.GetUserFromJwt(jwt);
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
    }

    public UserGetResponse GetByEmployeeId(int employeeId)
    {
        var user = _usersRepository.GetAllByPredicate(u => u.EmployeeId == employeeId).FirstOrDefault();
        return _mapper.Map<UserGetResponse>(user);
    }
}