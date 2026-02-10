using Consul;
using COService.API.Endpoints;
using COService.Application.Mappings;
using COService.Application.Messaging;
using COService.Application.Repositories;
using COService.Application.Services;
using COService.Infrastructure.Data;
using COService.Infrastructure.ExternalServices;
using COService.Infrastructure.Messaging;
using COService.Infrastructure.Messaging.Handlers;
using COService.Infrastructure.Repositories;
using COService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration Entity Framework Core
builder.Services.AddDbContext<COServiceDbContext>(options =>
{
    // Utiliser la chaîne de connexion \"chaine\" (authentification Windows / compte AD)
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

// Configuration des services externes
// Client Enrolement avec wrapper pour découverte de service dynamique
builder.Services.AddSingleton<IEnrolementServiceClient, EnrolementServiceClientWrapper>();

// Client Auth Service avec wrapper
builder.Services.AddSingleton<COService.Infrastructure.ExternalServices.IAuthServiceClient, COService.Infrastructure.ExternalServices.AuthServiceClientWrapper>();
builder.Services.AddScoped<COService.Application.Services.IAuthService, COService.Infrastructure.Services.AuthService>();

// Configuration des options de synchronisation Enrolement
builder.Services.Configure<EnrolementSyncOptions>(options =>
{
    options.Enabled = builder.Configuration.GetValue<bool>("EnrolementSync:Enabled", true);
    options.IntervalMinutes = builder.Configuration.GetValue<int>("EnrolementSync:IntervalMinutes", 60);
});

// Configuration RabbitMQ
builder.Services.Configure<RabbitMQOptions>(
    builder.Configuration.GetSection(RabbitMQOptions.SectionName));

// RabbitMQ Client (singleton pour maintenir la connexion)
builder.Services.AddSingleton<IRabbitMQClient, RabbitMQClient>();

// Event Publishers
builder.Services.AddScoped<ICertificateEventPublisher, CertificateEventPublisher>();
builder.Services.AddScoped<INotificationEventPublisher, NotificationEventPublisher>();

// RabbitMQ Event Handlers
builder.Services.AddScoped<PartenaireEventHandler>();
builder.Services.AddScoped<ExportateurEventHandler>();
builder.Services.AddScoped<ReferentielEventHandler>();

// RabbitMQ Consumer Service (Background Service)
builder.Services.AddHostedService<RabbitMQConsumerService>();

// Service de synchronisation Enrolement
builder.Services.AddScoped<IEnrolementSyncService, EnrolementSyncService>();
builder.Services.AddHostedService<EnrolementSyncService>();

// Repositories
builder.Services.AddScoped<ICertificatOrigineRepository, CertificatOrigineRepository>();
builder.Services.AddScoped<ICertificateLineRepository, CertificateLineRepository>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();
builder.Services.AddScoped<ICommentaireRepository, CommentaireRepository>();
builder.Services.AddScoped<ICertificateTypeRepository, CertificateTypeRepository>();
builder.Services.AddScoped<IPartenaireRepository, PartenaireRepository>();
builder.Services.AddScoped<IExportateurRepository, ExportateurRepository>();
builder.Services.AddScoped<ITypePartenaireRepository, TypePartenaireRepository>();
builder.Services.AddScoped<IStatutCertificatRepository, StatutCertificatRepository>();
// Repositories référentiels (synchronisés depuis Referentiel Service)
builder.Services.AddScoped<IPaysRepository, PaysRepository>();
builder.Services.AddScoped<IPortRepository, PortRepository>();
builder.Services.AddScoped<IAeroportRepository, AeroportRepository>();
builder.Services.AddScoped<IDeviseRepository, DeviseRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IIncotermRepository, IncotermRepository>();
builder.Services.AddScoped<IBureauDedouanementRepository, BureauDedouanementRepository>();
builder.Services.AddScoped<IUniteStatistiqueRepository, UniteStatistiqueRepository>();
builder.Services.AddScoped<IDepartementRepository, DepartementRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services Application
builder.Services.AddScoped<ICertificatOrigineService, CertificatOrigineService>();
builder.Services.AddScoped<ICertificateLineService, CertificateLineService>();
builder.Services.AddScoped<IAbonnementService, AbonnementService>();
builder.Services.AddScoped<ICommentaireService, CommentaireService>();
builder.Services.AddScoped<ICertificateTypeService, CertificateTypeService>();
builder.Services.AddScoped<IPartenaireService, PartenaireService>();
builder.Services.AddScoped<IExportateurService, ExportateurService>();
builder.Services.AddScoped<ITypePartenaireService, TypePartenaireService>();
builder.Services.AddScoped<IStatutCertificatService, StatutCertificatService>();
builder.Services.AddScoped<INumeroGenerationService, NumeroGenerationService>();
builder.Services.AddScoped<IFormuleAService, FormuleAService>();
builder.Services.AddScoped<IPDFGenerationService, PDFGenerationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

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
app.MapPartenaireEndpoints();
app.MapExportateurEndpoints();
app.MapTypePartenaireEndpoints();
app.MapStatutCertificatEndpoints();
app.MapWorkflowEndpoints();
app.MapFormuleAEndpoints();
app.MapPDFEndpoints();
app.MapEnrolementSyncEndpoints();

app.Run();

// Fonctions helper pour les policies Polly
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry: (outcome, timespan, retryCount, context) =>
            {
                // Log retry si nécessaire
            });
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 5,
            durationOfBreak: TimeSpan.FromSeconds(30));
}
