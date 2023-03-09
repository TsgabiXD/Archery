using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class ParcourController : ArcheryController
{
    private readonly ILogger<ParcourController> _logger;
    private readonly ParcourRepository _repository;

    public  ParcourController(ILogger<ParcourController> logger, ParcourRepository repository) : base(logger)
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

    [HttpPost]
    [Route("AddParcour")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddParcour(string name, string location, int animalNumber)
    {
        return Ok(_repository.AddParcour(name, location, animalNumber));
    }

    [HttpGet]
    [Route("GetParcour")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetParcour()
    {
        return Ok(_repository.GetParcour());
    }

}
