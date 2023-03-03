using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("[controller]")]
public class UserController : ArcheryController
{
    private readonly ILogger<UserController> _logger;
    private readonly UserRepository _repository;

    public UserController(ILogger<UserController> logger, UserRepository repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetUserController")]
    public IEnumerable<User> Get()
    {
        return _repository.GetAllUsers();
    }
}
