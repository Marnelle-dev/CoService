using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des Formules A (spécifique à Ouesso)
/// </summary>
public static class FormuleAEndpoints
{
    public static void MapFormuleAEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/formule-a")
            .WithTags("Formule A");

        // POST /api/formule-a/{id}/creer - Créer une Formule A depuis un CO validé
        group.MapPost("/{id:guid}/creer", async (
            Guid id,
            [FromBody] CreerFormuleARequest request,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.CreerFormuleAAsync(id, request.UserId, request.Password, cancellationToken);
                return Results.Ok(certificat);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("CreerFormuleA")
        .WithSummary("Crée une Formule A à partir d'un certificat d'origine validé")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/formule-a/{id}/controle - Contrôler une Formule A
        group.MapPost("/{id:guid}/controle", async (
            Guid id,
            [FromBody] ControleFormuleARequest request,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ControleFormuleAAsync(id, request.UserId, request.Password, cancellationToken);
                return Results.Ok(certificat);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("ControleFormuleA")
        .WithSummary("Contrôle une Formule A (12 → 13)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/formule-a/{id}/approuver - Approuver une Formule A
        group.MapPost("/{id:guid}/approuver", async (
            Guid id,
            [FromBody] ApprouverFormuleARequest request,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ApprouverFormuleAAsync(id, request.UserId, request.Password, cancellationToken);
                return Results.Ok(certificat);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("ApprouverFormuleA")
        .WithSummary("Approuve une Formule A (13 → 14)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/formule-a/{id}/valider - Valider définitivement une Formule A
        group.MapPost("/{id:guid}/valider", async (
            Guid id,
            [FromBody] ValiderFormuleARequest request,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ValiderFormuleAAsync(id, request.UserId, request.Password, cancellationToken);
                return Results.Ok(certificat);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("ValiderFormuleA")
        .WithSummary("Valide définitivement une Formule A (14 → 15)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/formule-a/{id}/rejeter - Rejeter une Formule A
        group.MapPost("/{id:guid}/rejeter", async (
            Guid id,
            [FromBody] RejeterFormuleARequest request,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.RejeterFormuleAAsync(id, request.UserId, request.Password, request.Commentaire, cancellationToken);
                return Results.Ok(certificat);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
        .WithName("RejeterFormuleA")
        .WithSummary("Rejette une Formule A (vers Rejeté)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/formule-a/{id}/peut-creer - Vérifier si on peut créer une Formule A
        group.MapGet("/{id:guid}/peut-creer", async (
            Guid id,
            [FromQuery] string userId,
            IFormuleAService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var peutCreer = await service.PeutCreerFormuleAAsync(id, userId, cancellationToken);
                return Results.Ok(new { peutCreer });
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("PeutCreerFormuleA")
        .WithSummary("Vérifie si on peut créer une Formule A depuis un certificat")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    // DTOs pour les requêtes
    public record CreerFormuleARequest(string UserId, string Password);
    public record ControleFormuleARequest(string UserId, string Password);
    public record ApprouverFormuleARequest(string UserId, string Password);
    public record ValiderFormuleARequest(string UserId, string Password);
    public record RejeterFormuleARequest(string UserId, string Password, string Commentaire);
}
