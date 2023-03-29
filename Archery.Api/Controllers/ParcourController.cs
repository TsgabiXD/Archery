using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Archery.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ParcourController : ArcheryController
{
    private readonly ParcourRepository _repository;

    public ParcourController(ILogger<ParcourController> logger, ParcourRepository repository) : base(logger)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("GetParcours")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_repository.GetAllParcours());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetParcour/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetParcour(int id)
    {
        try
        {
            return Ok(_repository.GetParcour(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost]
    [Route("AddParcour")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddParcour(Parcour parcour)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(_repository.AddParcour(parcour));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
