using Depot.API;
using Depot.API.Employees.Requests;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace Depot.Controllers.Employees;

[Route("api/v1/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase, IEmployeesController
{
    private readonly IEmployeesService _employeesService;

    public EmployeesController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpGet("to_get")]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] int? id = null, [FromQuery] int? userId = null, [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            if (id is not null)
            {
                return Ok(await _employeesService.GetAsync(id.Value));
            }
            if (userId is not null)
            {
                return Ok(_employeesService.GetByUserId(userId.Value));
            }
            return Ok(await _employeesService.GetAllAsync(paginationParameters));
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
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
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            await _employeesService.DeleteAsync(jwt, id);
            return Ok($"Employee with id = {id} deleted successfully");
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (UserSelfRemoveException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
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
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            await _employeesService.ArchiveAsync(jwt, id);
            return Ok($"Employee with id = {id} archived successfully");
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (UserSelfRemoveException e)
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
    public async Task<IActionResult> UpdateAsync([FromQuery] int id, [FromBody] EmployeeUpdateRequest request)
    {
        try
        {
            return Ok(await _employeesService.UpdateAsync(id, request));
        }
        catch (ModelNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("edit_me")]
    [Authorize]
    public async Task<IActionResult> EditMeAsync([FromBody] EmployeeEditMeRequest request)
    {
        try
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            return Ok(await _employeesService.EditMeAsync(jwt, request));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}