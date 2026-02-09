# 📋 GUIDE D'UTILISATION DES CODES UNIQUES

## 🎯 Principe

Dans le système COService, **tous les codes sont des clés uniques** dans leurs tables respectives et peuvent être utilisés comme identifiants principaux au lieu des GUIDs.

## ✅ Tables avec Codes Uniques

Toutes les tables suivantes ont un champ `Code` (ou `CodePartenaire`, `CodeExportateur`, etc.) avec un **index unique** :

### Organisations
- **Partenaires** : `CodePartenaire` (clé unique)
- **Exportateurs** : `CodeExportateur` (clé unique)
- **TypesPartenaires** : `Code` (clé unique)

### Référentiels Géographiques
- **Departements** : `Code` (clé unique, ex: "PNR", "OUE")
- **Pays** : `Code` (clé unique, ex: "FRA", "USA")
- **Ports** : `Code` (clé unique)
- **Aeroports** : `Code` (clé unique)

### Référentiels Transport
- **Modules** : `Code` (clé unique)
- **Incoterms** : `Code` (clé unique)
- **Corridors** : `Code` (clé unique)
- **RoutesNationales** : `Code` (clé unique)
- **Troncons** : `Code` (clé unique)
- **Sections** : `Code` (clé unique)

### Référentiels Douane
- **BureauxDedouanements** : `Code` (clé unique)
- **SectionsTariffaires** : `Code` (clé unique)
- **ChapitresTariffaires** : `Code` (clé unique)
- **DivisionsTariffaires** : `Code` (clé unique)
- **CategoriesTariffaires** : `Code` (clé unique)
- **PositionsTariffaires** : `Code` (clé unique)

### Référentiels Finance
- **Devises** : `Code` (clé unique, ex: "EUR", "USD", "XAF")

### Référentiels Statistiques
- **UniteStatistiques** : `Code` (clé unique)

### Autres
- **TypesCertificats** : `Code` (clé unique)
- **ZonesProductions** : `Code` (clé unique)

## 🔑 Utilisation des Codes dans le Code

### 1. Identification des Chambres de Commerce

Au lieu d'utiliser les anciens IDs entiers (`partner_id = 2`, `partner_id = 3`), utilisez directement les **codes uniques** :

```csharp
using COService.Shared.Constants;

// Méthode 1 : Comparaison directe avec les constantes (recommandé)
var certificat = await _repository.GetByIdAsync(id);
if (certificat.Partenaire?.CodePartenaire == ChambresCommerce.PointeNoire.CodePartenaire)
{
    // Workflow Pointe-Noire
}

if (certificat.Partenaire?.CodePartenaire == ChambresCommerce.Ouesso.CodePartenaire)
{
    // Workflow Ouesso
}

// Méthode 2 : Utilisation des méthodes helper des constantes
var codePartenaire = certificat.Partenaire?.CodePartenaire;
if (ChambresCommerce.EstPointeNoire(codePartenaire))
{
    // Workflow Pointe-Noire
}

if (ChambresCommerce.EstOuesso(codePartenaire))
{
    // Workflow Ouesso
}

// Récupérer le code partenaire directement (clé unique)
var codePartenaire = certificat.Partenaire?.CodePartenaire; // "PNR" ou "OUE"
```

### 2. Recherche par Code dans les Repositories

Tous les repositories ont des méthodes `GetByCodeAsync` :

```csharp
// Recherche par code partenaire (clé unique)
var partenaire = await _partenaireRepository.GetByCodeAsync("PNR");

// Recherche par code département (clé unique)
var departement = await _departementRepository.GetByCodeAsync("PNR");

// Recherche par code pays (clé unique)
var pays = await _paysRepository.GetByCodeAsync("FRA");

// Recherche par code devise (clé unique)
var devise = await _deviseRepository.GetByCodeAsync("EUR");
```

### 3. Utilisation dans les Workflows

Dans les workflows, utilisez directement les codes pour identifier les chambres :

```csharp
public async Task<CertificatOrigineDto> ValiderCertificatAsync(
    Guid certificatId, 
    string userId, 
    CancellationToken cancellationToken = default)
{
    var certificat = await _repository.GetByIdAsync(certificatId, cancellationToken);
    
    // Identifier la chambre directement par code partenaire (clé unique)
    var codePartenaire = certificat.Partenaire?.CodePartenaire;
    
    if (codePartenaire == ChambresCommerce.PointeNoire.CodePartenaire)
    {
        // Workflow spécifique Pointe-Noire
        return await ValiderCertificatPointeNoireAsync(certificat, userId, cancellationToken);
    }
    else if (codePartenaire == ChambresCommerce.Ouesso.CodePartenaire)
    {
        // Workflow spécifique Ouesso
        return await ValiderCertificatOuessoAsync(certificat, userId, cancellationToken);
    }
    
    throw new InvalidOperationException($"Chambre de commerce inconnue : {codePartenaire}");
}
```

### 4. Génération de Numéros de Certificats

Les codes département sont utilisés dans la génération des numéros :

```csharp
// Format : CO{Numéro}{Date}{CodeDépartement}
// Exemple : CO100000241031224PNR

var codeDepartement = certificat.Partenaire?.Departement?.Code; // "PNR" ou "OUE"
var numero = $"CO{numeroSequential}{dateFormatee}{codeDepartement}";
```

## 📝 Constantes Disponibles

### Chambres de Commerce

```csharp
using COService.Shared.Constants;

// Pointe-Noire
ChambresCommerce.PointeNoire.CodePartenaire; // "PNR"
ChambresCommerce.PointeNoire.CodeDepartement; // "PNR"
ChambresCommerce.PointeNoire.Nom; // "Chambre de Commerce de Pointe-Noire"

// Ouesso
ChambresCommerce.Ouesso.CodePartenaire; // "OUE"
ChambresCommerce.Ouesso.CodeDepartement; // "OUE"
ChambresCommerce.Ouesso.Nom; // "Chambre de Commerce d'Ouesso"
```

## ⚠️ Bonnes Pratiques

1. **Toujours utiliser les codes** pour identifier les entités dans les workflows et la logique métier
2. **Les GUIDs restent les clés primaires** pour les relations en base de données
3. **Les codes sont stables** et ne changent pas (contrairement aux GUIDs qui peuvent varier)
4. **Les codes sont lisibles** et faciles à manipuler dans le code
5. **Toujours vérifier l'existence** avant d'utiliser un code (via `GetByCodeAsync`)

## 🔍 Exemple Complet

```csharp
using COService.Shared.Constants;

public class WorkflowService
{
    private readonly ICertificatOrigineRepository _certificatRepository;
    
    public async Task ProcesserWorkflowAsync(Guid certificatId, CancellationToken cancellationToken = default)
    {
        // 1. Récupérer le certificat avec ses relations (inclure Partenaire)
        var certificat = await _certificatRepository.GetByIdAsync(certificatId, cancellationToken);
        if (certificat == null)
            throw new KeyNotFoundException($"Certificat {certificatId} introuvable");
        
        // 2. Identifier la chambre directement par code partenaire (clé unique)
        var codePartenaire = certificat.Partenaire?.CodePartenaire;
        
        // 3. Appliquer le workflow selon la chambre
        if (codePartenaire == ChambresCommerce.PointeNoire.CodePartenaire)
        {
            await ProcesserWorkflowPointeNoireAsync(certificat, cancellationToken);
        }
        else if (codePartenaire == ChambresCommerce.Ouesso.CodePartenaire)
        {
            await ProcesserWorkflowOuessoAsync(certificat, cancellationToken);
        }
        else
        {
            throw new InvalidOperationException($"Chambre de commerce inconnue : {codePartenaire}");
        }
    }
}
```

## 📚 Références

- **Constantes** : `COService.Shared/Constants/ChambresCommerce.cs`
- **Configurations EF** : Tous les fichiers dans `COService.Infrastructure/Data/Configurations/` avec `HasIndex(...).IsUnique()`

## 💡 Principe de Simplicité

**Pas besoin de helpers complexes** : Les codes étant des clés uniques, on peut les utiliser directement :

```csharp
// ✅ Simple et direct
if (certificat.Partenaire?.CodePartenaire == ChambresCommerce.PointeNoire.CodePartenaire)

// ❌ Inutilement complexe (helpers supprimés)
// if (ChambreCommerceHelper.EstPointeNoire(certificat))
```

Les constantes `ChambresCommerce` fournissent déjà les méthodes `EstPointeNoire()` et `EstOuesso()` si vous préférez cette syntaxe, mais la comparaison directe est plus simple et plus claire.

---

**Document créé le** : 2025-02-04  
**Version** : 1.0
