using Microsoft.AspNetCore.Mvc;
using SerilogDemo.Service;

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
        services.AddTransient<DemoService>();
        services.AddControllers(ConfigureControllers);
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseEndpoints(ep =>
        {
            ep.MapControllers();
        });
    }

    private void ConfigureControllers(MvcOptions options) { }
}
