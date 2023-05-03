using System.Formats.Asn1;
using Depot.API;
using Depot.API.Users.Requests;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class GenericDictionaryCrudController<TService, TGetResponse, TCreateRequest, TUpdateRequest, TCreateResponse, TUpdateResponse> : ControllerBase, IGenericDictionaryCrudController<TCreateRequest, TUpdateRequest> where TService : IGenericDictionaryCrudService<TGetResponse, TUpdateRequest, TCreateRequest, TUpdateResponse, TCreateResponse>
    where TGetResponse : class where TUpdateRequest : class where TCreateRequest : class 
    where TUpdateResponse : class where TCreateResponse : class
{
    protected TService _service;

    protected GenericDictionaryCrudController(TService service)
    {
        _service = service;
    }

    [HttpGet("to_get")]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] int? id = null, [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            if (id is not null)
            {
                return Ok(await _service.GetAsync(id.Value));    
            }
            return Ok(await _service.GetAllAsync(paginationParameters));
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

    [HttpDelete("to_delete")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok();
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

    [HttpPatch("to_archive")]
    [Authorize]
    public async Task<IActionResult> ArchiveAsync([FromQuery] int id)
    {
        try
        {
            await _service.ArchiveAsync(id);
            return Ok();
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

    [HttpPost("to_create")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] TCreateRequest request)
    {
        try
        {
            return Ok(await _service.CreateAsync(request));
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

    [HttpPut("to_update")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromQuery] int id, [FromBody] TUpdateRequest request)
    {
        try
        {
            return Ok(await _service.UpdateAsync(id, request));
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