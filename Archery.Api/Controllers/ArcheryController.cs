using Microsoft.AspNetCore.Mvc;

using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[ApiController]
public class ArcheryController : ControllerBase
{
    protected readonly ILogger<ArcheryController> _logger;

    public ArcheryController(ILogger<ArcheryController> logger)
    {
        _logger = logger;
    }
}
