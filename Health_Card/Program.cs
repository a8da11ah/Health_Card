using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Health_Card.Data;
using Health_Card.Interface.servant;
using Health_Card.Interface;
using Health_Card.Repository;
using Health_Card.Service;
using System.Data; // You'll need this for IDbConnection




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Health_Card REST API",
        Version = "v1"
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var dapperContext = sp.GetRequiredService<DapperContext>();
    return dapperContext.CreateConnection();
});

// Register repository with connection from DapperContext
builder.Services.AddScoped<IServantRepository, ServantRepository>();
builder.Services.AddScoped<IServantService, ServantService>();


    
// Database configuration
// builder.Services.DapperContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    // app.MapScalarApiReference();
    
    
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    // Optional: Serve Swagger UI if you want it side-by-side with Scalar
    // app.UseSwaggerUI();

    // Map Scalar UI at /scalar and tell it to pick up your OpenAPI document(s)
    app.MapScalarApiReference(options =>
    {
        options.Title = "Your API Reference";
        options.Theme = ScalarTheme.BluePlanet; // optional: choose a theme like BluePlanet, Mars, Solarized etc.
        // You can add or customize documents here if multiple OpenAPI specs exist
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
