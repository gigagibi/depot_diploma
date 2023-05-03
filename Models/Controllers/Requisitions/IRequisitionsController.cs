using Depot.API.Requisitions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Requisitions;

public interface IRequisitionsController : IGenericDictionaryCrudController<RequisitionCreateRequest, RequisitionUpdateRequest>
{
    Task<IActionResult> SetStatus(int id, int statusId);
}