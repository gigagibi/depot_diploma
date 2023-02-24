using Depot.API;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Services.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Transactions;

[ApiController]
[Route("api/v1/[controller]")]
public class TransactionsController : ControllerBase, ITransactionsController
{
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [HttpGet("to_get")]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] int? id, [FromQuery] int? invNumber, [FromQuery] int? entityId, [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            if (id is not null)
            {
                return Ok(await _transactionsService.GetAsync(id.Value));
            }
            if (invNumber is not null)
            {
                return Ok(await _transactionsService.GetByEntityInvNumberAsync(invNumber.Value, paginationParameters));
            }
            return entityId is not null ? Ok(await _transactionsService.GetByEntityIdAsync(entityId.Value, paginationParameters)) : Ok(await _transactionsService.GetAllAsync(paginationParameters));
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}