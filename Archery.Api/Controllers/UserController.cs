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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CheckUser(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(_repository.CheckUser(id));
    }
    
    [HttpGet]
    [Route("GetUsersRunningEvents/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUsersRunningEvents(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(_repository.GetUsersRunningEvents(id));
    }

    // [HttpPost]
    // [Route("AddUser")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public IActionResult AddUser(User user)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);

    //     return Ok(_repository.AddUser(user));
    // }

}
