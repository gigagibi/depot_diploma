using Depot.API;
using Depot.API.Authentication.Requests;
using Depot.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Depot.API.Users.Requests;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Depot.Controllers.Users;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase, IUsersController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("to_login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _usersService.Authenticate(request);
            if (result == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("to_get")]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] int? id = null, 
        [FromQuery] string? username = null, 
        [FromQuery] int? employeeId = null,
        [FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] bool all = false)
    {
        try
        {
            if (all)
            {
                paginationParameters = null;
            }
            if (id is not null)
            {
                return Ok(await _usersService.GetAsync(id.Value));
            }
            if (username is not null)
            {
                return Ok(await _usersService.GetAsync(username));
            }

            if (employeeId is not null)
            {
                return Ok(_usersService.GetByEmployeeId(employeeId.Value));
            }
            return Ok(await _usersService.GetAllAsync(paginationParameters));

        }
        catch (ModelNotFoundException e)
        {
            return NotFound("User not found");
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
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            await _usersService.DeleteAsync(jwt, id);
            return Ok($"User with id = {id} deleted successfully");
        }
        catch (ModelNotFoundException e)
        {
            return NotFound("User not found");
        }
        catch (UserSelfRemoveException e)
        {
            return NotFound(e.Message);
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
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            await _usersService.ArchiveAsync(jwt, id);
            return Ok($"User with id = {id} archived successfully");
        }
        catch (ModelNotFoundException e)
        {
            return NotFound("User not found");
        }
        catch (UserSelfRemoveException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("to_create")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] UserCreateRequest request)
    {
        try
        {
            return Ok(await _usersService.CreateAsync(request));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("to_update")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(int id, UserPatchRequest request)
    {
        try
        {
            return Ok(await _usersService.PatchAsync(id, request));
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

    [HttpPatch("edit_pass")]
    [Authorize]
    public async Task<IActionResult> EditPassword([FromBody] string newPassword)
    {
        try
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("The authorization header is either empty or isn't Bearer");
            var jwt = authHeader[7..];
            await _usersService.EditPassword(jwt, newPassword);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}