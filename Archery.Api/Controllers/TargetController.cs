using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class TargetController : ArcheryController
{
    private readonly ILogger<TargetController> _logger;
    private readonly TargetRepository _repository;

    public TargetController(ILogger<TargetController> logger, TargetRepository repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [Route("GetTargets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_repository.GetAllTargets());
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
    public IActionResult Target(int arrowCount, int hitArea)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(_repository.AddTarget(arrowCount, hitArea));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
