using Serilog;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(
        (ctx, logging) =>
        {
            logging.ClearProviders();
            logging.AddSerilog(
                new SerilogSettings
                {
                    LogToConsole = true,
                    LogToFile = true,
                    ConsoleLogOptions = new ConsoleLogOptions { LogLevel = LogLevel.Trace },
                    FileLogOptions = new FileLogOptions
                    {
                        LogLevel = LogLevel.Information,
                        OutDir = "logs",
                        FileName = "demo"
                    }
                }
                    .CreateConfiguration()
                    .CreateLogger()
            );
        }
    )
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<SerilogDemo.Server.Startup>();
    });

var app = builder.Build();
app.Run();
