// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = CoconaApp.CreateBuilder();

builder.Logging.AddFilter(x => x == LogLevel.Error);
builder.Services.AddHttpClient();

var app = builder.Build();
Console.OutputEncoding = Encoding.UTF8;

app.AddCommand("register", ([Option('n')]string name, 
    [Option('d')]string displayName, 
    [Option('i')]bool? isInternal,
    [Option('b')]bool? isBeta,
    [Option('a')]bool? isAvailable,
    [Option('o')]int? orderId
) =>
{
   
    Console.WriteLine($"[\u2713] Successfully registered API with unique name: {name}");
    var guid = Guid.NewGuid();
    Console.WriteLine($"Your key is: {guid}");
    //var result = Hashing.Sha256(guid.ToString());
    //Console.WriteLine($"Your hash is: {result}");
});

app.Run();