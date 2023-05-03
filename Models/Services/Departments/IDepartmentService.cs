using AutoMapper;
using Depot.API.Departments.Requests;
using Depot.API.Departments.Responses;
using Depot.Models.Departments;
using Depot.Repositories;
using Depot.Repositories.Departments;

namespace Depot.Services.Departments;

public interface IDepartmentService : IGenericDictionaryCrudService<DepartmentGetResponse, DepartmentUpdateRequest, DepartmentCreateRequest, DepartmentUpdateResponse, DepartmentCreateResponse>
{

}