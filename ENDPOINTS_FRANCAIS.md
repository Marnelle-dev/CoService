# Endpoints en Français - COService

## Convention de Nommage

Tous les endpoints de l'API COService utilisent des noms en français pour une meilleure cohérence avec le domaine métier.

## Endpoints Actuels

### Vérification de Santé

#### GET `/sante`
- **Nom** : `VerificationSante`
- **Description** : Vérification simple de l'état du service
- **Réponse** :
```json
{
  "statut": "sain",
  "horodatage": "2025-01-26T10:30:00Z",
  "service": "COService"
}
```

#### GET `/sante/detaillee`
- **Nom** : `VerificationSanteDetaillee`
- **Description** : Vérification détaillée de l'état du service et de ses dépendances
- **Réponse** :
```json
{
  "statut": "sain",
  "horodatage": "2025-01-26T10:30:00Z",
  "service": "COService",
  "verifications": {
    "baseDeDonnees": "ok",
    "consul": "ok"
  }
}
```

## Convention pour les Futurs Endpoints

### Structure des Routes

Les routes suivent le pattern suivant :
- `/api/certificats-origine` - Ressource principale
- `/api/certificats-origine/demandes` - Sous-ressource
- `/api/certificats-origine/{id}/telecharger` - Action sur une ressource

### Exemples de Futurs Endpoints

#### Certificats d'Origine

```
GET    /api/certificats-origine                    - Liste des certificats
GET    /api/certificats-origine/{id}               - Détails d'un certificat
POST   /api/certificats-origine                    - Créer un certificat
PUT    /api/certificats-origine/{id}               - Modifier un certificat
DELETE /api/certificats-origine/{id}               - Supprimer un certificat
GET    /api/certificats-origine/{id}/telecharger   - Télécharger le PDF
GET    /api/certificats-origine/{id}/progression   - Barre de progression
```

#### Demandes

```
GET    /api/certificats-origine/demandes            - Liste des demandes
GET    /api/certificats-origine/demandes/{id}       - Détails d'une demande
POST   /api/certificats-origine/demandes            - Créer une demande
GET    /api/certificats-origine/demandes/exportateur/{exportateurId} - Demandes par exportateur
```

#### Validations

```
GET    /api/certificats-origine/{id}/validations    - Liste des validations
POST   /api/certificats-origine/{id}/validations    - Ajouter une validation
```

#### Commentaires

```
GET    /api/certificats-origine/{id}/commentaires   - Liste des commentaires
POST   /api/certificats-origine/{id}/commentaires   - Ajouter un commentaire
```

### Convention de Nommage des Endpoints

- **Noms des méthodes** : En français avec PascalCase
  - Exemple : `VerificationSante`, `CreerCertificat`, `TelechargerCertificat`

- **Tags Swagger** : En français
  - Exemple : `"Santé"`, `"Certificats d'Origine"`, `"Validations"`

- **Résumés** : En français avec `.WithSummary()`
  - Exemple : `"Créer un nouveau certificat d'origine"`

- **Propriétés JSON** : En français (camelCase)
  - Exemple : `statut`, `horodatage`, `baseDeDonnees`

### Exemple de Code

```csharp
public static class CertificatOrigineEndpoints
{
    public static void MapCertificatOrigineEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/certificats-origine")
            .WithTags("Certificats d'Origine");
        
        // Créer un certificat
        group.MapPost("/", async (
            CreerCertificatDto dto,
            ICertificatOrigineService service) =>
        {
            var result = await service.CreerCertificatAsync(dto);
            return Results.Created($"/api/certificats-origine/{result.Id}", result);
        })
        .WithName("CreerCertificat")
        .WithSummary("Créer un nouveau certificat d'origine")
        .Produces<CertificatOrigineDto>(StatusCodes.Status201Created);
        
        // Télécharger un certificat
        group.MapGet("/{id}/telecharger", async (
            Guid id,
            ICertificatOrigineService service) =>
        {
            var fichier = await service.TelechargerCertificatAsync(id);
            return Results.File(fichier, "application/pdf");
        })
        .WithName("TelechargerCertificat")
        .WithSummary("Télécharger le certificat d'origine en PDF")
        .Produces<FileResult>(StatusCodes.Status200OK);
    }
}
```

## Configuration Consul

L'endpoint de health check pour Consul est configuré en français :
- **Endpoint** : `/sante`
- **Configuration** : `appsettings.json` → `Consul.HealthCheck.Endpoint`

## Notes

- ✅ Tous les endpoints utilisent des noms français
- ✅ Les réponses JSON utilisent des propriétés en français
- ✅ Les tags Swagger sont en français
- ✅ Les résumés sont en français
- ✅ Consul est configuré pour utiliser `/sante`

