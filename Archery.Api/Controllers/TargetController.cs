using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;
using Archery.Model.ApiHelper;

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
        return Ok(_repository.GetAllTargets());
    }

    [HttpPost]
    [Route("AddTarget")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Target(NewTarget newTarget)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(_repository.AddTarget(newTarget));
    }
}
