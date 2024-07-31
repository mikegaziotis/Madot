using Madot.Interface.API;
using Madot.Interface.BlazorWebUI;
using Madot.Interface.BlazorWebUI.Components;
using Microsoft.AspNetCore.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadConfiguration(builder.Configuration);

builder.Services.AddBlazorBootstrap();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.ConfigureRefitClients(AppSettings.ApiInternalUrl);

var app = builder.Build();

app.UseExceptionHandler("/Error", createScopeForErrors: true);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();