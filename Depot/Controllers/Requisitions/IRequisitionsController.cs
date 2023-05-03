using Depot.API.Requisitions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Requisitions;

public interface IRequisitionController : IGenericDictionaryCrudController<RequisitionsCreateRequest, RequisitionsUpdateRequest>
{
    Task<IActionResult> SetStatus(int id, int statusId);
}