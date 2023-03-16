using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class ParcourController : ArcheryController
{
    private readonly ParcourRepository _repository;

    public  ParcourController(ILogger<ParcourController> logger, ParcourRepository repository) : base(logger)
    {
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
    [Route("GetParcour/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetParcour(int id)
    {
        return Ok(_repository.GetParcour(id));
    }


    [HttpPost]
    [Route("AddParcour")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddParcour(Parcour parcour)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(_repository.AddParcour(parcour));
    }

}
