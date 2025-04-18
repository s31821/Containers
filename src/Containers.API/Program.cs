using System.Text.Json.Nodes;
using Containers.Application;
using Containers.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UniversityDatabase");

builder.Services.AddTransient<IContainerService, ContainerService>(_ => new ContainerService(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/containers", (IContainerService containerService) =>
{
    try
    {
        return Results.Ok(containerService.GetAllContainers());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/containers", (IContainerService containerService, Container container) =>
{
    try
    {
        bool result = containerService.CreateContainer(container);
        if (result)
        {
            return Results.Created();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
});

app.MapPost("/api/containers", async (IContainerService containerService, HttpRequest request) =>
{
    using (var reader = new StreamReader(request.Body))
    {
        string rawJson = await reader.ReadToEndAsync();

        var json = JsonNode.Parse(rawJson);
        var specifiedType = json["type"];
        if (specifiedType != null && specifiedType.ToString() == "Standard")
        {

        }
    }
});

app.Run();