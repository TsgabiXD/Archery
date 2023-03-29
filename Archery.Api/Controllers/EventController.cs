using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;
using Archery.Model.ApiHelper;

namespace Archery.Api.Controllers;

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
        try
        {
            return Ok(_repository.StartEvent(startEvent));
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }



    }

    [HttpPost]
    [Route("EndEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EndEvent(int stopEvent)
    {
        try
        {
            return Ok(_repository.EndEvent(stopEvent));
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
    }

}
