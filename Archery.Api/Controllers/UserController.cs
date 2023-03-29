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
        try
        {
            return Ok(_repository.GetAllUsers());
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("AddUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(_repository.AddUser(user));
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
    }

}
