using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserController : ArcheryController
{
    private readonly UserRepository _repository;

    public UserController(ILogger<UserController> logger, UserRepository repository) : base(logger)
    {
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
    public IActionResult AddUser(User user)
    {
        return Ok(_repository.AddUser(user));
    }

}
