using System.Transactions;
using AutoMapper;
using Depot.API;
using Depot.API.Employees.Requests;
using Depot.API.Employees.Responses;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Models.Users;
using Depot.Repositories.Employees;
using Depot.Repositories.Users;
using Microsoft.AspNetCore.Identity;

namespace Depot.Services.Employees;

public class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly JwtHelper _jwtHelper;

    public EmployeesService(IEmployeesRepository employeesRepository, IMapper mapper, IUsersRepository usersRepository, UserManager<User> userManager, JwtHelper jwtHelper)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
        _usersRepository = usersRepository;
        _userManager = userManager;
        _jwtHelper = jwtHelper;
    }

    public async Task<EmployeeGetResponse> GetAsync(int id)
    {
        var employee = await _employeesRepository.GetAsync(id);
        return _mapper.Map<EmployeeGetResponse>(employee);
    }
    
    public EmployeeGetResponse GetByUserId(int userId)
    {
        var employee = _employeesRepository.GetAllByPredicate(e => e.UserId == userId).FirstOrDefault();
        return _mapper.Map<EmployeeGetResponse>(employee);
    }

    public async Task<IEnumerable<EmployeeGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null)
    {
        var employees = await _employeesRepository.GetAllAsync(paginationParameters);
        return _mapper.Map<IEnumerable<EmployeeGetResponse>>(employees);
    }

    // TODO при удалении user удаляется и employee, но не наоборот, надо узнать почему
    public async Task DeleteAsync(string jwt, int id)
    {
        var employee = await _employeesRepository.GetAsync(id);
        var employeeFromJwt = await _jwtHelper.GetEmployeeFromJwt(jwt);
        if (employeeFromJwt.Id == id)
        {
            throw new UserSelfRemoveException("Can't delete yourself");
        }
        await _usersRepository.DeleteAsync(employee.UserId);
    }

    public async Task ArchiveAsync(string jwt, int id)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);   
        var employee = await _jwtHelper.GetEmployeeFromJwt(jwt);
        if (employee.Id == id)
        {
            throw new UserSelfRemoveException("Can't archive yourself");
        }
        
        await _employeesRepository.ArchiveAsync(id);
        await _usersRepository.ArchiveAsync(employee.UserId);
        scope.Complete();
    }

    public async Task<EmployeeUpdateResponse> UpdateAsync(int id, EmployeeUpdateRequest request)
    {
        var employee = await _employeesRepository.GetAsync(id);
        employee = _mapper.Map(request, employee);
        employee = await _employeesRepository.UpdateAsync(employee);
        return _mapper.Map<EmployeeUpdateResponse>(employee);
    }

    public async Task<EmployeeUpdateResponse> EditMeAsync(string jwt, EmployeeEditMeRequest request)
    {
        var employee = await _jwtHelper.GetEmployeeFromJwt(jwt);
        employee = _mapper.Map(request, employee);
        var updatedEmployee = await _employeesRepository.UpdateAsync(employee);
        return _mapper.Map<EmployeeUpdateResponse>(updatedEmployee);
    }

}