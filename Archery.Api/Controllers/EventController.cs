using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class EventController : ArcheryController
{
    private readonly EventRepository _repository;

    public EventController(ILogger<EventController> logger, EventRepository repository) : base(logger)
    {
        _repository = repository;
    }

    //[HttpGet]
    //[Route("GetEvent")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public IActionResult GetEvent(int id)
    //{
    //    return Ok(_repository());
    //}


    [HttpPost]
    [Route("StartEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult StartEvent(Event startEvent)
    {

        return Ok(_repository.StartEvent(startEvent));
    }

    [HttpPost]
    [Route("EndEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EndEvent(Event stopEvent)
    { 


        return Ok(_repository.EndEvent(stopEvent));
    }

}
