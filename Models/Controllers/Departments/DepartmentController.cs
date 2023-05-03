using Depot.API.Departments.Requests;
using Depot.API.Departments.Responses;
using Depot.Services.Departments;

namespace Depot.Controllers.Departments;

public class DepartmentController : GenericDictionaryCrudController<IDepartmentService, DepartmentGetResponse, DepartmentCreateRequest, DepartmentUpdateRequest, DepartmentCreateResponse, DepartmentUpdateResponse>, IDepartmentController
{
    public DepartmentController(IDepartmentService service) : base(service)
    {
    }
}