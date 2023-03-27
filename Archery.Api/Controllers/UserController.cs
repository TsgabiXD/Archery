using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class UserController : ArcheryController
{
    private readonly UserRepository _repository;

    public UserController(ILogger<UserController> logger, UserRepository repository) : base(logger)
    {
        _repository = repository;
    }

    [Authorize]
    [HttpGet]
    [Route("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(_repository.GetAllUsers());
    }

    
    [HttpGet]
    [Route("CheckUser/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CheckUser(string id)
    {
        return Ok(_repository.CheckUser(id));
    }

    // [HttpPost]
    // [Route("AddUser")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // public IActionResult AddUser(User user)
    // {
    //     return Ok(_repository.AddUser(user));
    // }
}
