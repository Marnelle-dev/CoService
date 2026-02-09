# 🔧 VALEURS TEMPORAIRES POUR IDENTIFIER LES CHAMBRES DE COMMERCE

## ⚠️ IMPORTANT : Valeurs Temporaires

Ce document liste les **valeurs temporaires** utilisées pour identifier les chambres de commerce en attendant la synchronisation avec le microservice **Enrolement**.

**Ces valeurs seront remplacées** par les vraies valeurs une fois que la synchronisation avec Enrolement sera opérationnelle.

---

## 📋 Valeurs Temporaires Proposées

### 🏢 Chambre de Commerce de Pointe-Noire

| Champ | Valeur Temporaire | Description | Où Utilisé |
|-------|-------------------|-------------|------------|
| **CodePartenaire** | `"PNR"` | Code unique du partenaire (clé unique) | Identification principale dans les workflows |
| **Code Département** | `"PNR"` | Code du département (clé unique) | Génération des numéros de certificats |
| **Code TypePartenaire** | `"CCI_STANDARD"` | Type de chambre de commerce standard | Classification (optionnel) |
| **Nom** | `"Chambre de Commerce de Pointe-Noire"` | Nom complet | Affichage, logs |

**Ancien système (référence)** :
- `partner_id = 2`
- `type = 1`
- `Code Département = "PNR"`

---

### 🏢 Chambre de Commerce d'Ouesso

| Champ | Valeur Temporaire | Description | Où Utilisé |
|-------|-------------------|-------------|------------|
| **CodePartenaire** | `"OUE"` | Code unique du partenaire (clé unique) | Identification principale dans les workflows |
| **Code Département** | `"OUE"` | Code du département (clé unique) | Génération des numéros de certificats |
| **Code TypePartenaire** | `"CCI_SPECIALE"` | Type de chambre de commerce spéciale | Classification (optionnel) |
| **Nom** | `"Chambre de Commerce d'Ouesso"` | Nom complet | Affichage, logs |

**Ancien système (référence)** :
- `partner_id = 3`
- `type = 3`
- `Code Département = "OUE"`

---

## 🔑 Identifiants Principaux

### Méthode 1 : CodePartenaire (Recommandé)

**Utilisation** : `certificat.Partenaire?.CodePartenaire`

```csharp
// Identification par CodePartenaire (clé unique)
if (certificat.Partenaire?.CodePartenaire == ChambresCommerce.PointeNoire.CodePartenaire)
{
    // Workflow Pointe-Noire
}
```

**Avantages** :
- ✅ Clé unique dans la table `Partenaires`
- ✅ Stable et ne change pas
- ✅ Facile à manipuler

### Méthode 2 : Code Département (Alternative)

**Utilisation** : `certificat.Partenaire?.Departement?.Code`

```csharp
// Identification par Code Département (clé unique)
if (certificat.Partenaire?.Departement?.Code == ChambresCommerce.PointeNoire.CodeDepartement)
{
    // Workflow Pointe-Noire
}
```

**Avantages** :
- ✅ Clé unique dans la table `Departements`
- ✅ Utilisé dans la génération des numéros de certificats
- ✅ Peut servir de fallback si CodePartenaire n'est pas disponible

---

## 📝 Constantes Définies

Les valeurs sont définies dans `COService.Shared/Constants/ChambresCommerce.cs` :

```csharp
public static class ChambresCommerce
{
    public static class PointeNoire
    {
        public const string CodePartenaire = "PNR";  // ⚠️ TEMPORAIRE
        public const string CodeDepartement = "PNR";  // ⚠️ TEMPORAIRE
        public const string Nom = "Chambre de Commerce de Pointe-Noire";
    }

    public static class Ouesso
    {
        public const string CodePartenaire = "OUE";  // ⚠️ TEMPORAIRE
        public const string CodeDepartement = "OUE";  // ⚠️ TEMPORAIRE
        public const string Nom = "Chambre de Commerce d'Ouesso";
    }
}
```

---

## 🔄 Migration vers les Vraies Valeurs

### Étape 1 : Synchronisation avec Enrolement

Une fois que le microservice **Enrolement** sera opérationnel et que la synchronisation fonctionnera :

1. Les partenaires seront synchronisés depuis Enrolement
2. Les `CodePartenaire` réels seront chargés dans la base de données
3. Les `Code` des départements réels seront chargés depuis le référentiel global

### Étape 2 : Mise à Jour des Constantes

**Option A : Mise à jour manuelle des constantes**

1. Vérifier les codes réels dans la base de données après synchronisation
2. Mettre à jour les constantes dans `ChambresCommerce.cs`
3. Redéployer l'application

**Option B : Configuration externe (Recommandé)**

Déplacer les valeurs dans `appsettings.json` :

```json
{
  "ChambresCommerce": {
    "PointeNoire": {
      "CodePartenaire": "PNR",  // À remplacer par la vraie valeur
      "CodeDepartement": "PNR"   // À remplacer par la vraie valeur
    },
    "Ouesso": {
      "CodePartenaire": "OUE",  // À remplacer par la vraie valeur
      "CodeDepartement": "OUE"   // À remplacer par la vraie valeur
    }
  }
}
```

Puis charger depuis la configuration au lieu de constantes.

**Option C : Détection automatique (Idéal)**

Créer un service qui détecte automatiquement les chambres depuis la base de données :

```csharp
// Détection automatique depuis la base
var partenaires = await _partenaireRepository.GetAllAsync();
var pointeNoire = partenaires.FirstOrDefault(p => 
    p.Nom.Contains("Pointe-Noire", StringComparison.OrdinalIgnoreCase));
var ouesso = partenaires.FirstOrDefault(p => 
    p.Nom.Contains("Ouesso", StringComparison.OrdinalIgnoreCase));
```

---

## ✅ Checklist de Migration

Quand les vraies valeurs seront disponibles :

- [ ] Vérifier les `CodePartenaire` réels dans la table `Partenaires` après synchronisation
- [ ] Vérifier les `Code` réels dans la table `Departements` après synchronisation
- [ ] Mettre à jour les constantes dans `ChambresCommerce.cs` OU
- [ ] Déplacer vers `appsettings.json` OU
- [ ] Implémenter la détection automatique
- [ ] Tester que les workflows fonctionnent avec les nouvelles valeurs
- [ ] Documenter les nouvelles valeurs dans ce fichier

---

## 🎯 Utilisation dans le Code

### Exemple : Identification dans un Workflow

```csharp
public async Task<CertificatOrigineDto> ValiderCertificatAsync(
    Guid certificatId, 
    string userId, 
    CancellationToken cancellationToken = default)
{
    var certificat = await _repository.GetByIdAsync(certificatId, cancellationToken);
    
    // Identification par CodePartenaire (clé unique - valeur temporaire)
    var codePartenaire = certificat.Partenaire?.CodePartenaire;
    
    if (codePartenaire == ChambresCommerce.PointeNoire.CodePartenaire) // "PNR" (temporaire)
    {
        // Workflow spécifique Pointe-Noire
        return await ValiderCertificatPointeNoireAsync(certificat, userId, cancellationToken);
    }
    else if (codePartenaire == ChambresCommerce.Ouesso.CodePartenaire) // "OUE" (temporaire)
    {
        // Workflow spécifique Ouesso
        return await ValiderCertificatOuessoAsync(certificat, userId, cancellationToken);
    }
    
    throw new InvalidOperationException($"Chambre de commerce inconnue : {codePartenaire}");
}
```

---

## 📌 Notes Importantes

1. **Les codes sont des clés uniques** : Une fois synchronisés, ils seront garantis uniques par la base de données
2. **Les valeurs temporaires sont cohérentes** : "PNR" pour Pointe-Noire, "OUE" pour Ouesso
3. **Facile à remplacer** : Les constantes sont centralisées dans un seul fichier
4. **Pas d'impact sur la logique** : La logique des workflows reste la même, seules les valeurs changent

---

**Document créé le** : 2025-02-04  
**Version** : 1.0  
**Statut** : ⚠️ Valeurs temporaires - À mettre à jour lors de l'intégration avec Enrolement
