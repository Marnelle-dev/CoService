using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des partenaires (Chambres de Commerce)
/// Lecture seule - synchronisés depuis le microservice Enrolement
/// </summary>
public static class PartenaireEndpoints
{
    public static void MapPartenaireEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/partenaires")
            .WithTags("Partenaires");

        // GET /api/partenaires - Liste tous les partenaires
        group.MapGet("/", async (
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaires = await service.GetAllAsync(cancellationToken);
            return Results.Ok(partenaires);
        })
        .WithName("GetAllPartenaires")
        .WithSummary("Récupère tous les partenaires")
        .Produces<IEnumerable<PartenaireDto>>(StatusCodes.Status200OK);

        // GET /api/partenaires/{id} - Récupère un partenaire par ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaire = await service.GetByIdAsync(id, cancellationToken);
            return partenaire == null
                ? Results.NotFound(new { message = $"Partenaire avec l'ID {id} introuvable." })
                : Results.Ok(partenaire);
        })
        .WithName("GetPartenaireById")
        .WithSummary("Récupère un partenaire par son identifiant")
        .Produces<PartenaireDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/partenaires/code/{code} - Récupère un partenaire par code
        group.MapGet("/code/{code}", async (
            string code,
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaire = await service.GetByCodeAsync(code, cancellationToken);
            return partenaire == null
                ? Results.NotFound(new { message = $"Partenaire avec le code {code} introuvable." })
                : Results.Ok(partenaire);
        })
        .WithName("GetPartenaireByCode")
        .WithSummary("Récupère un partenaire par son code")
        .Produces<PartenaireDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/partenaires/actifs - Liste les partenaires actifs
        group.MapGet("/actifs", async (
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaires = await service.GetActifsAsync(cancellationToken);
            return Results.Ok(partenaires);
        })
        .WithName("GetPartenairesActifs")
        .WithSummary("Récupère tous les partenaires actifs")
        .Produces<IEnumerable<PartenaireDto>>(StatusCodes.Status200OK);

        // GET /api/partenaires/type/{typeId} - Liste les partenaires par type
        group.MapGet("/type/{typeId:guid}", async (
            Guid typeId,
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaires = await service.GetByTypeAsync(typeId, cancellationToken);
            return Results.Ok(partenaires);
        })
        .WithName("GetPartenairesByType")
        .WithSummary("Récupère les partenaires par type")
        .Produces<IEnumerable<PartenaireDto>>(StatusCodes.Status200OK);

        // GET /api/partenaires/departement/{departementId} - Liste les partenaires par département
        group.MapGet("/departement/{departementId:guid}", async (
            Guid departementId,
            IPartenaireService service,
            CancellationToken cancellationToken) =>
        {
            var partenaires = await service.GetByDepartementAsync(departementId, cancellationToken);
            return Results.Ok(partenaires);
        })
        .WithName("GetPartenairesByDepartement")
        .WithSummary("Récupère les partenaires par département")
        .Produces<IEnumerable<PartenaireDto>>(StatusCodes.Status200OK);
    }
}
