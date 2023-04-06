using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;
using Archery.Model.ApiHelper;

namespace Archery.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class EventController : ArcheryController
{
    private readonly EventRepository _repository;

    public EventController(ILogger<EventController> logger, EventRepository repository) : base(logger)
    {
        _repository = repository;
    }

    [HttpPost]
    [Route("StartEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult StartEvent(NewEvent startEvent)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.StartEvent(startEvent));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetAdminViewElements")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAdminViewElements()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.GetAdminViewElements());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // TODO BadRequest durch richtige funktion ersaetzen
        }
    }

    [HttpPost]
    [Route("EndEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult EndEvent(int stopEvent)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_repository.EndEvent(stopEvent));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
