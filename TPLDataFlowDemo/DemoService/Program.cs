using DemoService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DataFlowDemo";
});

builder.Services.AddSingleton<JokeProvider>();
builder.Services.AddHostedService<WindowsBackgroundService>();

var host = builder.Build();
host.Run();