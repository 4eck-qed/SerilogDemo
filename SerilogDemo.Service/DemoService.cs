using Microsoft.Extensions.Logging;
using Simplic.OxS.Logging.Extensions;

namespace SerilogDemo.Service;

public class DemoService
{
    private readonly ILogger<DemoService> logger;

    public DemoService(ILogger<DemoService> logger)
    {
        this.logger = logger;
    }

    public void DoSomething()
    {
        logger.Info("doing something..");
        logger.Error(new Exception("demo error"));
    }
}
