using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class UserController : ArcheryController
{
    private readonly UserRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(ILogger<UserController> logger, UserRepository repository, IHttpContextAccessor httpContextAccessor) : base(logger)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetInactiveUsers()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.GetAllInactiveUsers());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CheckUser(string id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.CheckUser(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUsersRunningEvents()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var claims = _httpContextAccessor?.HttpContext?.User.Claims;

            return Ok(_repository.GetUsersRunningEvents(int.Parse(claims?.Single(c => c.Type == "userId").Value!)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "User,Admin")]
    [HttpDelete]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteUser(int? id)
    {
        try
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;

            var uid = int.Parse(claims?.Single(c => c.Type == "userId").Value!);
            if (id is not null)
            {

                return Ok(_repository.DeleteUser((int)id));
            }
            else return Ok(_repository.DeleteUser(int.Parse(claims?.Single(c => c.Type == "userId").Value!)));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }  
       
    }
}
