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
//app.Add("apiVersion-get", ()=>Console.WriteLine("Empty"));
app.Add("apiversion-publish", (string apiId, string versionNumber = "1.0", bool autoIncrement = false, bool updateLatest = false )=>Console.WriteLine("Empty"));

await app.RunAsync(args);


