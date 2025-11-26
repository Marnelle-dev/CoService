namespace COService.API.Endpoints;

/// <summary>
/// Endpoints pour les vérifications de santé
/// </summary>
public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sante")
            .WithTags("Santé");

        // Vérification de santé simple
        group.MapGet("/", () => Results.Ok(new
        {
            statut = "sain",
            horodatage = DateTime.UtcNow,
            service = "COService"
        }))
        .WithName("VerificationSante")
        .WithSummary("Vérification simple de l'état du service")
        .Produces(StatusCodes.Status200OK);

        // Vérification de santé détaillée (avec vérification de la base de données, etc.)
        group.MapGet("/detaillee", (HttpContext context) =>
        {
            var healthStatus = new
            {
                statut = "sain",
                horodatage = DateTime.UtcNow,
                service = "COService",
                verifications = new
                {
                    baseDeDonnees = "ok", // TODO: Vérifier la connexion à la base de données
                    consul = "ok"         // TODO: Vérifier la connexion à Consul
                }
            };

            return Results.Ok(healthStatus);
        })
        .WithName("VerificationSanteDetaillee")
        .WithSummary("Vérification détaillée de l'état du service et de ses dépendances")
        .Produces(StatusCodes.Status200OK);
    }
}

