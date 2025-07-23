using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Health_Card.Data;
using Health_Card.Interface.servant;
using Health_Card.Interface;
using Health_Card.Interface.GeneralRemark;
using Health_Card.Interface.MedicalReferral;
using Health_Card.Interface.ServantChronicTreatment;
using Health_Card.Interface.ServantMedicalReview;
using Health_Card.Interface.SurgicalOperation;
using Health_Card.Interface.Vaccination;
using Health_Card.Interface.WorkInjury;
using Health_Card.Repository;
using Health_Card.Service;
using System.Data; // You'll need this for IDbConnection
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;




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

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Validation Error",
            Type = "https://tools.ietf.org/html/rfc4918#section-11.2",
            Detail = "One or more validation errors occurred."
        };

        return new UnprocessableEntityObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };
    };
});
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

builder.Services.AddScoped<IChronicDiseaseRepository, ChronicDiseaseRepository>();
builder.Services.AddScoped<IChronicDiseaseService, ChronicDiseaseService>();

builder.Services.AddScoped<IGeneralRemarkRepository, GeneralRemarkRepository>();
builder.Services.AddScoped<IGeneralRemarkService, GeneralRemarkService>();

builder.Services.AddScoped<IMedicalReferralRepository, MedicalReferralRepository>();
builder.Services.AddScoped<IMedicalReferralService, MedicalReferralService>();

builder.Services.AddScoped<IServantChronicTreatmentRepository, ServantChronicTreatmentRepository>();
builder.Services.AddScoped<IServantChronicTreatmentService, ServantChronicTreatmentService>();

builder.Services.AddScoped<IServantMedicalReviewRepository, ServantMedicalReviewRepository>();
builder.Services.AddScoped<IServantMedicalReviewService, ServantMedicalReviewService>();
builder.Services.AddScoped<ISurgicalOperationRepository, SurgicalOperationRepository>();
builder.Services.AddScoped<ISurgicalOperationService, SurgicalOperationService>();
builder.Services.AddScoped<IVaccinationRepository, VaccinationRepository>();
builder.Services.AddScoped<IVaccinationService, VaccinationService>();
builder.Services.AddScoped<IWorkInjuryRepository, WorkInjuryRepository>();
builder.Services.AddScoped<IWorkInjuryService, WorkInjuryService>();


    
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
