using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcourController : ControllerBase
{
    private readonly ILogger<ParcourController> _logger;
    private readonly ParcourRepository _repository;

    public ParcourController(ILogger<ParcourController> logger, ParcourRepository repository)
    {
        _logger = logger;
        _repository=repository;
    }

    [HttpGet(Name = "GetParcourController")]
    public IEnumerable<Parcour> Get()
    {
        return _repository.GetAllParcours().ToArray();
    }
}
