using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des exportateurs
/// Lecture seule - synchronisés depuis le microservice Enrolement
/// </summary>
public static class ExportateurEndpoints
{
    public static void MapExportateurEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exportateurs")
            .WithTags("Exportateurs");

        // GET /api/exportateurs - Liste tous les exportateurs
        group.MapGet("/", async (
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateurs = await service.GetAllAsync(cancellationToken);
            return Results.Ok(exportateurs);
        })
        .WithName("GetAllExportateurs")
        .WithSummary("Récupère tous les exportateurs")
        .Produces<IEnumerable<ExportateurDto>>(StatusCodes.Status200OK);

        // GET /api/exportateurs/{id} - Récupère un exportateur par ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateur = await service.GetByIdAsync(id, cancellationToken);
            return exportateur == null
                ? Results.NotFound(new { message = $"Exportateur avec l'ID {id} introuvable." })
                : Results.Ok(exportateur);
        })
        .WithName("GetExportateurById")
        .WithSummary("Récupère un exportateur par son identifiant")
        .Produces<ExportateurDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/exportateurs/code/{code} - Récupère un exportateur par code
        group.MapGet("/code/{code}", async (
            string code,
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateur = await service.GetByCodeAsync(code, cancellationToken);
            return exportateur == null
                ? Results.NotFound(new { message = $"Exportateur avec le code {code} introuvable." })
                : Results.Ok(exportateur);
        })
        .WithName("GetExportateurByCode")
        .WithSummary("Récupère un exportateur par son code")
        .Produces<ExportateurDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/exportateurs/actifs - Liste les exportateurs actifs
        group.MapGet("/actifs", async (
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateurs = await service.GetActifsAsync(cancellationToken);
            return Results.Ok(exportateurs);
        })
        .WithName("GetExportateursActifs")
        .WithSummary("Récupère tous les exportateurs actifs")
        .Produces<IEnumerable<ExportateurDto>>(StatusCodes.Status200OK);

        // GET /api/exportateurs/partenaire/{partenaireId} - Liste les exportateurs par partenaire
        group.MapGet("/partenaire/{partenaireId:guid}", async (
            Guid partenaireId,
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateurs = await service.GetByPartenaireAsync(partenaireId, cancellationToken);
            return Results.Ok(exportateurs);
        })
        .WithName("GetExportateursByPartenaire")
        .WithSummary("Récupère les exportateurs par partenaire")
        .Produces<IEnumerable<ExportateurDto>>(StatusCodes.Status200OK);

        // GET /api/exportateurs/departement/{departementId} - Liste les exportateurs par département
        group.MapGet("/departement/{departementId:guid}", async (
            Guid departementId,
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateurs = await service.GetByDepartementAsync(departementId, cancellationToken);
            return Results.Ok(exportateurs);
        })
        .WithName("GetExportateursByDepartement")
        .WithSummary("Récupère les exportateurs par département")
        .Produces<IEnumerable<ExportateurDto>>(StatusCodes.Status200OK);

        // GET /api/exportateurs/type/{type} - Liste les exportateurs par type
        group.MapGet("/type/{type:int}", async (
            int type,
            IExportateurService service,
            CancellationToken cancellationToken) =>
        {
            var exportateurs = await service.GetByTypeAsync(type, cancellationToken);
            return Results.Ok(exportateurs);
        })
        .WithName("GetExportateursByType")
        .WithSummary("Récupère les exportateurs par type")
        .Produces<IEnumerable<ExportateurDto>>(StatusCodes.Status200OK);
    }
}
