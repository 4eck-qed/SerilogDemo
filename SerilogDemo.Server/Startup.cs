using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SerilogDemo.Server;

public class Startup
{
    public IConfiguration Configuration { get; protected set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(ConfigureControllers);

        // services.AddLogger<WebSocketClient>(o => o.Loggers = LoggeMask.Console);
        // coole features
        // - rolling interval für file logging (wann neues log file angelegt werden soll bspw. daily)
        // - asp net integration
        // --> über appsettings konfigurierbar
        // --> 
        // - komplett customizable log formatting
        // --> guter use case: eigene extensions für ILogger die [CallerMemberName] holen
        //                      und das für log scope nutzen

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddSerilog(new SerilogSettings
            {
                LogToConsole = true,
                LogToFile = true,
                ConsoleLogOptions = new ConsoleLogOptions
                {
                    LogLevel = LogLevel.Trace
                },
                FileLogOptions = new FileLogOptions
                {
                    LogLevel = LogLevel.Information,
                    OutDir = "logs",
                    FileName = "should that be here?"
                }
            }
            .CreateConfiguration()
            .CreateLogger());
        });

        RegisterServices(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        OnConfigure(app, env);
        app.UseEndpoints(MapEndpoints);
    }

    private void ConfigureControllers(MvcOptions options)
    {
    }

    private void RegisterServices(IServiceCollection services)
    {

    }

    private void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }

    private void MapEndpoints(IEndpointRouteBuilder builder)
    {

    }
}