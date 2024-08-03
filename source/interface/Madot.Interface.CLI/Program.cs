// See https://aka.ms/new-console-template for more information
using ConsoleAppFramework;
using Madot.Interface.CLI;
using Madot.Interface.CLI.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();
services.ConfigureServices();

await using var serviceProvider = services.BuildServiceProvider();
ConsoleApp.ServiceProvider = serviceProvider;

var app = ConsoleApp.Create();

app.Add("api-get", ApiGetCommandHandler.Send);
app.Add("api-insert", ApiInsertCommandHandler.Send);
app.Add("docs-merge", DocsMergeCommandHandler.Send);
app.Add("apiversion-publish", ApiVersionPublishCommandHandler.Send);

await app.RunAsync(args);


