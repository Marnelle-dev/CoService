using COService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour la génération de PDFs de certificats
/// </summary>
public static class PDFEndpoints
{
    public static void MapPDFEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pdf")
            .WithTags("PDF Generation");

        // GET /api/pdf/{id} - Génère le PDF selon le type de certificat
        group.MapGet("/{id:guid}", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFParTypeAsync(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"certificat-{id}.pdf");
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
        .WithName("GenererPDF")
        .WithSummary("Génère le PDF selon le type de certificat (détection automatique)")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/co - Génère le PDF CO standard
        group.MapGet("/{id:guid}/co", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFCertificatOrigineAsync(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"co-{id}.pdf");
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
        .WithName("GenererPDFCO")
        .WithSummary("Génère le PDF de certificat d'origine standard")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/ouesso - Génère le PDF CO Ouesso
        group.MapGet("/{id:guid}/ouesso", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFCertificatOuessoAsync(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"co-ouesso-{id}.pdf");
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
        .WithName("GenererPDFOuesso")
        .WithSummary("Génère le PDF de certificat d'origine pour Ouesso")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/formule-a - Génère le PDF Formule A
        group.MapGet("/{id:guid}/formule-a", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFFormuleAAsync(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"formule-a-{id}.pdf");
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
        .WithName("GenererPDFFormuleA")
        .WithSummary("Génère le PDF de Formule A")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/eur1 - Génère le PDF EUR.1
        group.MapGet("/{id:guid}/eur1", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFEUR1Async(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"eur1-{id}.pdf");
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
        .WithName("GenererPDFEUR1")
        .WithSummary("Génère le PDF EUR.1")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/alc - Génère le PDF ALC
        group.MapGet("/{id:guid}/alc", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var pdfBytes = await service.GenererPDFALCAsync(id, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", $"alc-{id}.pdf");
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
        .WithName("GenererPDFALC")
        .WithSummary("Génère le PDF ALC")
        .Produces<byte[]>(StatusCodes.Status200OK, "application/pdf")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // GET /api/pdf/{id}/qr-code - Génère le QR Code
        group.MapGet("/{id:guid}/qr-code", async (
            Guid id,
            IPDFGenerationService service,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var qrCode = await service.GenererQRCodeAsync(id, cancellationToken);
                return Results.Ok(new { qrCode });
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        })
        .WithName("GenererQRCode")
        .WithSummary("Génère le QR Code pour un certificat")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
