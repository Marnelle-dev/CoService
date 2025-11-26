# Explication des Couches - COService

Ce document explique en détail ce que chaque couche de l'architecture contient, ses responsabilités, et comment les données circulent entre les couches.

---

## Vue d'Ensemble de l'Architecture

```
┌─────────────────────────────────────────────────────────┐
│                    COService.API                         │
│  (Endpoints, Middleware, Configuration HTTP)            │
└──────────────────────┬──────────────────────────────────┘
                       │ Utilise
                       ▼
┌─────────────────────────────────────────────────────────┐
│                COService.Application                     │
│  (Services Métier, DTOs, Validators, Mappings)          │
└──────────────────────┬──────────────────────────────────┘
                       │ Utilise
                       ▼
┌─────────────────────────────────────────────────────────┐
│                  COService.Domain                       │
│  (Entités, Enums, Value Objects)                        │
└──────────────────────┬──────────────────────────────────┘
                       │ Utilisé par
                       ▼
┌─────────────────────────────────────────────────────────┐
│              COService.Infrastructure                    │
│  (DbContext, Repositories, Clients Externes)            │
└─────────────────────────────────────────────────────────┘
```

---

## 1. COService.Domain (Couche la plus interne)

### 🎯 **Responsabilité**
Contient les entités métier pures, sans dépendances externes. C'est le cœur de l'application.

### 📦 **Ce qu'elle contient :**

#### **Entities/** (Entités métier)
```csharp
// Exemple : DemandeCO.cs
public class DemandeCO
{
    public Guid Id { get; set; }
    public string NumeroDemande { get; set; }
    public Guid ExportateurId { get; set; }
    public string PaysDestination { get; set; }
    public DateTime DateCreation { get; set; }
    public StatutDemande StatutActuel { get; set; }
    public Guid? DossierId { get; set; }
    
    // Navigation properties
    public CertificatOrigine? CertificatOrigine { get; set; }
    public ICollection<StatutValidation> StatutValidations { get; set; }
}
```

**Ce qu'elle reçoit :**
- ✅ Aucune dépendance externe
- ✅ Pas de packages NuGet (sauf peut-être des annotations)
- ✅ Logique métier pure (méthodes de calcul, validations métier)

**Ce qu'elle ne contient PAS :**
- ❌ Accès à la base de données
- ❌ Appels HTTP
- ❌ Logging
- ❌ Configuration

#### **Enums/** (Énumérations)
```csharp
// Exemple : StatutDemande.cs
public enum StatutDemande
{
    Brouillon = 0,
    EnAttente = 1,
    Controle = 2,
    Approuve = 3,
    Valide = 4,
    Rejete = 5
}

public enum RoleUtilisateur
{
    Exportateur = 1,
    Controleur = 2,
    Superviseur = 3,
    Signataire = 4
}
```

#### **ValueObjects/** (Objets Valeur)
```csharp
// Exemple : InformationsMarchandise.cs
public class InformationsMarchandise
{
    public string Description { get; set; }
    public decimal Poids { get; set; }
    public string Unite { get; set; }
    public decimal Valeur { get; set; }
    public string Devise { get; set; }
}
```

### 🔄 **Flux de données :**
- **Reçoit :** Rien (couche la plus basique)
- **Envoie :** Ses entités aux autres couches (Application, Infrastructure)

---

## 2. COService.Application (Couche Logique Métier)

### 🎯 **Responsabilité**
Contient la logique métier de l'application, les services, les DTOs, et les règles de validation.

### 📦 **Ce qu'elle contient :**

#### **Services/** (Services métier)
```csharp
// Exemple : CertificatOrigineService.cs
public interface ICertificatOrigineService
{
    Task<CertificatOrigineDto> CreerDemandeAsync(CreerDemandeCODto dto);
    Task<IEnumerable<DemandeCODto>> GetDemandesParExportateurAsync(Guid exportateurId);
    Task<CertificatOrigineDto> TelechargerCertificatAsync(Guid certificatId, string pays, string role);
}

public class CertificatOrigineService : ICertificatOrigineService
{
    private readonly ICertificatOrigineRepository _repository;
    private readonly IVisaDossierServiceClient _visaDossierClient;
    private readonly IMapper _mapper;
    
    // Logique métier ici
}
```

**Ce qu'elle reçoit :**
- ✅ Les entités du Domain (via les repositories)
- ✅ Les DTOs de l'API
- ✅ Les interfaces des repositories (pas les implémentations)
- ✅ Les interfaces des clients externes (pas les implémentations)
- ✅ AutoMapper pour transformer Domain ↔ DTOs

**Ce qu'elle fait :**
- ✅ Orchestre les opérations métier
- ✅ Valide les règles métier
- ✅ Appelle les repositories pour accéder aux données
- ✅ Appelle les clients externes (visaDossier, Auth, etc.)
- ✅ Transforme les entités Domain en DTOs
- ✅ Gère les transactions métier

#### **DTOs/** (Data Transfer Objects)
```csharp
// Exemple : DemandeCODto.cs
public class DemandeCODto
{
    public Guid Id { get; set; }
    public string NumeroDemande { get; set; }
    public Guid ExportateurId { get; set; }
    public string PaysDestination { get; set; }
    public string StatutActuel { get; set; }
    public DateTime DateCreation { get; set; }
    public List<StatutValidationDto> Validations { get; set; }
}

// Exemple : CreerDemandeCODto.cs (pour la création)
public class CreerDemandeCODto
{
    [Required]
    public Guid ExportateurId { get; set; }
    
    [Required]
    public string PaysDestination { get; set; }
    
    public InformationsMarchandiseDto Marchandise { get; set; }
}
```

**Ce qu'elle reçoit :**
- ✅ Les DTOs de l'API (input)
- ✅ Les entités du Domain (via repositories)
- ✅ Les réponses des microservices externes

**Ce qu'elle envoie :**
- ✅ Les DTOs vers l'API (output)
- ✅ Les commandes vers les repositories

#### **Mappings/** (Profils AutoMapper)
```csharp
// Exemple : MappingProfile.cs
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain → DTO
        CreateMap<DemandeCO, DemandeCODto>();
        CreateMap<CertificatOrigine, CertificatOrigineDto>();
        
        // DTO → Domain
        CreateMap<CreerDemandeCODto, DemandeCO>();
    }
}
```

#### **Validators/** (Validateurs FluentValidation)
```csharp
// Exemple : DemandeCOValidator.cs
public class CreerDemandeCOValidator : AbstractValidator<CreerDemandeCODto>
{
    public CreerDemandeCOValidator()
    {
        RuleFor(x => x.ExportateurId)
            .NotEmpty()
            .WithMessage("L'ID de l'exportateur est requis");
            
        RuleFor(x => x.PaysDestination)
            .NotEmpty()
            .Length(2, 100)
            .WithMessage("Le pays de destination est requis");
    }
}
```

### 🔄 **Flux de données :**
- **Reçoit :**
  - DTOs de l'API
  - Entités du Domain (via repositories)
  - Réponses des microservices (via clients)
- **Envoie :**
  - DTOs vers l'API
  - Commandes vers les repositories
  - Requêtes vers les microservices

---

## 3. COService.Infrastructure (Couche Infrastructure)

### 🎯 **Responsabilité**
Gère l'accès aux données, les appels aux microservices externes, et toute l'infrastructure technique.

### 📦 **Ce qu'elle contient :**

#### **Data/** (Accès aux données)
```csharp
// Exemple : COServiceDbContext.cs
public class COServiceDbContext : DbContext
{
    public DbSet<DemandeCO> DemandesCO { get; set; }
    public DbSet<CertificatOrigine> CertificatsOrigine { get; set; }
    public DbSet<StatutValidation> StatutValidations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(COServiceDbContext).Assembly);
    }
}
```

**Ce qu'elle reçoit :**
- ✅ Les entités du Domain
- ✅ Les configurations EF Core
- ✅ La chaîne de connexion (via configuration)

**Ce qu'elle fait :**
- ✅ Mappe les entités Domain vers les tables SQL
- ✅ Gère les migrations
- ✅ Exécute les requêtes SQL

#### **Configurations/** (Configurations EF Core)
```csharp
// Exemple : DemandeCOConfiguration.cs
public class DemandeCOConfiguration : IEntityTypeConfiguration<DemandeCO>
{
    public void Configure(EntityTypeBuilder<DemandeCO> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NumeroDemande)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(x => x.NumeroDemande)
            .IsUnique();
        builder.HasOne(x => x.CertificatOrigine)
            .WithOne(x => x.DemandeCO)
            .HasForeignKey<CertificatOrigine>(x => x.DemandeCOId);
    }
}
```

#### **Repositories/** (Repositories)
```csharp
// Exemple : ICertificatOrigineRepository.cs (interface dans Application)
public interface ICertificatOrigineRepository : IRepository<DemandeCO>
{
    Task<DemandeCO?> GetByNumeroAsync(string numero);
    Task<IEnumerable<DemandeCO>> GetByExportateurIdAsync(Guid exportateurId);
    Task<IEnumerable<DemandeCO>> GetByPaysAndRoleAsync(string pays, string role);
}

// Exemple : CertificatOrigineRepository.cs (implémentation dans Infrastructure)
public class CertificatOrigineRepository : Repository<DemandeCO>, ICertificatOrigineRepository
{
    public CertificatOrigineRepository(COServiceDbContext context) : base(context)
    {
    }
    
    public async Task<DemandeCO?> GetByNumeroAsync(string numero)
    {
        return await _context.DemandesCO
            .Include(x => x.StatutValidations)
            .FirstOrDefaultAsync(x => x.NumeroDemande == numero);
    }
}
```

**Ce qu'elle reçoit :**
- ✅ Les entités du Domain
- ✅ Le DbContext
- ✅ Les requêtes des services Application

**Ce qu'elle fait :**
- ✅ Exécute les requêtes SQL via EF Core
- ✅ Retourne les entités Domain
- ✅ Gère les opérations CRUD

#### **ExternalServices/** (Clients pour microservices)
```csharp
// Exemple : IVisaDossierServiceClient.cs (interface dans Application)
public interface IVisaDossierServiceClient
{
    Task<DossierResponseDto> CreerDossierAsync(CreerDossierDto dto);
    Task<StatutDossierDto> GetStatutDossierAsync(Guid dossierId);
    Task NotifierValidationAsync(Guid dossierId, string statut);
}

// Exemple : VisaDossierServiceClient.cs (implémentation dans Infrastructure)
public class VisaDossierServiceClient : IVisaDossierServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VisaDossierServiceClient> _logger;
    
    public async Task<DossierResponseDto> CreerDossierAsync(CreerDossierDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/dossiers", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierResponseDto>();
    }
}
```

**Ce qu'elle reçoit :**
- ✅ Les DTOs des services Application
- ✅ Les URLs des microservices (via configuration)
- ✅ Les tokens d'authentification

**Ce qu'elle fait :**
- ✅ Fait des appels HTTP aux microservices
- ✅ Gère les retry policies (Polly)
- ✅ Gère les erreurs et timeouts
- ✅ Retourne les réponses en DTOs

#### **Services/** (Services infrastructure)
```csharp
// Exemple : NotificationService.cs
public class NotificationService : INotificationService
{
    private readonly INotificationServiceClient _client;
    
    public async Task EnvoyerNotificationAsync(Guid userId, string message)
    {
        await _client.SendNotificationAsync(new NotificationDto
        {
            UserId = userId,
            Message = message
        });
    }
}
```

### 🔄 **Flux de données :**
- **Reçoit :**
  - Entités du Domain (pour les repositories)
  - DTOs des services Application (pour les clients externes)
  - Configuration (chaînes de connexion, URLs)
- **Envoie :**
  - Entités Domain vers Application (via repositories)
  - Réponses des microservices vers Application (via clients)

---

## 4. COService.API (Couche Présentation)

### 🎯 **Responsabilité**
Point d'entrée de l'application, gère les requêtes HTTP, la validation, et retourne les réponses.

### 📦 **Ce qu'elle contient :**

#### **Program.cs** (Configuration)
```csharp
var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddDbContext<COServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICertificatOrigineService, CertificatOrigineService>();
builder.Services.AddScoped<ICertificatOrigineRepository, CertificatOrigineRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Endpoints
app.MapCertificatOrigineEndpoints();

app.Run();
```

#### **Endpoints/** (Endpoints API)
```csharp
// Exemple : CertificatOrigineEndpoints.cs
public static class CertificatOrigineEndpoints
{
    public static void MapCertificatOrigineEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/certificats-origine")
            .WithTags("Certificats d'Origine");
        
        // Créer une demande
        group.MapPost("/demandes", async (
            CreerDemandeCODto dto,
            ICertificatOrigineService service,
            IValidator<CreerDemandeCODto> validator) =>
        {
            // Validation
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            
            // Appel au service
            var result = await service.CreerDemandeAsync(dto);
            return Results.Created($"/api/certificats-origine/{result.Id}", result);
        })
        .WithName("CreerDemande")
        .Produces<CertificatOrigineDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
        
        // Liste des demandes
        group.MapGet("/demandes", async (
            Guid exportateurId,
            ICertificatOrigineService service) =>
        {
            var result = await service.GetDemandesParExportateurAsync(exportateurId);
            return Results.Ok(result);
        })
        .WithName("GetDemandes")
        .Produces<IEnumerable<DemandeCODto>>();
    }
}
```

**Ce qu'elle reçoit :**
- ✅ Les requêtes HTTP (GET, POST, PUT, DELETE)
- ✅ Les paramètres de route
- ✅ Les body JSON (DTOs)
- ✅ Les headers (authentification, etc.)

**Ce qu'elle fait :**
- ✅ Valide les DTOs (via FluentValidation)
- ✅ Appelle les services Application
- ✅ Gère les erreurs (via middleware)
- ✅ Retourne les réponses HTTP (JSON, fichiers, etc.)
- ✅ Gère l'authentification/autorisation

#### **Middleware/** (Middleware personnalisés)
```csharp
// Exemple : ExceptionHandlingMiddleware.cs
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Une erreur est survenue");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsJsonAsync(new
        {
            error = "Une erreur interne est survenue",
            message = exception.Message
        });
    }
}
```

### 🔄 **Flux de données :**
- **Reçoit :**
  - Requêtes HTTP (JSON, paramètres, headers)
  - DTOs des clients
- **Envoie :**
  - DTOs vers les services Application
  - Réponses HTTP (JSON, fichiers) vers les clients

---

## 5. COService.Shared (Couche Partagée)

### 🎯 **Responsabilité**
Contient les éléments partagés entre les couches (constantes, exceptions, utilitaires).

### 📦 **Ce qu'elle contient :**

#### **Constants/** (Constantes)
```csharp
// Exemple : StatutsConstants.cs
public static class StatutsConstants
{
    public const string STATUT_CONTROLE = "Contrôlé";
    public const string STATUT_APPROUVE = "Approuvé";
    public const string STATUT_VALIDE = "Validé";
}
```

#### **Exceptions/** (Exceptions personnalisées)
```csharp
// Exemple : COServiceException.cs
public class COServiceException : Exception
{
    public COServiceException(string message) : base(message)
    {
    }
    
    public COServiceException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
```

### 🔄 **Flux de données :**
- **Reçoit :** Rien (éléments statiques)
- **Envoie :** Utilisé par toutes les couches

---

## Flux Complet d'une Requête

### Exemple : Créer une demande de CO

```
1. Client HTTP
   ↓ POST /api/certificats-origine/demandes
   ↓ Body: { "exportateurId": "...", "paysDestination": "Cameroun" }

2. COService.API (Endpoints)
   ↓ Reçoit: CreerDemandeCODto (JSON)
   ↓ Valide avec FluentValidation
   ↓ Appelle: ICertificatOrigineService.CreerDemandeAsync(dto)

3. COService.Application (Services)
   ↓ Reçoit: CreerDemandeCODto
   ↓ Transforme en DemandeCO (entité Domain) via AutoMapper
   ↓ Appelle: IVisaDossierServiceClient.CreerDossierAsync()
   ↓ Appelle: ICertificatOrigineRepository.AddAsync(demandeCO)
   ↓ Retourne: CertificatOrigineDto

4. COService.Infrastructure (Repositories)
   ↓ Reçoit: DemandeCO (entité Domain)
   ↓ Sauvegarde via DbContext dans SQL Server
   ↓ Retourne: DemandeCO (entité Domain)

5. COService.Infrastructure (Clients Externes)
   ↓ Reçoit: CreerDossierDto
   ↓ Appelle: HTTP POST vers visaDossier Service
   ↓ Retourne: DossierResponseDto

6. COService.Application (Services)
   ↓ Reçoit: DemandeCO (entité) + DossierResponseDto
   ↓ Transforme en CertificatOrigineDto via AutoMapper
   ↓ Retourne: CertificatOrigineDto

7. COService.API (Endpoints)
   ↓ Reçoit: CertificatOrigineDto
   ↓ Retourne: HTTP 201 Created avec JSON
   ↓ Body: { "id": "...", "numeroDemande": "..." }

8. Client HTTP
   ↓ Reçoit: Réponse JSON
```

---

## Règles de Dépendances

### ✅ **Ce qui est autorisé :**

- **API** → **Application** → **Domain**
- **API** → **Infrastructure** (pour DI)
- **Application** → **Domain**
- **Application** → **Infrastructure** (interfaces seulement)
- **Infrastructure** → **Domain**
- **Toutes les couches** → **Shared**

### ❌ **Ce qui est INTERDIT :**

- **Domain** → **Aucune autre couche** (sauf Shared)
- **Application** → **API** (pas de dépendance vers la présentation)
- **Infrastructure** → **Application** (sauf interfaces)
- **Infrastructure** → **API** (pas de dépendance vers la présentation)

---

## Résumé par Couche

| Couche | Reçoit | Contient | Envoie |
|--------|--------|----------|--------|
| **Domain** | Rien | Entités, Enums, Value Objects | Entités vers autres couches |
| **Application** | DTOs (API), Entités (Domain), Réponses (Microservices) | Services, DTOs, Validators, Mappings | DTOs (API), Commandes (Repositories) |
| **Infrastructure** | Entités (Domain), DTOs (Application), Configuration | DbContext, Repositories, Clients Externes | Entités (Application), Réponses (Application) |
| **API** | Requêtes HTTP, DTOs (Clients) | Endpoints, Middleware, Configuration | DTOs (Application), Réponses HTTP (Clients) |
| **Shared** | Rien | Constantes, Exceptions | Utilisé par toutes les couches |

