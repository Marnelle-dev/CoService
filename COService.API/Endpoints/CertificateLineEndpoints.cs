using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des lignes de certificat
/// </summary>
public static class CertificateLineEndpoints
{
    public static void MapCertificateLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/certificats/{certificateId:guid}/lignes")
            .WithTags("Lignes de certificat");

        // GET /api/certificats/{certificateId}/lignes - Liste toutes les lignes d'un certificat
        group.MapGet("/", async (
            Guid certificateId,
            ICertificateLineService service,
            CancellationToken cancellationToken) =>
        {
            var lignes = await service.GetLignesByCertificateIdAsync(certificateId, cancellationToken);
            return Results.Ok(lignes);
        })
        .WithName("GetLignesByCertificateId")
        .WithSummary("Récupère toutes les lignes d'un certificat")
        .Produces<IEnumerable<CertificateLineDto>>(StatusCodes.Status200OK);

        // GET /api/certificats/{certificateId}/lignes/{id} - Récupère une ligne par ID
        group.MapGet("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            ICertificateLineService service,
            CancellationToken cancellationToken) =>
        {
            var ligne = await service.GetLigneByIdAsync(id, cancellationToken);
            return ligne == null
                ? Results.NotFound(new { message = $"Ligne avec l'ID {id} introuvable." })
                : Results.Ok(ligne);
        })
        .WithName("GetLigneById")
        .WithSummary("Récupère une ligne par son identifiant")
        .Produces<CertificateLineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST /api/certificats/{certificateId}/lignes - Crée une nouvelle ligne
        group.MapPost("/", async (
            Guid certificateId,
            [FromBody] CreerCertificateLineDto dto,
            ICertificateLineService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var ligne = await service.CreerLigneAsync(certificateId, dto, utilisateur, cancellationToken);
                return Results.Created($"/api/certificats/{certificateId}/lignes/{ligne.Id}", ligne);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("CreerLigne")
        .WithSummary("Crée une nouvelle ligne de certificat")
        .Accepts<CreerCertificateLineDto>("application/json")
        .Produces<CertificateLineDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound);

        // PUT /api/certificats/{certificateId}/lignes/{id} - Modifie une ligne
        group.MapPut("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            [FromBody] ModifierCertificateLineDto dto,
            ICertificateLineService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var ligne = await service.ModifierLigneAsync(id, dto, utilisateur, cancellationToken);
                return Results.Ok(ligne);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("ModifierLigne")
        .WithSummary("Modifie une ligne de certificat")
        .Accepts<ModifierCertificateLineDto>("application/json")
        .Produces<CertificateLineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE /api/certificats/{certificateId}/lignes/{id} - Supprime une ligne
        group.MapDelete("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            ICertificateLineService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await service.SupprimerLigneAsync(id, cancellationToken);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("SupprimerLigne")
        .WithSummary("Supprime une ligne de certificat")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}

