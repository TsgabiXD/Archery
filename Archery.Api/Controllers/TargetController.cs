using Microsoft.AspNetCore.Authorization;

using Archery.Repository;
using Archery.Model.ApiHelper;

namespace Archery.Api.Controllers;


[Authorize("User")] // TODO check this
[Route("api/[controller]")]
public class TargetController : ArcheryController
{
    private readonly TargetRepository _repository;

    public TargetController(ILogger<TargetController> logger, TargetRepository repository) : base(logger)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetMyTargets(int id)
    {
        try
        {
            return Ok(_repository.GetMyTargets(id));
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
            return Ok(_repository.AddTarget(newTarget));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
