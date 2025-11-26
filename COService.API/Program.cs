using Consul;
using COService.API.Endpoints;
using COService.Application.Mappings;
using COService.Application.Repositories;
using COService.Application.Services;
using COService.Infrastructure.Data;
using COService.Infrastructure.Repositories;
using COService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration Entity Framework Core
builder.Services.AddDbContext<COServiceDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("chaine");
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    });
});

// Configuration AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configuration Consul
var consulConfig = builder.Configuration.GetSection("Consul");
builder.Services.Configure<ConsulServiceOptions>(consulConfig);

// Client Consul
builder.Services.AddSingleton<IConsulClient>(sp =>
{
    var consulAddress = consulConfig.GetValue<string>("Address") ?? "http://localhost:8500";
    return new ConsulClient(config =>
    {
        config.Address = new Uri(consulAddress);
    });
});

// Services Consul
builder.Services.AddSingleton<IServiceDiscovery, ServiceDiscovery>();
builder.Services.AddHostedService<ConsulService>();

// Repositories
builder.Services.AddScoped<ICertificatOrigineRepository, CertificatOrigineRepository>();
builder.Services.AddScoped<ICertificateLineRepository, CertificateLineRepository>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();
builder.Services.AddScoped<ICommentaireRepository, CommentaireRepository>();
builder.Services.AddScoped<ICertificateTypeRepository, CertificateTypeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services Application
builder.Services.AddScoped<ICertificatOrigineService, CertificatOrigineService>();
builder.Services.AddScoped<ICertificateLineService, CertificateLineService>();
builder.Services.AddScoped<IAbonnementService, AbonnementService>();
builder.Services.AddScoped<ICommentaireService, CommentaireService>();
builder.Services.AddScoped<ICertificateTypeService, CertificateTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger activé en Development et Production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "COService API v1");
    c.RoutePrefix = "swagger"; // Swagger UI à /swagger
});

// Redirection de la racine vers Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Middleware de gestion d'erreur
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = new
        {
            status = StatusCodes.Status500InternalServerError,
            title = "Une erreur s'est produite",
            detail = exception?.Message ?? "Une erreur inattendue s'est produite",
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    });
});

app.UseHttpsRedirection();

// Endpoints de vérification de santé
app.MapHealthEndpoints();

// Endpoints CRUD
app.MapCertificatEndpoints();
app.MapCertificateLineEndpoints();
app.MapAbonnementEndpoints();
app.MapCommentaireEndpoints();
app.MapCertificateTypeEndpoints();

app.Run();
