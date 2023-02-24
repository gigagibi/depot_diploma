using Depot.API;
using Depot.API.Entities.Entity.Requests;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Entities;

[ApiController]
[Route("api/v1/[controller]")]
public class EntityController : ControllerBase, IEntityController
{
    private readonly IEntitiesService _entitiesService;

    public EntityController(IEntitiesService entitiesService)
    {
        _entitiesService = entitiesService;
    }

    [HttpGet("to_get")]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] int? id, [FromQuery] int? invNumber, [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            if (id is not null || invNumber is not null)
            {
                var entity = await _entitiesService.GetAsync(id, invNumber);
                return Ok(entity);
            }
            var entities = await _entitiesService.GetAllAsync(paginationParameters);
            return Ok(entities);
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("parts/to_get")]
    [Authorize]
    public async Task<IActionResult> GetPartsAsync([FromQuery] int? id, [FromQuery] int? invNumber, [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            var parts = await _entitiesService.GetPartsAsync(id, invNumber, paginationParameters);
            return Ok(parts);
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("father/to_get")]
    [Authorize]
    public async Task<IActionResult> GetFatherAsync([FromQuery] int? id, [FromQuery] int? invNumber)
    {
        try
        {
            var father = await _entitiesService.GetFatherAsync(id, invNumber);
            return Ok(father);
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("to_generate_inv")]
    [Authorize]
    public async Task<IActionResult> GenerateInvNumberAsync()
    {
        try
        {
            var invNumber = await _entitiesService.GenerateInvNumberAsync();
            return Ok(invNumber);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("to_check_inv_exists")]
    [Authorize]
    public async Task<IActionResult> CheckIfInvNumberExists([FromQuery] int invNumber)
    {
        return await _entitiesService.CheckIfInvNumberExists(invNumber) ? Ok("Inv number exist") : NotFound("InvNumber does not exist");
    }

    [HttpPatch("to_reserve")]
    [Authorize]
    public async Task<IActionResult> ReserveAsync([FromQuery] int? id, [FromQuery] int? invNumber)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.ReserveAsync(id, invNumber, jwt));
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (EntityReservationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPatch("to_unreserve")]
    [Authorize]
    public async Task<IActionResult> UnreserveAsync([FromQuery] int? id, [FromQuery] int? invNumber)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.UnreserveAsync(id, invNumber, jwt));
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (EntityReservationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("to_update")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromQuery] int? id, [FromQuery] int? invNumber, [FromBody] EntityUpdateRequest request)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.UpdateAsync(id, invNumber, request, jwt));
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (InvNumberAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("to_create")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] EntityCreateRequest request)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.CreateAsync(request, jwt));
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (InvNumberAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("to_delete")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromQuery] int? id, [FromQuery] int? invNumber)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.DeleteAsync(id, invNumber, jwt));
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPatch("to_archive")]
    [Authorize]
    public async Task<IActionResult> ArchiveAsync([FromQuery] int? id, [FromQuery] int? invNumber)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _entitiesService.ArchiveAsync(id, invNumber, jwt));
        }
        catch (ModelNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid arguments, it's possible that id and invNumber are both null");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}