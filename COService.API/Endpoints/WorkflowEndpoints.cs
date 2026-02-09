using COService.Application.DTOs;
using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la gestion des workflows de validation des certificats
/// </summary>
public static class WorkflowEndpoints
{
    public static void MapWorkflowEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workflow")
            .WithTags("Workflow de validation");

        // POST /api/workflow/{id}/soumettre - Soumettre un certificat
        group.MapPost("/{id:guid}/soumettre", async (
            Guid id,
            [FromBody] SoumettreCertificatRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.SoumettreCertificatAsync(id, request.UserId, cancellationToken);
                return Results.Ok(certificat);
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
        .WithName("SoumettreCertificat")
        .WithSummary("Soumet un certificat pour validation (Élaboré → Soumis)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/workflow/{id}/controle - Contrôler un certificat
        group.MapPost("/{id:guid}/controle", async (
            Guid id,
            [FromBody] ControleCertificatRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ControleCertificatAsync(id, request.UserId, request.Password, cancellationToken);
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
        .WithName("ControleCertificat")
        .WithSummary("Contrôle un certificat (Soumis → Contrôlé)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/workflow/{id}/approuver - Approuver un certificat
        group.MapPost("/{id:guid}/approuver", async (
            Guid id,
            [FromBody] ApprouverCertificatRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ApprouverCertificatAsync(id, request.UserId, request.Password, cancellationToken);
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
        .WithName("ApprouverCertificat")
        .WithSummary("Approuve un certificat (Contrôlé → Approuvé)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/workflow/{id}/valider - Valider définitivement un certificat
        group.MapPost("/{id:guid}/valider", async (
            Guid id,
            [FromBody] ValiderCertificatRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.ValiderCertificatAsync(id, request.UserId, request.Password, cancellationToken);
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
        .WithName("ValiderCertificat")
        .WithSummary("Valide définitivement un certificat (Approuvé → Validé)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/workflow/{id}/rejeter - Rejeter un certificat
        group.MapPost("/{id:guid}/rejeter", async (
            Guid id,
            [FromBody] RejeterCertificatRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.RejeterCertificatAsync(id, request.UserId, request.Password, request.Commentaire, cancellationToken);
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
        .WithName("RejeterCertificat")
        .WithSummary("Rejette un certificat (vers Rejeté)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/workflow/{id}/demander-modification - Demander une modification
        group.MapPost("/{id:guid}/demander-modification", async (
            Guid id,
            [FromBody] DemanderModificationRequest request,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var certificat = await service.DemanderModificationAsync(id, request.UserId, request.Commentaire, cancellationToken);
                return Results.Ok(certificat);
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
        .WithName("DemanderModification")
        .WithSummary("Demande une modification sur un certificat validé (Validé → Modification)")
        .Produces<CertificatOrigineDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/workflow/{id}/transitions-possibles - Récupère les transitions possibles
        group.MapGet("/{id:guid}/transitions-possibles", async (
            Guid id,
            [FromQuery] string userId,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var transitions = await service.GetTransitionsPossiblesAsync(id, userId, cancellationToken);
                return Results.Ok(new { transitions });
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("GetTransitionsPossibles")
        .WithSummary("Récupère les transitions possibles pour un certificat selon l'utilisateur")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // GET /api/workflow/{id}/transition-valide - Vérifie si une transition est valide
        group.MapGet("/{id:guid}/transition-valide", async (
            Guid id,
            [FromQuery] string codeNouveauStatut,
            [FromQuery] string userId,
            IWorkflowService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var estValide = await service.EstTransitionValideAsync(id, codeNouveauStatut, userId, cancellationToken);
                return Results.Ok(new { estValide });
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("EstTransitionValide")
        .WithSummary("Vérifie si une transition de statut est valide pour un certificat")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    // DTOs pour les requêtes
    public record SoumettreCertificatRequest(string UserId);
    public record ControleCertificatRequest(string UserId, string Password);
    public record ApprouverCertificatRequest(string UserId, string Password);
    public record ValiderCertificatRequest(string UserId, string Password);
    public record RejeterCertificatRequest(string UserId, string Password, string Commentaire);
    public record DemanderModificationRequest(string UserId, string Commentaire);
}
