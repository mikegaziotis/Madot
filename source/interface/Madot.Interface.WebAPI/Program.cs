using Madot.Interface.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadAppConfiguration(builder.Configuration);

builder.Services.ConfigureServices();

var app = builder.Build();

app.AddMiddleware();

await app.RunAsync();

