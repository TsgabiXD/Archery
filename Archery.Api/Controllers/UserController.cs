using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class UserController : ArcheryController
{
    private readonly ILogger<UserController> _logger;
    private readonly UserRepository _repository;

    public UserController(ILogger<UserController> logger, UserRepository repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [Route("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(_repository.GetAllUsers());
    }

    [HttpPost]
    [Route("AddUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddUser(string firstname, string lastname, string nickname)
    {
        return Ok(_repository.AddUser(firstname,lastname,nickname));
    }

}
