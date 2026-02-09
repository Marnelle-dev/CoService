# 📊 TABLES LIÉES AUX MICROSERVICES EXTERNES

## Vue d'ensemble

Ce document liste toutes les tables du microservice COService qui sont liées aux microservices externes (référentiel, enrolement, authentification).

---

## 🔄 MICROSERVICE ENROLEMENT

### Tables synchronisées localement

Ces tables sont **représentées localement** dans COService mais sont **gérées et synchronisées** depuis le microservice **enrolement**.

#### 1. Partenaires (Chambres de Commerce)
- **Table** : `Partenaires`
- **Entité** : `Partenaire`
- **Source** : Microservice **enrolement**
- **Mode** : Synchronisation (lecture seule dans COService)
- **Champs principaux** :
  - `Id` (Guid) - ID depuis enrolement
  - `CodePartenaire` (string, unique)
  - `Nom` (string)
  - `Adresse` (string)
  - `Telephone` (string)
  - `Email` (string)
  - `TypePartenaireId` (Guid?, FK vers `TypesPartenaires`)
  - `DepartementId` (Guid?, FK vers `Departements`)
  - `Actif` (bool)
  - `DerniereSynchronisation` (DateTime) - Date de dernière sync
  - Champs d'audit

**Relations dans COService** :
- `HasMany` : Certificats, ZonesProductions, Logos, Facturations
- `BelongsTo` : TypePartenaire, Departement
- `HasMany` : Exportateurs (via `Exportateur.PartenaireId`)

**Service de synchronisation** : `EnrolementSyncService`

---

#### 2. Exportateurs
- **Table** : `Exportateurs`
- **Entité** : `Exportateur`
- **Source** : Microservice **enrolement**
- **Mode** : Synchronisation (lecture seule dans COService)
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
  - `PartenaireId` (Guid?, FK vers `Partenaires`)
  - `DepartementId` (Guid?, FK vers `Departements`)
  - `TypeExportateur` (int?) - Type d'exportateur
  - `DerniereSynchronisation` (DateTime) - Date de dernière sync
  - Champs d'audit

**Relations dans COService** :
- `HasMany` : Certificats, DocumentsExportateurs, Logos, Facturations
- `BelongsTo` : Partenaire (via `PartenaireId`), Departement

**Service de synchronisation** : `EnrolementSyncService`

---

### ⚠️ Table ExportateursPartenaires : Non nécessaire

La table pivot `ExportateursPartenaires` **n'est pas nécessaire** dans COService.

**Raison** :
- Un exportateur peut s'adresser à plusieurs chambres de commerce, mais cela se gère au niveau du **certificat** lui-même
- Chaque certificat a un `PartenaireId` qui indique la chambre de commerce concernée
- Pas besoin de table pivot pour gérer les associations exportateur-partenaire

**Approche** :
- Un exportateur peut créer des certificats pour **n'importe quelle chambre** (sélection au moment de la création)
- Le `PartenaireId` du certificat détermine la chambre de commerce
- Pas de validation d'association nécessaire via table pivot

---

## 🌍 MICROSERVICE RÉFÉRENTIEL GLOBAL

### Tables synchronisées localement

Ces tables sont **nécessaires localement** pour le fonctionnement du microservice COService mais sont **synchronisées** depuis le microservice **référentiel global** car elles sont utilisées par plusieurs microservices.

#### 1. Departements
- **Table** : `Departements`
- **Entité** : `Departement`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique) - Ex: "PNR", "OUE"
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Génération de numéros de certificats (format : `CO{Numéro}{Date}{CodeDépartement}`)
- Relation avec Partenaires (chaque partenaire a un département)

**Service de synchronisation** : `ReferentielSyncService`

---

#### 2. Pays
- **Table** : `Pays`
- **Entité** : `Pays`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique) - Code ISO pays
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pays de destination dans certificats
- Validation des données

**Service de synchronisation** : `ReferentielSyncService`

---

#### 3. Ports
- **Table** : `Ports`
- **Entité** : `Port`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `PaysId` (Guid, FK vers `Pays`)
  - `Type` (string) - Maritime, Fluvial
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection port de destination, port de sortie
- Filtrage par pays

**Service de synchronisation** : `ReferentielSyncService`

---

#### 4. Aéroports
- **Table** : `Aeroports`
- **Entité** : `Aeroport`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `PaysId` (Guid, FK vers `Pays`)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pour transport aérien
- Filtrage par pays

**Service de synchronisation** : `ReferentielSyncService`

---

#### 5. Fleuves : Non nécessaire
- **Note** : Les ports fluviaux sont gérés via la table `Ports` avec le champ `Type` (Maritime, Fluvial)
- **Pas de table séparée** : La table `Fleuves` n'est pas nécessaire

---

#### 6. RoutesNationales
- **Table** : `RoutesNationales`
- **Entité** : `RouteNationale`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pour transport routier

**Service de synchronisation** : `ReferentielSyncService`

---

#### 7. Corridors
- **Table** : `Corridors`
- **Entité** : `Corridor`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pour transport

**Service de synchronisation** : `ReferentielSyncService`

---

#### 8. Tronçons
- **Table** : `Troncons`
- **Entité** : `Troncon`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `CorridorId` (Guid, FK vers `Corridors`)
  - `RouteId` (Guid, FK vers `RoutesNationales`)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pour transport

**Service de synchronisation** : `ReferentielSyncService`

---

#### 9. Sections
- **Table** : `Sections`
- **Entité** : `Section`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection pour transport

**Service de synchronisation** : `ReferentielSyncService`

---

#### 10. Devises
- **Table** : `Devises`
- **Entité** : `Devise`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique) - Ex: "EUR", "USD", "XAF"
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection devise dans certificats
- Calculs de valeurs

**Service de synchronisation** : `ReferentielSyncService`

---

#### 11. TauxDeChanges
- **Table** : `TauxDeChanges`
- **Entité** : `TauxDeChange`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `DeviseId` (Guid, FK vers `Devises`)
  - `Source` (string)
  - `Taux` (decimal)
  - `ValideDe` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Calculs financiers
- Conversions de devises

**Service de synchronisation** : `ReferentielSyncService`

---

#### 12. Incoterms
- **Table** : `Incoterms`
- **Entité** : `Incoterm`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `ModuleId` (Guid, FK vers `Modules`)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection dans certificats

**Service de synchronisation** : `ReferentielSyncService`

---

#### 13. BureauxDedouanements
- **Table** : `BureauxDedouanements`
- **Entité** : `BureauDedouanement`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection dans certificats

**Service de synchronisation** : `ReferentielSyncService`

---

#### 14. SectionsTariffaires
- **Table** : `SectionsTariffaires`
- **Entité** : `SectionTarifaire`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des produits

**Service de synchronisation** : `ReferentielSyncService`

---

#### 15. ChapitresTariffaires
- **Table** : `ChapitresTariffaires`
- **Entité** : `ChapitreTarifaire`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des produits

**Service de synchronisation** : `ReferentielSyncService`

---

#### 16. DivisionsTariffaires
- **Table** : `DivisionsTariffaires`
- **Entité** : `DivisionTarifaire`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `ChapitreId` (Guid, FK vers `ChapitresTariffaires`)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des produits

**Service de synchronisation** : `ReferentielSyncService`

---

#### 17. CategoriesTariffaires
- **Table** : `CategoriesTariffaires`
- **Entité** : `CategorieTarifaire`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `DivisionCode` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des produits

**Service de synchronisation** : `ReferentielSyncService`

---

#### 18. PositionsTariffaires
- **Table** : `PositionsTariffaires`
- **Entité** : `PositionTarifaire`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Description` (string)
  - `CategorieCodeId` (Guid, FK vers `CategoriesTariffaires`)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des produits

**Service de synchronisation** : `ReferentielSyncService`

---

#### 19. Modules (Modes de transport)
- **Table** : `Modules`
- **Entité** : `Module`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Sélection mode de transport (Aérien, Maritime, Fluvial, Routier)

**Service de synchronisation** : `ReferentielSyncService`

---

#### 20. TypeTransports
- **Table** : `TypeTransports`
- **Entité** : `TypeTransport`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Classification des types de transport

**Service de synchronisation** : `ReferentielSyncService`

---

#### 21. UniteDeChargements
- **Table** : `UniteDeChargements`
- **Entité** : `UniteDeChargement`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Description` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Unités de chargement dans certificats

**Service de synchronisation** : `ReferentielSyncService`

---

#### 22. UniteStatistiques
- **Table** : `UniteStatistiques`
- **Entité** : `UniteStatistique`
- **Source** : Microservice **référentiel global**
- **Mode** : Synchronisation locale
- **Champs** :
  - `Id` (Guid)
  - `Code` (string, unique)
  - `Nom` (string)
  - `Actif` (bool)
  - Champs d'audit

**Utilisation dans COService** :
- Unités statistiques dans certificats

**Service de synchronisation** : `ReferentielSyncService`

---

### Note importante

Tous les référentiels partagés (Pays, Ports, Aéroports, Devises, etc.) sont **synchronisés localement** car ils sont utilisés par plusieurs microservices. Cela améliore les performances en évitant des appels API répétés.

Si certains référentiels ne nécessitent pas de synchronisation locale (cas rares), ils peuvent être consultés uniquement via API, mais ce n'est pas la recommandation par défaut.

---

## 🔐 MICROSERVICE AUTHENTIFICATION

### ⚠️ Aucune table locale

L'authentification est **exclusivement gérée** par le microservice **authentification**. **Aucune table locale** n'est créée dans COService pour les utilisateurs, rôles ou permissions.

### Données utilisées depuis Auth Service

#### 1. Utilisateurs (Users)
- **Source** : Microservice **authentification** (via API)
- **Stockage dans COService** : 
  - `UserId` (string) stocké dans les entités (pas de FK)
  - Exemples : `CreePar`, `ModifiePar`, `VisaPar` dans les entités
- **Pas de table locale** : `Users` n'existe pas dans COService

#### 2. Rôles (Roles)
- **Source** : Microservice **authentification** (via API)
- **Utilisation** : Vérification des permissions pour les workflows
- **Pas de table locale** : `Roles` n'existe pas dans COService

#### 3. Permissions
- **Source** : Microservice **authentification** (via API)
- **Utilisation** : Vérification des permissions pour les actions
- **Pas de table locale** : `Permissions` n'existe pas dans COService

### Client API Authentification

**Interface** : `IAuthServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task<UserInfoDto> GetUserInfoAsync(string userId)`
- `Task<bool> VerifierRoleAsync(string userId, string role)`
- `Task<bool> VerifierPermissionAsync(string userId, string permission)`
- `Task<bool> VerifierMotDePasseAsync(string userId, string password)`
- `Task<List<string>> GetRolesAsync(string userId)`
- `Task<bool> VerifierOrganisationAsync(string userId, Guid organisationId)`

---

## 📋 RÉSUMÉ PAR CATÉGORIE

### Tables synchronisées depuis Enrolement (2 tables)
1. ✅ `Partenaires` - Synchronisée
2. ✅ `Exportateurs` - Synchronisée

### Table non nécessaire
- ❌ `ExportateursPartenaires` - **Non synchronisée** (pas nécessaire dans COService)

### Tables synchronisées depuis Référentiel Global (22+ tables)
1. ✅ `Departements` - Synchronisée localement
2. ✅ `Pays` - Synchronisée localement
3. ✅ `Ports` - Synchronisée localement
4. ✅ `Aeroports` - Synchronisée localement
5. ✅ `Fleuves` - Synchronisée localement
6. ✅ `RoutesNationales` - Synchronisée localement
7. ✅ `Corridors` - Synchronisée localement
8. ✅ `Troncons` - Synchronisée localement
9. ✅ `Sections` - Synchronisée localement
10. ✅ `Devises` - Synchronisée localement
11. ✅ `TauxDeChanges` - Synchronisée localement
12. ✅ `Incoterms` - Synchronisée localement
13. ✅ `BureauxDedouanements` - Synchronisée localement
14. ✅ `SectionsTariffaires` - Synchronisée localement
15. ✅ `ChapitresTariffaires` - Synchronisée localement
16. ✅ `DivisionsTariffaires` - Synchronisée localement
17. ✅ `CategoriesTariffaires` - Synchronisée localement
18. ✅ `PositionsTariffaires` - Synchronisée localement
19. ✅ `Modules` - Synchronisée localement
20. ✅ `TypeTransports` - Synchronisée localement
21. ✅ `UniteDeChargements` - Synchronisée localement
22. ✅ `UniteStatistiques` - Synchronisée localement

### Tables propres au CO (gérées localement)
1. ✅ `ZonesProductions` - Gérée localement (CRUD complet)

### Tables liées à Authentification
- ❌ **Aucune table locale**
- ✅ Utilisation uniquement via API
- ✅ Stockage de `UserId` (string) dans les entités (pas de FK)

---

## 🔄 MÉCANISMES DE SYNCHRONISATION

### Synchronisation Enrolement

**Service** : `EnrolementSyncService`  
**Fréquence** : Périodique (ex: toutes les heures) + À la demande

**Tables synchronisées** :
- `Partenaires`
- `Exportateurs`

**Table non synchronisée** :
- `ExportateursPartenaires` - Non nécessaire dans COService

**Champs de suivi** :
- `DerniereSynchronisation` (DateTime) dans chaque entité

### Synchronisation Référentiel

**Service** : `ReferentielSyncService`  
**Fréquence** : Périodique (ex: toutes les heures) + À la demande

**Tables synchronisées** :
- `Departements`
- `Pays`
- `Ports`
- `Aeroports`
- `Fleuves`
- `RoutesNationales`
- `Corridors`
- `Troncons`
- `Sections`
- `Devises`
- `TauxDeChanges`
- `Incoterms`
- `BureauxDedouanements`
- `SectionsTariffaires`
- `ChapitresTariffaires`
- `DivisionsTariffaires`
- `CategoriesTariffaires`
- `PositionsTariffaires`
- `Modules`
- `TypeTransports`
- `UniteDeChargements`
- `UniteStatistiques`
- Et autres référentiels partagés

**Champs de suivi** :
- Optionnel : `DerniereSynchronisation` si nécessaire

---

## 📝 TABLES LOCALES (Gérées par COService)

Ces tables sont **entièrement gérées** par COService, sans synchronisation externe :

### Tables métier principales
- ✅ `Certificats` - Gérée localement
- ✅ `LignesCertificats` - Gérée localement
- ✅ `ValidationsCertificats` - Gérée localement
- ✅ `Commentaires` - Gérée localement
- ✅ `Abonnements` - Gérée localement
- ✅ `TypesCertificats` - Gérée localement
- ✅ `DestinatairesProduits` - Gérée localement
- ✅ `DocumentsCertificats` - Gérée localement
- ✅ `DocumentsExportateurs` - Gérée localement
- ✅ `Logos` - Gérée localement
- ✅ `Facturations` - Gérée localement
- ✅ `PrixCertificats` - Gérée localement
- ✅ `PrixProduits` - Gérée localement
- ✅ `Produits` - Gérée localement
- ✅ `PortsCongo` - Gérée localement
- ✅ `VisasClient` - Gérée localement

### Tables de configuration
- ✅ `TypesPartenaires` - Gérée localement (peut être synchronisée si nécessaire)
- ✅ `StatutsCertificats` - Gérée localement
- ✅ `ZonesProductions` - Gérée localement (propre au CO)

---

## 🎯 RÈGLES IMPORTANTES

### ⚠️ Pas de FK vers Auth Service
- Les entités stockent `UserId` (string), pas de FK
- Exemples : `CreePar`, `ModifiePar`, `VisaPar` sont des strings
- Vérification des utilisateurs via API Auth Service

### ⚠️ Pas de création/modification directe
- **Partenaires** et **Exportateurs** : Lecture seule (synchronisés depuis enrolement)
- **Departements** et **ZonesProductions** : Lecture seule (synchronisés depuis référentiel)

### ⚠️ Consultation uniquement via API
- **Pays, Ports, Aéroports, etc.** : Consultation uniquement, pas de stockage local
- Utilisation de `IReferentielServiceClient` pour les appels API

---

**Document créé le** : 2025-01-XX  
**Version** : 1.0  
**Statut** : Documentation complète
