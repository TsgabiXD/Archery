using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;
using Archery.Model.ApiHelper;

namespace Archery.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class EventController : ArcheryController
{

    private readonly ILogger<EventController> _logger;
    private readonly EventRepository _repository;

    public EventController(ILogger<EventController> logger, EventRepository repository) : base(logger)
    {
        _logger = logger;
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
    public IActionResult StartEvent(NewEvent startEvent)
    {

        return Ok(_repository.StartEvent(startEvent));
    }

    [HttpPost]
    [Route("EndEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EndEvent(int stopEvent)
    { 
        return Ok(_repository.EndEvent(stopEvent));
    }

}
