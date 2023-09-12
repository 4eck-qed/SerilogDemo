using Microsoft.AspNetCore.Mvc;
using SerilogDemo.Service;
using Simplic.OxS.Logging.Extensions;

namespace Simplic.OxS.Logging.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> logger;
    private readonly DemoService service;

    public DemoController(ILogger<DemoController> logger, DemoService service)
    {
        this.logger = logger;
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        logger.Debug("with scope");
        logger.LogDebug("no scope");

        service.DoSomething();

        return Ok();
    }
}
