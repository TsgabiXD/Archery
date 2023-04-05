using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Archery.Repository;
using Archery.Model.ApiHelper;

namespace Archery.Api.Controllers;


[Route("api/[controller]")]
public class TargetController : ArcheryController
{
    private readonly TargetRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TargetController(ILogger<TargetController> logger, TargetRepository repository, IHttpContextAccessor httpContextAccessor) : base(logger)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetMyTargets()
    {
        try
        {
            var claims = _httpContextAccessor?.HttpContext?.User.Claims;
            var role = claims?.Single(c => c.Type == ClaimTypes.Role).Value!;

            if (role != "Admin" && role != "User")
                return Unauthorized();

            return Ok(_repository.GetMyTargets(int.Parse(claims?.Single(c => c.Type == "userId").Value!)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("AddTarget")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Target(NewTarget newTarget)
    {

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var claims = _httpContextAccessor?.HttpContext?.User.Claims;
            var role = claims?.Single(c => c.Type == ClaimTypes.Role).Value!;

            if (role != "Admin" && role != "User")
                return Unauthorized();

            return Ok(_repository.AddTarget(newTarget, int.Parse(claims.Single(c => c.Type == "userId").Value!)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
