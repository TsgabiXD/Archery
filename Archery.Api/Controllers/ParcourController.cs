using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("[controller]")]
public class ParcourController : ArcheryController
{
    private readonly ILogger<ParcourController> _logger;
    private readonly ParcourRepository _repository;

    public ParcourController(ILogger<ParcourController> logger, ParcourRepository repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
    }    

    [HttpGet]
    [Route("GetParcours")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(_repository.GetAllParcours());
    }

    [HttpGet]
    [Route("AddParcour")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddParcours(string name, string location, string nickname)
    {
        return Ok(_repository.AddParcours(name, location, nickname));
    }

}
