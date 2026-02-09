using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des statuts de certificats
/// </summary>
public static class StatutCertificatEndpoints
{
    public static void MapStatutCertificatEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/statuts-certificats")
            .WithTags("Statuts de certificats");

        // GET /api/statuts-certificats - Liste tous les statuts
        group.MapGet("/", async (
            IStatutCertificatService service,
            CancellationToken cancellationToken) =>
        {
            var statuts = await service.GetAllStatutsAsync(cancellationToken);
            return Results.Ok(statuts);
        })
        .WithName("GetAllStatutsCertificats")
        .WithSummary("Récupère tous les statuts de certificats")
        .Produces<IEnumerable<StatutCertificatDto>>(StatusCodes.Status200OK);

        // GET /api/statuts-certificats/{id} - Récupère un statut par ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            IStatutCertificatService service,
            CancellationToken cancellationToken) =>
        {
            var statut = await service.GetStatutByIdAsync(id, cancellationToken);
            return statut == null
                ? Results.NotFound(new { message = $"Statut avec l'ID {id} introuvable." })
                : Results.Ok(statut);
        })
        .WithName("GetStatutCertificatById")
        .WithSummary("Récupère un statut par son identifiant")
        .Produces<StatutCertificatDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/statuts-certificats/code/{code} - Récupère un statut par code
        group.MapGet("/code/{code}", async (
            string code,
            IStatutCertificatService service,
            CancellationToken cancellationToken) =>
        {
            var statut = await service.GetStatutByCodeAsync(code, cancellationToken);
            return statut == null
                ? Results.NotFound(new { message = $"Statut avec le code {code} introuvable." })
                : Results.Ok(statut);
        })
        .WithName("GetStatutCertificatByCode")
        .WithSummary("Récupère un statut par son code")
        .Produces<StatutCertificatDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
