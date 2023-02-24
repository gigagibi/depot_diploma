using Depot.API.Departments.Requests;
using Depot.Models.Departments;

namespace Depot.Controllers.Departments;

public interface IDepartmentController : IGenericDictionaryCrudController<DepartmentCreateRequest, DepartmentUpdateRequest>
{
}