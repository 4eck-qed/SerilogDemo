using log4net;
using Microsoft.Extensions.Logging;
using NLog;
using Simplic.OxS.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

Console.WriteLine("-- TESTING THE LOGGER: aws --");

var logger = new Serilog.LoggerConfiguration()
    .MinimumLevel.Fatal()
    .CreateLogger();


Console.WriteLine("-- ^^ >> DONE << ^^ --");