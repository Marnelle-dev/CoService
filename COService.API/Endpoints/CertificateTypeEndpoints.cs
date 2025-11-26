using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des types de certificats
/// </summary>
public static class CertificateTypeEndpoints
{
    public static void MapCertificateTypeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/types-certificats")
            .WithTags("Types de certificats");

        // GET /api/types-certificats - Liste tous les types de certificats
        group.MapGet("/", async (
            ICertificateTypeService service,
            CancellationToken cancellationToken) =>
        {
            var types = await service.GetAllCertificateTypesAsync(cancellationToken);
            return Results.Ok(types);
        })
        .WithName("GetAllCertificateTypes")
        .WithSummary("Récupère tous les types de certificats")
        .Produces<IEnumerable<CertificateTypeDto>>(StatusCodes.Status200OK);

        // GET /api/types-certificats/{id} - Récupère un type par ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            ICertificateTypeService service,
            CancellationToken cancellationToken) =>
        {
            var type = await service.GetCertificateTypeByIdAsync(id, cancellationToken);
            return type == null
                ? Results.NotFound(new { message = $"Type de certificat avec l'ID {id} introuvable." })
                : Results.Ok(type);
        })
        .WithName("GetCertificateTypeById")
        .WithSummary("Récupère un type de certificat par son identifiant")
        .Produces<CertificateTypeDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/types-certificats/code/{code} - Récupère un type par code
        group.MapGet("/code/{code}", async (
            string code,
            ICertificateTypeService service,
            CancellationToken cancellationToken) =>
        {
            var type = await service.GetCertificateTypeByCodeAsync(code, cancellationToken);
            return type == null
                ? Results.NotFound(new { message = $"Type de certificat avec le code '{code}' introuvable." })
                : Results.Ok(type);
        })
        .WithName("GetCertificateTypeByCode")
        .WithSummary("Récupère un type de certificat par son code")
        .Produces<CertificateTypeDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST /api/types-certificats - Crée un nouveau type de certificat
        group.MapPost("/", async (
            [FromBody] CreerCertificateTypeDto dto,
            ICertificateTypeService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var type = await service.CreerCertificateTypeAsync(dto, utilisateur, cancellationToken);
                return Results.Created($"/api/types-certificats/{type.Id}", type);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("CreerCertificateType")
        .WithSummary("Crée un nouveau type de certificat")
        .Accepts<CreerCertificateTypeDto>("application/json")
        .Produces<CertificateTypeDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        // PUT /api/types-certificats/{id} - Modifie un type de certificat
        group.MapPut("/{id:guid}", async (
            Guid id,
            [FromBody] ModifierCertificateTypeDto dto,
            ICertificateTypeService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var type = await service.ModifierCertificateTypeAsync(id, dto, utilisateur, cancellationToken);
                return Results.Ok(type);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("ModifierCertificateType")
        .WithSummary("Modifie un type de certificat")
        .Accepts<ModifierCertificateTypeDto>("application/json")
        .Produces<CertificateTypeDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // DELETE /api/types-certificats/{id} - Supprime un type de certificat
        group.MapDelete("/{id:guid}", async (
            Guid id,
            ICertificateTypeService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await service.SupprimerCertificateTypeAsync(id, cancellationToken);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("SupprimerCertificateType")
        .WithSummary("Supprime un type de certificat")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}

