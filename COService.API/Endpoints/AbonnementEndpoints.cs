using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des abonnements
/// </summary>
public static class AbonnementEndpoints
{
    public static void MapAbonnementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/abonnements")
            .WithTags("Abonnements");

        // GET /api/abonnements - Liste tous les abonnements
        group.MapGet("/", async (
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            var abonnements = await service.GetAllAbonnementsAsync(cancellationToken);
            return Results.Ok(abonnements);
        })
        .WithName("GetAllAbonnements")
        .WithSummary("Récupère tous les abonnements")
        .Produces<IEnumerable<AbonnementDto>>(StatusCodes.Status200OK);

        // GET /api/abonnements/{id} - Récupère un abonnement par ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            var abonnement = await service.GetAbonnementByIdAsync(id, cancellationToken);
            return abonnement == null
                ? Results.NotFound(new { message = $"Abonnement avec l'ID {id} introuvable." })
                : Results.Ok(abonnement);
        })
        .WithName("GetAbonnementById")
        .WithSummary("Récupère un abonnement par son identifiant")
        .Produces<AbonnementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/abonnements/exportateur/{exportateur} - Récupère les abonnements par exportateur
        group.MapGet("/exportateur/{exportateur}", async (
            string exportateur,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            var abonnements = await service.GetAbonnementsByExportateurAsync(exportateur, cancellationToken);
            return Results.Ok(abonnements);
        })
        .WithName("GetAbonnementsByExportateur")
        .WithSummary("Récupère les abonnements par exportateur")
        .Produces<IEnumerable<AbonnementDto>>(StatusCodes.Status200OK);

        // GET /api/abonnements/partenaire/{partenaire} - Récupère les abonnements par partenaire
        group.MapGet("/partenaire/{partenaire}", async (
            string partenaire,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            var abonnements = await service.GetAbonnementsByPartenaireAsync(partenaire, cancellationToken);
            return Results.Ok(abonnements);
        })
        .WithName("GetAbonnementsByPartenaire")
        .WithSummary("Récupère les abonnements par partenaire (chambre de commerce)")
        .Produces<IEnumerable<AbonnementDto>>(StatusCodes.Status200OK);

        // POST /api/abonnements - Crée un nouvel abonnement
        group.MapPost("/", async (
            [FromBody] CreerAbonnementDto dto,
            IAbonnementService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var abonnement = await service.CreerAbonnementAsync(dto, utilisateur, cancellationToken);
                return Results.Created($"/api/abonnements/{abonnement.Id}", abonnement);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("CreerAbonnement")
        .WithSummary("Crée un nouvel abonnement")
        .Accepts<CreerAbonnementDto>("application/json")
        .Produces<AbonnementDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound);

        // PUT /api/abonnements/{id} - Modifie un abonnement
        group.MapPut("/{id:guid}", async (
            Guid id,
            [FromBody] ModifierAbonnementDto dto,
            IAbonnementService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var abonnement = await service.ModifierAbonnementAsync(id, dto, utilisateur, cancellationToken);
                return Results.Ok(abonnement);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("ModifierAbonnement")
        .WithSummary("Modifie un abonnement")
        .Accepts<ModifierAbonnementDto>("application/json")
        .Produces<AbonnementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE /api/abonnements/{id} - Supprime un abonnement
        group.MapDelete("/{id:guid}", async (
            Guid id,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await service.SupprimerAbonnementAsync(id, cancellationToken);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("SupprimerAbonnement")
        .WithSummary("Supprime un abonnement")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        // POST /api/abonnements/{id}/certificats - Rattache des certificats à un abonnement
        group.MapPost("/{id:guid}/certificats", async (
            Guid id,
            [FromBody] List<Guid> certificateIds,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var abonnement = await service.RattacherCertificatsAsync(id, certificateIds, cancellationToken);
                return Results.Ok(abonnement);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("RattacherCertificats")
        .WithSummary("Rattache des certificats à un abonnement")
        .Accepts<List<Guid>>("application/json")
        .Produces<AbonnementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE /api/abonnements/{id}/certificats/{certificateId} - Détache un certificat d'un abonnement
        group.MapDelete("/{id:guid}/certificats/{certificateId:guid}", async (
            Guid id,
            Guid certificateId,
            IAbonnementService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var abonnement = await service.DetacherCertificatAsync(id, certificateId, cancellationToken);
                return Results.Ok(abonnement);
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
        .WithName("DetacherCertificat")
        .WithSummary("Détache un certificat d'un abonnement")
        .Produces<AbonnementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
