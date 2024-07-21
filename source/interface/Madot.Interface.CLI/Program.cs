// See https://aka.ms/new-console-template for more information
using Madot.Interface.CLI.CommandHandlers;

using ConsoleAppFramework;
using Madot.Interface.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;


var services = new ServiceCollection();
services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Error);
    x.AddZLoggerConsole();
});
services.AddTransient<ApiGetCommandHandler>();
services.ConfigureRefitClients();

await using var serviceProvider = services.BuildServiceProvider();
ConsoleApp.ServiceProvider = serviceProvider;

var app = ConsoleApp.Create();

app.Add("api-get", ApiGetCommandHandler.Send);

await app.RunAsync(args);


