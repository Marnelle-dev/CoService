# 📋 PLAN DE MIGRATION COMPLÈTE - COService Microservice

## 🎯 Objectifs et Contraintes

### Contraintes importantes
1. **Noms de tables en français et PascalCase** : Toutes les tables doivent être nommées en français avec PascalCase (ex: `Certificats`, `Partenaires`, `LignesCertificat`)
2. **Organisations externes** : Les organisations (Partenaires, Exportateurs) sont gérées par le microservice **enrolement**. Elles sont représentées localement mais synchronisées depuis ce microservice
3. **Référentiels locaux synchronisés** : Les référentiels locaux (Department, WoodProductionArea, etc.) sont alimentés par le microservice référentiel global via synchronisation
4. **Authentification externe** : Gestion des utilisateurs via microservice Auth (pas de table `users` locale)
5. **Référentiels globaux via API** : Pays, Ports, Aéroports, etc. consultés via API du microservice référentiel

---

## 📊 État Actuel du Projet

### ✅ Entités déjà implémentées

| Entité | Table (actuelle) | Table (à corriger) | Statut |
|--------|------------------|-------------------|--------|
| `CertificatOrigine` | `certificates` | `Certificats` | ⚠️ À renommer |
| `CertificateLine` | `certificate_lines` | `LignesCertificats` | ⚠️ À renommer |
| `CertificateValidation` | `certificate_validations` | `ValidationsCertificats` | ⚠️ À renommer |
| `CertificateType` | `certificate_types` | `TypesCertificats` | ⚠️ À renommer |
| `Commentaire` | `commentaires` | `Commentaires` | ⚠️ À corriger |
| `Abonnement` | `abonnements` | `Abonnements` | ⚠️ À corriger |

**Action requise** : Renommer toutes les tables en PascalCase français

---

## 📝 Entités à Implémenter (par priorité)

### 🔴 Phase 1 : Organisations (PRIORITÉ HAUTE)

Ces entités sont essentielles car elles sont référencées par les certificats.

**⚠️ IMPORTANT** : Les organisations (Partenaire, Exportateur) sont gérées par le microservice **enrolement**. Elles sont représentées localement pour les relations avec les certificats, mais sont synchronisées depuis le microservice enrolement.

#### 1.1 Partenaire (Chambres de Commerce)
- **Entité** : `Partenaire`
- **Table** : `Partenaires`
- **Source** : Microservice **enrolement** (synchronisation)
- **Mode** : Lecture seule (synchronisation depuis enrolement)
- **Champs principaux** :
  - `Id` (Guid) - ID depuis enrolement
  - `CodePartenaire` (string, unique)
  - `Nom` (string)
  - `Adresse` (string)
  - `Telephone` (string)
  - `Email` (string)
  - `TypePartenaireId` (Guid?, FK vers `types_partenaires`)
  - `DepartementId` (Guid?, FK vers `Departements` - référentiel local)
  - `Actif` (bool)
  - `DerniereSynchronisation` (DateTime) - Date de dernière sync depuis enrolement
  - Champs d'audit (CreeLe, CreePar, ModifierLe, ModifiePar)
- **Relations** :
  - `HasMany` : Certificats, ZonesProductions, Logos, Facturations
  - `BelongsTo` : TypePartenaire, Departement
  - `HasMany` : Exportateurs (via `Exportateur.PartenaireId`)
- **Synchronisation** :
  - Service `EnrolementSyncService` qui appelle le microservice enrolement
  - Synchronisation périodique ou événementielle
  - Endpoints en lecture seule (GET uniquement)

#### 1.2 Exportateur
- **Entité** : `Exportateur`
- **Table** : `Exportateurs`
- **Source** : Microservice **enrolement** (synchronisation)
- **Mode** : Lecture seule (synchronisation depuis enrolement)
- **Champs principaux** :
  - `Id` (Guid) - ID depuis enrolement
  - `CodeExportateur` (string, unique)
  - `Nom` (string)
  - `RaisonSociale` (string)
  - `NIU` (string) - Numéro d'Identification Unique
  - `RCCM` (string)
  - `CodeActivite` (string)
  - `Adresse` (string)
  - `Telephone` (string)
  - `Email` (string)
  - `Actif` (bool)
  - `PartenaireId` (Guid?, FK vers `partenaires`)
  - `DepartementId` (Guid?, FK vers `Departements`)
  - `TypeExportateur` (int?) - Type d'exportateur
  - `DerniereSynchronisation` (DateTime) - Date de dernière sync depuis enrolement
  - Champs d'audit
- **Relations** :
  - `HasMany` : Certificats, DocumentsExportateurs, Logos, Facturations
  - `BelongsTo` : Partenaire, Departement
  - `BelongsTo` : Partenaire (via `PartenaireId`)
- **Synchronisation** :
  - Service `EnrolementSyncService` qui appelle le microservice enrolement
  - Synchronisation périodique ou événementielle
  - Endpoints en lecture seule (GET uniquement)

#### 1.3 Destinataire de Produits
- **Entité** : `DestinataireProduit`
- **Table** : `DestinatairesProduits`
- **Champs principaux** :
  - `Id` (Guid)
  - `Nom` (string)
  - `Adresse1`, `Adresse2` (string)
  - `Pays` (string) - Code pays (référentiel global via API)
  - `Ville` (string)
  - `CodePostal` (string)
  - `Email` (string)
  - `Telephone` (string)
  - `SiteWeb` (string?)
  - `OrganisationId` (Guid?) - ID de l'organisation propriétaire
  - Champs d'audit
- **Relations** :
  - `HasMany` : Certificats

#### 1.4 Table Pivot Exportateurs-Partenaires : Non nécessaire

**⚠️ Cette table n'est pas nécessaire dans COService.**

**Raison** :
- Un exportateur peut s'adresser à plusieurs chambres de commerce, mais cela se gère au niveau du **certificat** lui-même
- Chaque certificat a un `PartenaireId` qui indique la chambre de commerce concernée
- Pas besoin de table pivot pour gérer les associations exportateur-partenaire

**Approche** :
- Un exportateur peut créer des certificats pour **n'importe quelle chambre** (sélection au moment de la création)
- Le `PartenaireId` du certificat détermine la chambre de commerce
- Pas de validation d'association nécessaire via table pivot

---

### 🟠 Phase 2 : Référentiels Locaux (PRIORITÉ HAUTE)

Ces référentiels sont nécessaires localement mais sont **synchronisés depuis le microservice référentiel global**.

#### 2.1 Département
- **Entité** : `Departement`
- **Table** : `Departements`
- **Note** : Synchronisé depuis le microservice référentiel global
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit
- **Utilisation** : Génération de numéros de certificats

#### 2.2 Zone de Production
- **Entité** : `ZoneProduction`
- **Table** : `ZonesProductions`
- **Note** : Synchronisé depuis le microservice référentiel global
- **Champs** :
  - `Id` (Guid)
  - `PartenaireId` (Guid, FK vers `partenaires`)
  - `Nom` (string)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : Partenaire
  - `HasMany` : Certificats

#### 2.3 Type de Partenaire
- **Entité** : `TypePartenaire`
- **Table** : `TypesPartenaires`
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - Champs d'audit
- **Relations** :
  - `HasMany` : Partenaires

#### 2.4 Statut de Certificat
- **Entité** : `StatutCertificat`
- **Table** : `StatutsCertificats`
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Libelle` (string)
  - `Ordre` (int) - Pour le workflow
  - Champs d'audit
- **Relations** :
  - `HasMany` : Certificats

---

### 🟡 Phase 3 : Documents (PRIORITÉ MOYENNE)

#### 3.1 Document de Certificat
- **Entité** : `DocumentCertificat`
- **Table** : `DocumentsCertificats`
- **Champs** :
  - `Id` (Guid)
  - `CertificatId` (Guid, FK)
  - `CheminDocument` (string) - Chemin du fichier
  - `TypeDocument` (string)
  - `UploadPar` (string) - ID utilisateur (via Auth Service)
  - `DateUpload` (DateTime)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : CertificatOrigine

#### 3.2 Document d'Exportateur
- **Entité** : `DocumentExportateur`
- **Table** : `DocumentsExportateurs`
- **Champs** :
  - `Id` (Guid)
  - `ExportateurId` (Guid, FK)
  - `CheminDocument` (string)
  - `TypeDocument` (string)
  - `UploadPar` (string)
  - `DateUpload` (DateTime)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : Exportateur

#### 3.3 Logo
- **Entité** : `Logo`
- **Table** : `Logos`
- **Champs** :
  - `Id` (Guid)
  - `OrganisationId` (Guid) - ID de l'organisation (Partenaire ou Exportateur)
  - `TypeOrganisation` (string) - "Partenaire" ou "Exportateur"
  - `CheminLogo` (string)
  - `PartenaireId` (Guid?, FK optionnelle)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : Partenaire (optionnel)

---

### 🟡 Phase 4 : Financier (PRIORITÉ MOYENNE)

#### 4.1 Facturation
- **Entité** : `Facturation`
- **Table** : `Facturations`
- **Champs** :
  - `Id` (Guid)
  - `CertificatId` (Guid, FK)
  - `ExportateurId` (Guid, FK)
  - `PartenaireId` (Guid, FK)
  - `StatutFacturation` (string)
  - `Montant` (decimal)
  - `NumeroFacture` (string)
  - `DateCreation` (DateTime)
  - `DatePaiement` (DateTime?)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : CertificatOrigine, Exportateur, Partenaire

#### 4.2 Prix de Certificat
- **Entité** : `PrixCertificat`
- **Table** : `PrixCertificats`
- **Champs** :
  - `Id` (Guid)
  - `PartenaireId` (Guid, FK)
  - `ValeurPrix` (decimal)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : Partenaire

#### 4.3 Prix de Produit
- **Entité** : `PrixProduit`
- **Table** : `PrixProduits`
- **Champs** :
  - `Id` (Guid)
  - `ProduitId` (Guid, FK vers `produits`)
  - `PartenaireId` (Guid, FK)
  - `Prix` (decimal)
  - Champs d'audit
- **Relations** :
  - `BelongsTo` : Produit, Partenaire

---

### 🟡 Phase 5 : Produits (PRIORITÉ MOYENNE)

#### 5.1 Produit
- **Entité** : `Produit`
- **Table** : `Produits`
- **Champs** :
  - `Id` (Guid)
  - `CodeProduit` (string, unique)
  - `NomProduit` (string)
  - `Description` (string?)
  - `OrganisationId` (Guid?) - Organisation propriétaire
  - Champs d'audit
- **Relations** :
  - `HasMany` : LignesCertificat, PrixProduits

---

### 🟢 Phase 6 : Autres (PRIORITÉ BASSE)

#### 6.1 Port du Congo
- **Entité** : `PortCongo`
- **Table** : `PortsCongo`
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - Champs d'audit
- **Relations** :
  - `HasMany` : Certificats

#### 6.2 Visa Client
- **Entité** : `VisaClient`
- **Table** : `VisasClients`
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Libelle` (string)
  - Champs d'audit
- **Relations** :
  - `HasMany` : Certificats

---

## 🔄 Synchronisation des Données Externes

### 1. Organisations (Microservice Enrolement)

Les organisations (Partenaire, Exportateur) sont gérées par le microservice **enrolement** et doivent être synchronisées localement.

#### Service de Synchronisation : `EnrolementSyncService`

**Localisation** : `COService.Infrastructure/Services/EnrolementSyncService.cs`

**Responsabilités** :
- Appeler le microservice enrolement pour récupérer les organisations
- Synchroniser les Partenaires et Exportateurs
- Mettre à jour les données locales
- Gérer les conflits et les erreurs

**Mécanismes de synchronisation** :

**Option 1 : Synchronisation périodique (Polling)**
```csharp
// Service qui s'exécute périodiquement (ex: toutes les heures)
public class EnrolementSyncService : IHostedService
{
    // Appelle l'API enrolement et synchronise les données
}
```

**Option 2 : Synchronisation événementielle (Event-Driven)**
- Le microservice enrolement publie des événements lors des modifications
- COService écoute ces événements et met à jour localement

**Option 3 : Synchronisation à la demande**
- Endpoint API pour forcer une synchronisation manuelle
- Synchronisation lors de la première utilisation d'une organisation

**Recommandation** : Implémenter une combinaison (périodique + à la demande)

#### Client API Enrolement

**Localisation** : `COService.Infrastructure/Clients/IEnrolementServiceClient.cs`

**Interface** :
```csharp
public interface IEnrolementServiceClient
{
    Task<PartenaireDto> GetPartenaireAsync(Guid id);
    Task<List<PartenaireDto>> GetAllPartenairesAsync();
    Task<ExportateurDto> GetExportateurAsync(Guid id);
    Task<List<ExportateurDto>> GetAllExportateursAsync();
    Task<List<ExportateurDto>> GetExportateursByPartenaireAsync(Guid partenaireId);
}
```

**Implémentation** : Utiliser Refit ou HttpClient pour appeler le microservice enrolement

### 2. Référentiels Locaux (Microservice Référentiel Global)

Les référentiels locaux (Departement, ZoneProduction) sont synchronisés depuis le microservice référentiel global.

#### Service de Synchronisation : `ReferentielSyncService`

**Localisation** : `COService.Infrastructure/Services/ReferentielSyncService.cs`

**Responsabilités** :
- Appeler le microservice référentiel pour récupérer les données
- Synchroniser Departements et ZonesProductions
- Mettre à jour les données locales

**Mécanismes** : Similaires à EnrolementSyncService (périodique, événementiel, à la demande)

---

## 📋 Plan d'Action Détaillé

### Étape 1 : Correction des noms de tables existantes

1. Renommer `certificates` → `Certificats`
2. Renommer `certificate_lines` → `LignesCertificats`
3. Renommer `certificate_validations` → `ValidationsCertificats`
4. Renommer `certificate_types` → `TypesCertificats`
5. Renommer `commentaires` → `Commentaires`
6. Renommer `abonnements` → `Abonnements`

**Action** : Créer une migration pour renommer toutes les tables en PascalCase

---

### Étape 2 : Implémentation Phase 1 (Organisations)

#### 2.1 Partenaire et Exportateur (Synchronisés depuis Enrolement)

Pour Partenaire et Exportateur :

1. **Créer l'entité** dans `COService.Domain/Entities/`
   - Propriétés avec noms en français
   - Ajouter `DerniereSynchronisation` (DateTime)
   - Relations de navigation

2. **Créer les DTOs** dans `COService.Application/DTOs/`
   - `PartenaireDto` (pas de CreerDto/ModifierDto car lecture seule)
   - `ExportateurDto` (pas de CreerDto/ModifierDto car lecture seule)

3. **Créer la configuration EF Core** dans `COService.Infrastructure/Data/Configurations/`
   - Nom de table en français
   - Noms de colonnes en français
   - Relations et contraintes

4. **Créer le repository** dans `COService.Infrastructure/Repositories/`
   - Interface dans `COService.Application/Repositories/`
   - Implémentation dans `COService.Infrastructure/Repositories/`
   - Méthodes : GetByIdAsync, GetAllAsync, GetByCodeAsync (lecture seule)

5. **Créer le client API Enrolement** dans `COService.Infrastructure/Clients/`
   - Interface `IEnrolementServiceClient`
   - Implémentation avec Refit ou HttpClient
   - Méthodes pour récupérer Partenaires et Exportateurs

6. **Créer le service de synchronisation** dans `COService.Infrastructure/Services/`
   - `EnrolementSyncService` : Synchronise les organisations depuis enrolement
   - Peut être un `IHostedService` pour synchronisation périodique
   - Méthode `SynchroniserPartenairesAsync()`, `SynchroniserExportateursAsync()`

7. **Créer le service métier** dans `COService.Application/Services/`
   - Interface `IPartenaireService` et `IExportateurService`
   - Implémentation : Lecture depuis le repository local
   - Optionnel : Méthode pour forcer la synchronisation

8. **Créer les endpoints** dans `COService.API/Endpoints/`
   - **GET uniquement** : `/api/partenaires`, `/api/partenaires/{id}`, `/api/partenaires/code/{code}`
   - **GET uniquement** : `/api/exportateurs`, `/api/exportateurs/{id}`, `/api/exportateurs/code/{code}`
   - **POST** : `/api/sync/enrolement` - Endpoint pour forcer la synchronisation (admin uniquement)

9. **Ajouter le DbSet** dans `COServiceDbContext`

10. **Créer la migration** EF Core

#### 2.2 DestinataireProduit (Géré localement)

Pour DestinataireProduit (géré localement, pas de synchronisation) :

1. **Créer l'entité** dans `COService.Domain/Entities/`
2. **Créer les DTOs** : `DestinataireProduitDto`, `CreerDestinataireProduitDto`, `ModifierDestinataireProduitDto`
3. **Créer la configuration EF Core**
4. **Créer le repository** (CRUD complet)
5. **Créer le service** (CRUD complet)
6. **Créer les endpoints** (CRUD complet)
7. **Ajouter le DbSet** dans `COServiceDbContext`
8. **Créer la migration** EF Core

---

### Étape 3 : Implémentation Phase 2 (Référentiels Locaux)

Même processus que Phase 1, mais avec :
- Service de synchronisation avec le microservice référentiel global
- Endpoints en lecture seule (ou avec synchronisation)

---

### Étape 4 : Implémentation Phases 3-6

Même processus itératif pour chaque entité.

---

## 🔌 Intégrations Externes

### Microservice Enrolement
- **Gestion des organisations** : Partenaires, Exportateurs
- **Synchronisation locale** : Les organisations sont représentées localement mais synchronisées depuis enrolement
- **Service client** : `IEnrolementServiceClient` dans `COService.Infrastructure/Clients/`
- **Service de sync** : `EnrolementSyncService` pour synchroniser périodiquement ou à la demande
- **Endpoints** : Lecture seule (GET) pour les organisations, pas de création/modification directe

### Microservice Authentification
- **Pas de table `users` locale**
- Utilisation de tokens JWT
- Vérification des rôles via API Auth Service
- Stockage de `UserId` (string) dans les entités (pas de FK)

### Microservice Référentiel Global
- **Consultation via API** : Pays, Ports, Aéroports, Fleuves, Routes, Corridors, etc.
- **Synchronisation locale** : Departements, ZonesProductions
- **Service client HTTP** : `IReferentielServiceClient` dans `COService.Infrastructure/Clients/`
- **Service de sync** : `ReferentielSyncService` pour synchroniser les référentiels locaux

### Microservice Notifications
- Envoi d'emails lors des changements de statut
- Intégration via API

---

## 📝 Conventions de Nommage

### Tables
- **Format** : Nom en français, pluriel (s ou aux), PascalCase
- **Exemples** : `Partenaires`, `Exportateurs`, `DestinatairesProduits`, `Certificats`, `LignesCertificats`, `ValidationsCertificats`, `TypesCertificats`

### Colonnes
- **Format** : Nom en français, PascalCase
- **Exemples** : `CodePartenaire`, `RaisonSociale`, `DateCreation`

### Entités C#
- **Format** : Nom en français, singulier, PascalCase
- **Exemples** : `Partenaire`, `Exportateur`, `DestinataireProduit`

### DTOs
- **Format** : `{NomEntite}Dto`, `Creer{NomEntite}Dto`, `Modifier{NomEntite}Dto`
- **Exemples** : `PartenaireDto`, `CreerPartenaireDto`, `ModifierPartenaireDto`

---

## ✅ Checklist de Validation

Pour chaque entité implémentée, vérifier :

- [ ] Entité créée avec toutes les propriétés
- [ ] Table nommée en français avec PascalCase et au pluriel (ex: `Certificats`, `Partenaires`, `LignesCertificats`)
- [ ] Colonnes nommées en français avec PascalCase
- [ ] Configuration EF Core complète
- [ ] Repository créé (interface + implémentation)
- [ ] Service créé (interface + implémentation)
- [ ] DTOs créés (Dto, CreerDto, ModifierDto)
- [ ] Mapping AutoMapper configuré
- [ ] Endpoints API créés (CRUD complet)
- [ ] DbSet ajouté dans DbContext
- [ ] Migration créée et testée
- [ ] Relations configurées correctement
- [ ] Champs d'audit présents (CreeLe, CreePar, ModifierLe, ModifiePar)

---

## 🎯 Prochaines Actions

1. **Corriger les noms de tables existantes** (migration)
2. **Commencer par Partenaire** (Phase 1.1)
3. **Implémenter Exportateur** (Phase 1.2)
4. **Implémenter DestinataireProduit** (Phase 1.3)
5. **Continuer avec les autres phases**

---

**Document créé le** : 2025-01-XX  
**Version** : 1.0  
**Statut** : Planification
