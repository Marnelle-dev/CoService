using COService.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la synchronisation avec le microservice Enrolement
/// </summary>
public static class EnrolementSyncEndpoints
{
    public static void MapEnrolementSyncEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sync/enrolement")
            .WithTags("Synchronisation Enrolement");

        // POST /api/sync/enrolement/partenaires - Synchroniser tous les partenaires
        group.MapPost("/partenaires", async (
            IEnrolementSyncService syncService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await syncService.SynchroniserPartenairesAsync(cancellationToken);
                return Results.Ok(new { message = "Synchronisation des partenaires réussie" });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SynchroniserPartenaires")
        .WithSummary("Déclenche la synchronisation de tous les partenaires depuis Enrolement")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /api/sync/enrolement/exportateurs - Synchroniser tous les exportateurs
        group.MapPost("/exportateurs", async (
            IEnrolementSyncService syncService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await syncService.SynchroniserExportateursAsync(cancellationToken);
                return Results.Ok(new { message = "Synchronisation des exportateurs réussie" });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SynchroniserExportateurs")
        .WithSummary("Déclenche la synchronisation de tous les exportateurs depuis Enrolement")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /api/sync/enrolement/tout - Synchroniser tout
        group.MapPost("/tout", async (
            IEnrolementSyncService syncService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await syncService.SynchroniserPartenairesAsync(cancellationToken);
                await syncService.SynchroniserExportateursAsync(cancellationToken);
                return Results.Ok(new { message = "Synchronisation complète réussie" });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SynchroniserTout")
        .WithSummary("Déclenche la synchronisation complète (partenaires + exportateurs) depuis Enrolement")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /api/sync/enrolement/partenaire/{id} - Synchroniser un partenaire spécifique
        group.MapPost("/partenaire/{id:guid}", async (
            Guid id,
            IEnrolementSyncService syncService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await syncService.SynchroniserPartenaireAsync(id, cancellationToken);
                return Results.Ok(new { message = $"Synchronisation du partenaire {id} réussie" });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SynchroniserPartenaire")
        .WithSummary("Déclenche la synchronisation d'un partenaire spécifique depuis Enrolement")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /api/sync/enrolement/exportateur/{id} - Synchroniser un exportateur spécifique
        group.MapPost("/exportateur/{id:guid}", async (
            Guid id,
            IEnrolementSyncService syncService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await syncService.SynchroniserExportateurAsync(id, cancellationToken);
                return Results.Ok(new { message = $"Synchronisation de l'exportateur {id} réussie" });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SynchroniserExportateur")
        .WithSummary("Déclenche la synchronisation d'un exportateur spécifique depuis Enrolement")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
