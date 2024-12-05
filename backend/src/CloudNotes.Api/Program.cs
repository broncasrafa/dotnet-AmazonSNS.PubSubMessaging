using CloudNotes.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.Configure();

await app.RunAsync();
