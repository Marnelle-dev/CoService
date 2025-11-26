using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des commentaires
/// </summary>
public static class CommentaireEndpoints
{
    public static void MapCommentaireEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/certificats/{certificateId:guid}/commentaires")
            .WithTags("Commentaires");

        // GET /api/certificats/{certificateId}/commentaires - Liste tous les commentaires d'un certificat
        group.MapGet("/", async (
            Guid certificateId,
            ICommentaireService service,
            CancellationToken cancellationToken) =>
        {
            var commentaires = await service.GetCommentairesByCertificateIdAsync(certificateId, cancellationToken);
            return Results.Ok(commentaires);
        })
        .WithName("GetCommentairesByCertificateId")
        .WithSummary("Récupère tous les commentaires d'un certificat")
        .Produces<IEnumerable<CommentaireDto>>(StatusCodes.Status200OK);

        // GET /api/certificats/{certificateId}/commentaires/{id} - Récupère un commentaire par ID
        group.MapGet("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            ICommentaireService service,
            CancellationToken cancellationToken) =>
        {
            var commentaire = await service.GetCommentaireByIdAsync(id, cancellationToken);
            return commentaire == null
                ? Results.NotFound(new { message = $"Commentaire avec l'ID {id} introuvable." })
                : Results.Ok(commentaire);
        })
        .WithName("GetCommentaireById")
        .WithSummary("Récupère un commentaire par son identifiant")
        .Produces<CommentaireDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST /api/certificats/{certificateId}/commentaires - Crée un nouveau commentaire
        group.MapPost("/", async (
            Guid certificateId,
            [FromBody] CreerCommentaireDto dto,
            ICommentaireService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var commentaire = await service.CreerCommentaireAsync(certificateId, dto, utilisateur, cancellationToken);
                return Results.Created($"/api/certificats/{certificateId}/commentaires/{commentaire.Id}", commentaire);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("CreerCommentaire")
        .WithSummary("Crée un nouveau commentaire")
        .Accepts<CreerCommentaireDto>("application/json")
        .Produces<CommentaireDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound);

        // PUT /api/certificats/{certificateId}/commentaires/{id} - Modifie un commentaire
        group.MapPut("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            [FromBody] ModifierCommentaireDto dto,
            ICommentaireService service,
            [FromHeader(Name = "X-User-Id")] string? utilisateur,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var commentaire = await service.ModifierCommentaireAsync(id, dto, utilisateur, cancellationToken);
                return Results.Ok(commentaire);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("ModifierCommentaire")
        .WithSummary("Modifie un commentaire")
        .Accepts<ModifierCommentaireDto>("application/json")
        .Produces<CommentaireDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE /api/certificats/{certificateId}/commentaires/{id} - Supprime un commentaire
        group.MapDelete("/{id:guid}", async (
            Guid certificateId,
            Guid id,
            ICommentaireService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                await service.SupprimerCommentaireAsync(id, cancellationToken);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("SupprimerCommentaire")
        .WithSummary("Supprime un commentaire")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}

