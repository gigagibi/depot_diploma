using Depot.API.Requisitions.Requests;
using Depot.API.Requisitions.Responses;
using Depot.Exceptions;
using Depot.Services.Requisitions;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Requisitions;

public class RequisitionsController : GenericDictionaryCrudController<IRequisitionsService, RequisitionGetResponse, RequisitionCreateRequest, RequisitionUpdateRequest, RequisitionCreateResponse, RequisitionUpdateResponse>, IRequisitionsController
{
    public RequisitionsController(IRequisitionsService service) : base(service)
    {
    }

    [HttpPatch("to_set_status")]
    public async Task<IActionResult> SetStatus([FromQuery] int id, [FromQuery] int statusId)
    {
        try
        {
            return Ok(await _service.SetStatus(id, statusId));
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}