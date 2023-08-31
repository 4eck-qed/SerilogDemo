var builder = Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<SerilogDemo.Server.Startup>();
    });

var app = builder.Build();
app.Run();