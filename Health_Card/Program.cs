using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Health_Card.Data;
using Health_Card.Interface;
using Health_Card.Repository;
using Health_Card.Service;
using System.Data;
using Health_Card.Model; // You'll need this for IDbConnection
using Microsoft.AspNetCore.Mvc;

using Health_Card.Dto;




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
builder.Services.AddScoped<IRepositoryBase<Servant,ServantFilter>, ServantRepository>();
builder.Services.AddScoped<IServiceBase<Servant,ServantFilter>, ServantService>();

builder.Services.AddScoped<IRepositoryBase<ChronicDisease,ChronicDiseaseFilter>, ChronicDiseaseRepository>();
builder.Services.AddScoped<IServiceBase<ChronicDisease,ChronicDiseaseFilter>, ChronicDiseaseService>();



builder.Services.AddScoped< IRepositoryBase<GeneralRemark, GeneralRemarkFilter>, GeneralRemarkRepository>();
builder.Services.AddScoped< IServiceBase<GeneralRemark, GeneralRemarkFilter>, GeneralRemarkService>();

builder.Services.AddScoped< IRepositoryBase<MedicalReferral, MedicalReferralFilter>, MedicalReferralRepository>();
builder.Services.AddScoped< IServiceBase<MedicalReferral, MedicalReferralFilter>, MedicalReferralService>();

builder.Services.AddScoped< IRepositoryBase<ServantChronicTreatment,ServantChronicTreatmentFilter>, ServantChronicTreatmentRepository>();
builder.Services.AddScoped<IServiceBase<ServantChronicTreatment,ServantChronicTreatmentFilter>, ServantChronicTreatmentService>();

builder.Services.AddScoped<IRepositoryBase<ServantMedicalReview,ServantMedicalReviewFilter>, ServantMedicalReviewRepository>();
builder.Services.AddScoped<IServiceBase<ServantMedicalReview,ServantMedicalReviewFilter>, ServantMedicalReviewService>();


builder.Services.AddScoped< IRepositoryBase<SurgicalOperation,SurgicalOperationFilter>, SurgicalOperationRepository>();
builder.Services.AddScoped<IServiceBase<SurgicalOperation, SurgicalOperationFilter>, SurgicalOperationService>();


builder.Services.AddScoped<IRepositoryBase<Vaccination,VaccinationFilter>, VaccinationRepository>();
builder.Services.AddScoped< IServiceBase<Vaccination,VaccinationFilter>, VaccinationService>();


builder.Services.AddScoped< IRepositoryBase<WorkInjury,WorkInjuryFilter>, WorkInjuryRepository>();
builder.Services.AddScoped<IServiceBase<WorkInjury,WorkInjuryFilter>, WorkInjuryService>();


    
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
