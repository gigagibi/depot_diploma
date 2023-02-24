using AutoMapper;
using Depot.API.Departments.Requests;
using Depot.API.Departments.Responses;
using Depot.Models.Departments;
using Depot.Repositories;
using Depot.Repositories.Departments;

namespace Depot.Services.Departments;

public class DepartmentService : GenericDictionaryCrudService<Department, IDepartmentsRepository, DepartmentGetResponse, DepartmentUpdateRequest, DepartmentCreateRequest, DepartmentUpdateResponse, DepartmentCreateResponse>, IDepartmentService
{
    public DepartmentService(IDepartmentsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}