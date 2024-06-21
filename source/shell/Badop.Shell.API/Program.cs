using System.Text.Json.Serialization;
using Badop.Core.Application;
using Badop.Infrastructure.SqlServer;
using Badop.Shell.API.Endpoints;
using OpenTelemetry.Logs;
using Badop.Shell.API.Automapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDatabaseConfiguration();
builder.Services.RegisterApplicationContext();
builder.Services.RegisterHandlers();

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
});

builder.Services.AddAutomapper();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddRouting();

app.Run();

