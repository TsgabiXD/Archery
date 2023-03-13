﻿using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class EventController : ArcheryController
{

    private readonly ILogger<ParcourController> _logger;
    private readonly ParcourRepository _repository;

    public EventController(ILogger<ParcourController> logger, ParcourRepository repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
    }


    [HttpGet]
    [Route("StartEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(_repository.GetAllParcours());
    }
}