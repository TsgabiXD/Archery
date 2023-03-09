using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("[controller]")]
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
    public IActionResult Target(int arrowCount,int hitArea)
    {
        return Ok(_repository.AddTarget(arrowCount, hitArea));
    }
}
