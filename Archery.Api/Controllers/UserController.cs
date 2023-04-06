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
    [Route("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.GetAllUsers());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("CheckUser/{id}")]
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
}
