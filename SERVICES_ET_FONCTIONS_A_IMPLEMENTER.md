# 📋 SERVICES ET FONCTIONS À IMPLÉMENTER - COService

## Vue d'ensemble

Ce document liste tous les services, fonctions et endpoints qui doivent être implémentés dans le microservice COService, organisés par couche et par fonctionnalité.

---

## 🏗️ ARCHITECTURE DES SERVICES

### Couche Application (Services Métier)

#### 1. Services CRUD Standards

##### 1.1 CertificatOrigineService
**Interface** : `ICertificatOrigineService`  
**Localisation** : `COService.Application/Services/`

**Fonctions principales** :
- `Task<CertificatOrigineDto> CreerCertificatAsync(CreerCertificatOrigineDto dto)`
- `Task<CertificatOrigineDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<CertificatOrigineDto>> GetAllAsync()`
- `Task<IEnumerable<CertificatOrigineDto>> GetByExportateurAsync(Guid exportateurId)`
- `Task<IEnumerable<CertificatOrigineDto>> GetByPartenaireAsync(Guid partenaireId)`
- `Task<IEnumerable<CertificatOrigineDto>> GetByStatutAsync(StatutCertificat statut)`
- `Task<CertificatOrigineDto> ModifierCertificatAsync(Guid id, ModifierCertificatOrigineDto dto)`
- `Task<bool> SupprimerCertificatAsync(Guid id)`
- `Task<CertificatOrigineDto> SoumettreCertificatAsync(Guid id, string userId)`
- `Task<CertificatOrigineDto> GenererNumeroCertificatAsync(Guid id, Guid partenaireId)`

**Fonctions de workflow** :
- `Task<CertificatOrigineDto> ControleCertificatAsync(Guid id, string userId, string password, string? commentaire)`
- `Task<CertificatOrigineDto> ApprouverCertificatAsync(Guid id, string userId, string password)`
- `Task<CertificatOrigineDto> ValiderCertificatAsync(Guid id, string userId, string password)`
- `Task<CertificatOrigineDto> RejeterCertificatAsync(Guid id, string userId, string password, string commentaire)`
- `Task<CertificatOrigineDto> DemanderModificationAsync(Guid id, string userId, string commentaire)`

**Fonctions Formule A** : (Déléguées à FormuleAService)
- Les fonctions Formule A sont gérées par le service dédié `FormuleAService`

**Fonctions de recherche** :
- `Task<IEnumerable<CertificatOrigineDto>> RechercherAsync(string? numero, Guid? exportateurId, Guid? partenaireId, StatutCertificat? statut, DateTime? dateDebut, DateTime? dateFin)`

##### 1.2 CertificateLineService
**Interface** : `ICertificateLineService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CertificateLineDto> CreerLigneAsync(CreerCertificateLineDto dto)`
- `Task<CertificateLineDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<CertificateLineDto>> GetByCertificatIdAsync(Guid certificatId)`
- `Task<CertificateLineDto> ModifierLigneAsync(Guid id, ModifierCertificateLineDto dto)`
- `Task<bool> SupprimerLigneAsync(Guid id)`
- `Task<decimal> CalculerTotalValeurAsync(Guid certificatId)`
- `Task<decimal> CalculerTotalPoidsAsync(Guid certificatId)`
- `Task<decimal> CalculerTotalVolumeAsync(Guid certificatId)`

##### 1.3 AbonnementService
**Interface** : `IAbonnementService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<AbonnementDto> CreerAbonnementAsync(CreerAbonnementDto dto)`
- `Task<AbonnementDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<AbonnementDto>> GetAllAsync()`
- `Task<IEnumerable<AbonnementDto>> GetByExportateurAsync(Guid exportateurId)`
- `Task<IEnumerable<AbonnementDto>> GetByPartenaireAsync(Guid partenaireId)`
- `Task<AbonnementDto> ModifierAbonnementAsync(Guid id, ModifierAbonnementDto dto)`
- `Task<bool> SupprimerAbonnementAsync(Guid id)`
- `Task<string> GenererNumeroAbonnementAsync()`
- `Task<IEnumerable<CertificatOrigineDto>> GenererCertificatsDepuisAbonnementAsync(Guid abonnementId, int nombreCertificats)`
- `Task<int> GetNombreCertificatsDisponiblesAsync(Guid abonnementId)`
- `Task<int> GetNombreCertificatsUtilisesAsync(Guid abonnementId)`

##### 1.4 CommentaireService
**Interface** : `ICommentaireService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CommentaireDto> AjouterCommentaireAsync(CreerCommentaireDto dto)`
- `Task<CommentaireDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<CommentaireDto>> GetByCertificatIdAsync(Guid certificatId)`
- `Task<bool> SupprimerCommentaireAsync(Guid id)`

##### 1.5 CertificateTypeService
**Interface** : `ICertificateTypeService`  
**Localisation** : `COService.Application/Services/`  
**Statut** : ✅ Déjà implémenté

##### 1.6 PartenaireService
**Interface** : `IPartenaireService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** (Lecture seule - synchronisé depuis enrolement) :
- `Task<PartenaireDto?> GetByIdAsync(Guid id)`
- `Task<PartenaireDto?> GetByCodeAsync(string code)`
- `Task<IEnumerable<PartenaireDto>> GetAllAsync()`
- `Task<IEnumerable<PartenaireDto>> GetActifsAsync()`
- `Task<IEnumerable<PartenaireDto>> GetByTypeAsync(Guid typePartenaireId)`
- `Task<IEnumerable<PartenaireDto>> GetByDepartementAsync(Guid departementId)`
- `Task<bool> ForcerSynchronisationAsync()` - Force la synchronisation depuis enrolement

##### 1.7 ExportateurService
**Interface** : `IExportateurService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** (Lecture seule - synchronisé depuis enrolement) :
- `Task<ExportateurDto?> GetByIdAsync(Guid id)`
- `Task<ExportateurDto?> GetByCodeAsync(string code)`
- `Task<IEnumerable<ExportateurDto>> GetAllAsync()`
- `Task<IEnumerable<ExportateurDto>> GetActifsAsync()`
- `Task<IEnumerable<ExportateurDto>> GetByPartenaireAsync(Guid partenaireId)`
- `Task<IEnumerable<ExportateurDto>> GetByDepartementAsync(Guid departementId)`
- `Task<IEnumerable<ExportateurDto>> GetByTypeAsync(int typeExportateur)`
- `Task<bool> ForcerSynchronisationAsync()` - Force la synchronisation depuis enrolement

##### 1.8 DestinataireProduitService
**Interface** : `IDestinataireProduitService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<DestinataireProduitDto> CreerDestinataireAsync(CreerDestinataireProduitDto dto)`
- `Task<DestinataireProduitDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<DestinataireProduitDto>> GetAllAsync()`
- `Task<IEnumerable<DestinataireProduitDto>> GetByOrganisationAsync(Guid organisationId)`
- `Task<DestinataireProduitDto> ModifierDestinataireAsync(Guid id, ModifierDestinataireProduitDto dto)`
- `Task<bool> SupprimerDestinataireAsync(Guid id)`

##### 1.9 DocumentCertificatService
**Interface** : `IDocumentCertificatService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<DocumentCertificatDto> UploadDocumentAsync(Guid certificatId, IFormFile file, string typeDocument, string userId)`
- `Task<DocumentCertificatDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<DocumentCertificatDto>> GetByCertificatIdAsync(Guid certificatId)`
- `Task<FileStream> TelechargerDocumentAsync(Guid id)`
- `Task<bool> SupprimerDocumentAsync(Guid id)`

##### 1.10 DocumentExportateurService
**Interface** : `IDocumentExportateurService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<DocumentExportateurDto> UploadDocumentAsync(Guid exportateurId, IFormFile file, string typeDocument, string userId)`
- `Task<DocumentExportateurDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<DocumentExportateurDto>> GetByExportateurIdAsync(Guid exportateurId)`
- `Task<FileStream> TelechargerDocumentAsync(Guid id)`
- `Task<bool> SupprimerDocumentAsync(Guid id)`

##### 1.11 LogoService
**Interface** : `ILogoService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<LogoDto> UploadLogoAsync(Guid organisationId, string typeOrganisation, IFormFile file)`
- `Task<LogoDto?> GetByOrganisationAsync(Guid organisationId, string typeOrganisation)`
- `Task<FileStream> TelechargerLogoAsync(Guid id)`
- `Task<bool> SupprimerLogoAsync(Guid id)`

##### 1.12 FacturationService
**Interface** : `IFacturationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<FacturationDto> CreerFacturationAsync(Guid certificatId)`
- `Task<FacturationDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<FacturationDto>> GetAllAsync()`
- `Task<IEnumerable<FacturationDto>> GetByCertificatIdAsync(Guid certificatId)`
- `Task<IEnumerable<FacturationDto>> GetByExportateurIdAsync(Guid exportateurId)`
- `Task<IEnumerable<FacturationDto>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<FacturationDto> ModifierStatutFacturationAsync(Guid id, string nouveauStatut)`
- `Task<FacturationDto> MarquerCommePayeAsync(Guid id, DateTime datePaiement)`
- `Task<decimal> CalculerMontantFacturationAsync(Guid certificatId, Guid partenaireId)`
- `Task<FileStream> GenererPDFFactureAsync(Guid id)`

##### 1.13 PrixCertificatService
**Interface** : `IPrixCertificatService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<PrixCertificatDto> DefinirPrixAsync(CreerPrixCertificatDto dto)`
- `Task<PrixCertificatDto?> GetByIdAsync(Guid id)`
- `Task<PrixCertificatDto?> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<PrixCertificatDto> ModifierPrixAsync(Guid id, decimal nouveauPrix)`
- `Task<decimal> GetPrixParPartenaireAsync(Guid partenaireId)`

##### 1.14 ProduitService
**Interface** : `IProduitService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<ProduitDto> CreerProduitAsync(CreerProduitDto dto)`
- `Task<ProduitDto?> GetByIdAsync(Guid id)`
- `Task<ProduitDto?> GetByCodeAsync(string code)`
- `Task<IEnumerable<ProduitDto>> GetAllAsync()`
- `Task<IEnumerable<ProduitDto>> GetByOrganisationAsync(Guid organisationId)`
- `Task<ProduitDto> ModifierProduitAsync(Guid id, ModifierProduitDto dto)`
- `Task<bool> SupprimerProduitAsync(Guid id)`

##### 1.15 PrixProduitService
**Interface** : `IPrixProduitService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<PrixProduitDto> DefinirPrixAsync(CreerPrixProduitDto dto)`
- `Task<PrixProduitDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<PrixProduitDto>> GetByProduitIdAsync(Guid produitId)`
- `Task<IEnumerable<PrixProduitDto>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<PrixProduitDto> ModifierPrixAsync(Guid id, decimal nouveauPrix)`
- `Task<decimal> GetPrixParProduitEtPartenaireAsync(Guid produitId, Guid partenaireId)`

#### 2. Services de Workflow et Validation

##### 2.1 ValidationCertificatService
**Interface** : `IValidationCertificatService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CertificateValidationDto> EnregistrerValidationAsync(CreerValidationDto dto)`
- `Task<CertificateValidationDto?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<CertificateValidationDto>> GetByCertificatIdAsync(Guid certificatId)`
- `Task<IEnumerable<CertificateValidationDto>> GetHistoriqueValidationAsync(Guid certificatId)`
- `Task<bool> VerifierTransitionValideAsync(StatutCertificat statutActuel, StatutCertificat nouveauStatut, string roleUtilisateur)`
- `Task<bool> VerifierPermissionValidationAsync(string userId, string role, Guid certificatId, StatutCertificat nouveauStatut)`
- `Task<bool> VerifierMotDePasseAsync(string userId, string password)`
- `Task<bool> VerifierOrganisationAsync(string userId, Guid certificatId, string role)`

##### 2.2 WorkflowService
**Interface** : `IWorkflowService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CertificatOrigineDto> ExecuterTransitionAsync(Guid certificatId, StatutCertificat nouveauStatut, string userId, string password, string? commentaire)`
- `Task<IEnumerable<StatutCertificat>> GetTransitionsPossiblesAsync(Guid certificatId, string userId)`
- `Task<bool> PeutControleAsync(string userId, Guid certificatId)` - Vérifie rôles 3 ou 4
- `Task<bool> PeutApprouverAsync(string userId, Guid certificatId)` - Vérifie rôles 3 ou 4
- `Task<bool> PeutValiderAsync(string userId, Guid certificatId)` - Vérifie rôle 6 + même organisation
- `Task<bool> PeutRejeterAsync(string userId, Guid certificatId)` - Vérifie rôles 3, 4 ou 6 selon statut
- `Task<bool> PeutDemanderModificationAsync(string userId, Guid certificatId)` - Exportateur, certificat validé
- `Task<Dictionary<string, bool>> GetPermissionsWorkflowAsync(string userId, Guid certificatId)`

**Règles de workflow CO** :
- Transitions : 1 (Élaboré) → 2 (Soumis) → 4 (Contrôlé) → 7 (Approuvé) → 8 (Validé)
- Rejets possibles : 2→5, 4→5, 7→5 (avec commentaire obligatoire)
- Modification : 8→10 (Modification) → 7 (Approuvé) → 8 (Validé)
- Vérifications : Mot de passe obligatoire pour toutes transitions, rôle et organisation

##### 2.3 FormuleAService
**Interface** : `IFormuleAService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CertificatOrigineDto> CreerFormuleAAsync(Guid certificatOrigineId, string userId, string password)`
- `Task<CertificatOrigineDto> ControleFormuleAAsync(Guid id, string userId, string password)`
- `Task<CertificatOrigineDto> ApprouverFormuleAAsync(Guid id, string userId, string password)`
- `Task<CertificatOrigineDto> ValiderFormuleAAsync(Guid id, string userId, string password)`
- `Task<CertificatOrigineDto> RejeterFormuleAAsync(Guid id, string userId, string password, string commentaire)`

**Validations Formule A** :
- `Task<bool> PeutCreerFormuleAAsync(Guid certificatId, string userId)` - Vérifie :
  - CO est validé (statut 8)
  - CO appartient à une chambre autorisée (vérifie type partenaire depuis DB, pas hardcodé)
  - Exportateur type 3 OU propriétaire du CO
- `Task<bool> VerifierAutorisationFormuleAAsync(Guid certificatId, Guid exportateurId)` - Vérifie type exportateur et propriété
- `Task<bool> VerifierChambreAutoriseeFormuleAAsync(Guid partenaireId)` - Vérifie type partenaire depuis DB

**Règles de workflow Formule A** :
- Transitions : 12 (Formule A soumise) → 13 (Formule A contrôlée) → 14 (Formule A approuvée) → 15 (Formule A validée)
- Rejets possibles : 12→5, 13→5, 14→5 (avec commentaire obligatoire)
- Vérifications : Mot de passe obligatoire, rôles 3/4 pour contrôle/approbation, rôle 6 + même organisation pour validation

#### 3. Services de Génération

##### 3.1 PDFGenerationService
**Interface** : `IPDFGenerationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<byte[]> GenererPDFCertificatOrigineAsync(Guid certificatId)` - CO standard (formule='CO')
- `Task<byte[]> GenererPDFCertificatOuessoAsync(Guid certificatId)` - CO Ouesso (selon type partenaire)
- `Task<byte[]> GenererPDFFormuleAAsync(Guid certificatId)` - Formule A (is_formule_a=true, statut 15)
- `Task<byte[]> GenererPDFEUR1Async(Guid certificatId)` - EUR.1 (formule='EUR-1')
- `Task<byte[]> GenererPDFALCAsync(Guid certificatId)` - ALC (formule='CO+ALC')
- `Task<byte[]> GenererPDFFormuleACargoAsync(Guid certificatId)` - CO+Formule A cargo (formule='B')
- `Task<string> GenererQRCodeAsync(Guid certificatId)`
- `Task<byte[]> AjouterSignatureAsync(byte[] pdfBytes, string userId)`
- `Task<byte[]> GenererPDFParTypeAsync(Guid certificatId)` - Détecte automatiquement le type selon formule et is_formule_a

**Logique de sélection PDF** :
- Détecte automatiquement le type de certificat selon :
  - `is_formule_a` = true → Formule A
  - `formule` = 'CO' → CO standard
  - `formule` = 'EUR-1' → EUR.1
  - `formule` = 'CO+ALC' → ALC
  - `formule` = 'B' → CO+Formule A cargo
  - Type de partenaire (depuis DB) pour template spécifique

##### 3.2 NumeroGenerationService
**Interface** : `INumeroGenerationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<string> GenererNumeroCertificatAsync(Guid partenaireId, Guid certificatId)` - Format: CO{Numéro}{Date}{CodeDépartement}
- `Task<string> GenererNumeroAbonnementAsync()` - Format: {Année}{Mois}{Jour}{Heure}{Minute}{Seconde}{Lettre}
- `Task<string> GenererNumeroFactureAsync(Guid partenaireId)`
- `Task<string> GetCodeDepartementPartenaireAsync(Guid partenaireId)` - Récupère depuis DB (partenaire → département → code)
- `Task<string> GetDernierNumeroPourDateAsync(Guid partenaireId, DateTime date)` - Recherche dernier numéro par date et partenaire
- `Task<int> ExtraireNumeroSequencielAsync(string numeroCertificat)` - Extrait le numéro séquentiel
- `Task<string> FormaterDatePourNumeroAsync(DateTime date)` - Format ddmmyy

**Algorithme génération numéro certificat** :
1. Récupérer le partenaire (depuis DB)
2. Récupérer le département du partenaire (depuis DB)
3. Récupérer le code département (depuis DB, pas hardcodé)
4. Formater la date (ddmmyy)
5. Rechercher le dernier numéro pour ce partenaire et cette date
6. Extraire le numéro séquentiel
7. Incrémenter
8. Construire : `CO{Numéro}{Date}{CodeDépartement}`

#### 4. Services de Synchronisation

##### 4.1 EnrolementSyncService
**Interface** : `IEnrolementSyncService`  
**Localisation** : `COService.Infrastructure/Services/`

**Fonctions** :
- `Task SynchroniserPartenairesAsync()`
- `Task SynchroniserExportateursAsync()`
- `Task SynchroniserPartenaireAsync(Guid partenaireId)`
- `Task SynchroniserExportateurAsync(Guid exportateurId)`
- `Task SynchroniserToutAsync()`
- `Task<DateTime> GetDerniereSynchronisationAsync()`

**Implémentation** : Peut être un `IHostedService` pour synchronisation périodique

##### 4.2 ReferentielSyncService
**Interface** : `IReferentielSyncService`  
**Localisation** : `COService.Infrastructure/Services/`

**Fonctions** :
- `Task SynchroniserDepartementsAsync()`
- `Task SynchroniserZonesProductionAsync()`
- `Task SynchroniserToutAsync()`

#### 5. Services de Notification

##### 5.1 NotificationService
**Interface** : `INotificationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task EnvoyerNotificationChangementStatutAsync(Guid certificatId, StatutCertificat ancienStatut, StatutCertificat nouveauStatut)`
- `Task EnvoyerNotificationRejetAsync(Guid certificatId, string commentaire)` - Email avec motif du rejet
- `Task EnvoyerNotificationValidationAsync(Guid certificatId)` - Email avec lien génération PDF
- `Task EnvoyerNotificationSoumissionAsync(Guid certificatId)` - Notification chambre + email exportateur
- `Task EnvoyerNotificationControleAsync(Guid certificatId, bool approuve)` - Si approuvé, notification exportateur
- `Task EnvoyerNotificationApprobationAsync(Guid certificatId)` - Notification Président + email exportateur
- `Task EnvoyerNotificationFormuleAAsync(Guid certificatId, StatutCertificat statut)` - Notifications spécifiques Formule A
- `Task EnvoyerEmailAsync(string destinataire, string sujet, string corps)`

---

## 🔌 COUCHE INFRASTRUCTURE (Clients Externes)

### Clients API Microservices

#### 1. EnrolementServiceClient
**Interface** : `IEnrolementServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task<PartenaireDto> GetPartenaireAsync(Guid id)`
- `Task<List<PartenaireDto>> GetAllPartenairesAsync()`
- `Task<PartenaireDto?> GetPartenaireByCodeAsync(string code)`
- `Task<ExportateurDto> GetExportateurAsync(Guid id)`
- `Task<List<ExportateurDto>> GetAllExportateursAsync()`
- `Task<ExportateurDto?> GetExportateurByCodeAsync(string code)`
- `Task<List<ExportateurDto>> GetExportateursByPartenaireAsync(Guid partenaireId)`

**Implémentation** : Utilise Refit ou HttpClient

#### 2. ReferentielServiceClient
**Interface** : `IReferentielServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task<PaysDto> GetPaysAsync(string code)`
- `Task<List<PaysDto>> GetAllPaysAsync()`
- `Task<PortDto> GetPortAsync(Guid id)`
- `Task<List<PortDto>> GetPortsByPaysAsync(string codePays)`
- `Task<AeroportDto> GetAeroportAsync(Guid id)`
- `Task<List<AeroportDto>> GetAeroportsByPaysAsync(string codePays)`
- `Task<DepartementDto> GetDepartementAsync(Guid id)`
- `Task<List<DepartementDto>> GetAllDepartementsAsync()`
- `Task<ZoneProductionDto> GetZoneProductionAsync(Guid id)`
- `Task<List<ZoneProductionDto>> GetZonesProductionByPartenaireAsync(Guid partenaireId)`

**Implémentation** : Utilise Refit ou HttpClient

#### 3. AuthServiceClient
**Interface** : `IAuthServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task<UserInfoDto> GetUserInfoAsync(string userId)`
- `Task<bool> VerifierRoleAsync(string userId, string role)`
- `Task<bool> VerifierPermissionAsync(string userId, string permission)`
- `Task<bool> VerifierMotDePasseAsync(string userId, string password)`
- `Task<List<string>> GetRolesAsync(string userId)`
- `Task<bool> VerifierOrganisationAsync(string userId, Guid organisationId)`

**Implémentation** : Utilise Refit ou HttpClient

#### 4. NotificationServiceClient
**Interface** : `INotificationServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task EnvoyerEmailAsync(EnvoyerEmailDto dto)`
- `Task EnvoyerNotificationAsync(EnvoyerNotificationDto dto)`

**Implémentation** : Utilise Refit ou HttpClient

#### 5. DocumentServiceClient
**Interface** : `IDocumentServiceClient`  
**Localisation** : `COService.Infrastructure/Clients/`

**Fonctions** :
- `Task<DocumentDto> UploadDocumentAsync(IFormFile file, string type)`
- `Task<FileStream> TelechargerDocumentAsync(Guid documentId)`
- `Task<bool> SupprimerDocumentAsync(Guid documentId)`

**Implémentation** : Utilise Refit ou HttpClient

---

## 📦 REPOSITORIES (Couche Infrastructure)

### Repositories Standards

Chaque entité aura son repository avec les méthodes CRUD de base :

#### Pattern Standard pour chaque Repository :
- `Task<T?> GetByIdAsync(Guid id)`
- `Task<IEnumerable<T>> GetAllAsync()`
- `Task<T> AddAsync(T entity)`
- `Task UpdateAsync(T entity)`
- `Task DeleteAsync(T entity)`
- `Task<bool> ExistsAsync(Guid id)`

#### Repositories Spécifiques :

##### CertificatOrigineRepository
- `Task<CertificatOrigine?> GetByNumeroAsync(string numero)`
- `Task<IEnumerable<CertificatOrigine>> GetByExportateurIdAsync(Guid exportateurId)`
- `Task<IEnumerable<CertificatOrigine>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<IEnumerable<CertificatOrigine>> GetByStatutAsync(StatutCertificat statut)`
- `Task<IEnumerable<CertificatOrigine>> RechercherAsync(string? numero, Guid? exportateurId, Guid? partenaireId, StatutCertificat? statut, DateTime? dateDebut, DateTime? dateFin)`
- `Task<string?> GetDernierNumeroAsync(Guid partenaireId)`

##### AbonnementRepository
- `Task<Abonnement?> GetByNumeroAsync(string numero)`
- `Task<IEnumerable<Abonnement>> GetByExportateurIdAsync(Guid exportateurId)`
- `Task<IEnumerable<Abonnement>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<int> GetNombreCertificatsDisponiblesAsync(Guid abonnementId)`
- `Task<int> GetNombreCertificatsUtilisesAsync(Guid abonnementId)`

##### PartenaireRepository
- `Task<Partenaire?> GetByCodeAsync(string code)`
- `Task<IEnumerable<Partenaire>> GetActifsAsync()`
- `Task<IEnumerable<Partenaire>> GetByTypeAsync(Guid typePartenaireId)`
- `Task<IEnumerable<Partenaire>> GetByDepartementAsync(Guid departementId)`

##### ExportateurRepository
- `Task<Exportateur?> GetByCodeAsync(string code)`
- `Task<IEnumerable<Exportateur>> GetActifsAsync()`
- `Task<IEnumerable<Exportateur>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<IEnumerable<Exportateur>> GetByTypeAsync(int typeExportateur)`

##### FacturationRepository
- `Task<Facturation?> GetByCertificatIdAsync(Guid certificatId)`
- `Task<IEnumerable<Facturation>> GetByExportateurIdAsync(Guid exportateurId)`
- `Task<IEnumerable<Facturation>> GetByPartenaireIdAsync(Guid partenaireId)`
- `Task<Facturation?> GetByNumeroFactureAsync(string numeroFacture)`

---

## 🌐 ENDPOINTS API (Couche API)

### Endpoints par Entité

#### CertificatsOrigineEndpoints
- `GET /api/certificats` - Liste tous les certificats
- `GET /api/certificats/{id}` - Détail d'un certificat
- `GET /api/certificats/numero/{numero}` - Recherche par numéro
- `GET /api/certificats/exportateur/{exportateurId}` - Par exportateur
- `GET /api/certificats/partenaire/{partenaireId}` - Par partenaire
- `GET /api/certificats/statut/{statut}` - Par statut
- `GET /api/certificats/recherche` - Recherche avancée (query params)
- `POST /api/certificats` - Créer un certificat
- `PUT /api/certificats/{id}` - Modifier un certificat
- `DELETE /api/certificats/{id}` - Supprimer un certificat
- `POST /api/certificats/{id}/soumettre` - Soumettre un certificat
- `POST /api/certificats/{id}/controle` - Contrôler un certificat
- `POST /api/certificats/{id}/approuver` - Approuver un certificat
- `POST /api/certificats/{id}/valider` - Valider un certificat
- `POST /api/certificats/{id}/rejeter` - Rejeter un certificat
- `POST /api/certificats/{id}/demander-modification` - Demander modification
- `POST /api/certificats/{id}/generer-numero` - Générer le numéro
- `POST /api/certificats/{id}/formule-a` - Créer une Formule A (vérifie prérequis)

#### LignesCertificatsEndpoints
- `GET /api/certificats/{certificatId}/lignes` - Lignes d'un certificat
- `GET /api/lignes-certificats/{id}` - Détail d'une ligne
- `POST /api/lignes-certificats` - Créer une ligne
- `PUT /api/lignes-certificats/{id}` - Modifier une ligne
- `DELETE /api/lignes-certificats/{id}` - Supprimer une ligne
- `GET /api/certificats/{certificatId}/lignes/totaux` - Calculer les totaux

#### AbonnementsEndpoints
- `GET /api/abonnements` - Liste tous les abonnements
- `GET /api/abonnements/{id}` - Détail d'un abonnement
- `GET /api/abonnements/exportateur/{exportateurId}` - Par exportateur
- `GET /api/abonnements/partenaire/{partenaireId}` - Par partenaire
- `POST /api/abonnements` - Créer un abonnement
- `PUT /api/abonnements/{id}` - Modifier un abonnement
- `DELETE /api/abonnements/{id}` - Supprimer un abonnement
- `POST /api/abonnements/{id}/generer-certificats` - Générer certificats depuis abonnement
- `GET /api/abonnements/{id}/statistiques` - Statistiques (disponibles/utilisés)

#### PartenairesEndpoints (Lecture seule)
- `GET /api/partenaires` - Liste tous les partenaires
- `GET /api/partenaires/{id}` - Détail d'un partenaire
- `GET /api/partenaires/code/{code}` - Recherche par code
- `GET /api/partenaires/actifs` - Partenaires actifs
- `GET /api/partenaires/type/{typeId}` - Par type
- `GET /api/partenaires/departement/{departementId}` - Par département
- `POST /api/sync/enrolement/partenaires` - Forcer synchronisation (admin)

#### ExportateursEndpoints (Lecture seule)
- `GET /api/exportateurs` - Liste tous les exportateurs
- `GET /api/exportateurs/{id}` - Détail d'un exportateur
- `GET /api/exportateurs/code/{code}` - Recherche par code
- `GET /api/exportateurs/actifs` - Exportateurs actifs
- `GET /api/exportateurs/partenaire/{partenaireId}` - Par partenaire
- `GET /api/exportateurs/type/{type}` - Par type
- `POST /api/sync/enrolement/exportateurs` - Forcer synchronisation (admin)

#### DestinatairesProduitsEndpoints
- `GET /api/destinataires-produits` - Liste tous les destinataires
- `GET /api/destinataires-produits/{id}` - Détail d'un destinataire
- `GET /api/destinataires-produits/organisation/{organisationId}` - Par organisation
- `POST /api/destinataires-produits` - Créer un destinataire
- `PUT /api/destinataires-produits/{id}` - Modifier un destinataire
- `DELETE /api/destinataires-produits/{id}` - Supprimer un destinataire

#### DocumentsCertificatsEndpoints
- `GET /api/certificats/{certificatId}/documents` - Documents d'un certificat
- `GET /api/documents-certificats/{id}` - Détail d'un document
- `POST /api/certificats/{certificatId}/documents` - Upload un document
- `GET /api/documents-certificats/{id}/telecharger` - Télécharger un document
- `DELETE /api/documents-certificats/{id}` - Supprimer un document

#### DocumentsExportateursEndpoints
- `GET /api/exportateurs/{exportateurId}/documents` - Documents d'un exportateur
- `GET /api/documents-exportateurs/{id}` - Détail d'un document
- `POST /api/exportateurs/{exportateurId}/documents` - Upload un document
- `GET /api/documents-exportateurs/{id}/telecharger` - Télécharger un document
- `DELETE /api/documents-exportateurs/{id}` - Supprimer un document

#### FacturationsEndpoints
- `GET /api/facturations` - Liste toutes les facturations
- `GET /api/facturations/{id}` - Détail d'une facturation
- `GET /api/facturations/certificat/{certificatId}` - Par certificat
- `GET /api/facturations/exportateur/{exportateurId}` - Par exportateur
- `GET /api/facturations/partenaire/{partenaireId}` - Par partenaire
- `POST /api/certificats/{certificatId}/facturation` - Créer une facturation
- `PUT /api/facturations/{id}/statut` - Modifier le statut
- `POST /api/facturations/{id}/marquer-paye` - Marquer comme payé
- `GET /api/facturations/{id}/pdf` - Générer PDF facture

#### PDFEndpoints
- `GET /api/certificats/{id}/pdf` - Générer PDF (détection automatique du type)
- `GET /api/certificats/{id}/pdf/co` - Générer PDF CO standard
- `GET /api/certificats/{id}/pdf/ouesso` - Générer PDF CO Ouesso (selon type partenaire)
- `GET /api/certificats/{id}/pdf/formule-a` - Générer PDF Formule A (statut 15 requis)
- `GET /api/certificats/{id}/pdf/eur1` - Générer PDF EUR.1 (formule='EUR-1')
- `GET /api/certificats/{id}/pdf/alc` - Générer PDF ALC (formule='CO+ALC')
- `GET /api/certificats/{id}/pdf/formule-a-cargo` - Générer PDF CO+Formule A cargo (formule='B')

#### CommentairesEndpoints
- `GET /api/certificats/{certificatId}/commentaires` - Commentaires d'un certificat
- `GET /api/commentaires/{id}` - Détail d'un commentaire
- `POST /api/commentaires` - Ajouter un commentaire
- `DELETE /api/commentaires/{id}` - Supprimer un commentaire

#### ValidationsCertificatsEndpoints
- `GET /api/certificats/{certificatId}/validations` - Historique des validations
- `GET /api/validations-certificats/{id}` - Détail d'une validation
- `GET /api/certificats/{certificatId}/validations/transitions-possibles` - Transitions possibles selon rôle
- `GET /api/certificats/{certificatId}/validations/permissions` - Permissions workflow (controle, approbation, validation, rejet)

#### FormuleAEndpoints
- `POST /api/certificats/{id}/formule-a` - Créer Formule A depuis CO validé
- `POST /api/certificats/{id}/formule-a/controle` - Contrôler Formule A (12→13)
- `POST /api/certificats/{id}/formule-a/approuver` - Approuver Formule A (13→14)
- `POST /api/certificats/{id}/formule-a/valider` - Valider Formule A (14→15)
- `POST /api/certificats/{id}/formule-a/rejeter` - Rejeter Formule A (→5)
- `GET /api/certificats/{id}/formule-a/peut-creer` - Vérifier si peut créer Formule A

#### SynchronisationEndpoints
- `POST /api/sync/enrolement` - Synchroniser toutes les organisations
- `POST /api/sync/enrolement/partenaires` - Synchroniser partenaires
- `POST /api/sync/enrolement/exportateurs` - Synchroniser exportateurs
- `POST /api/sync/referentiel` - Synchroniser référentiels locaux
- `GET /api/sync/derniere-synchronisation` - Dernière synchronisation

#### StatistiquesEndpoints
- `GET /api/statistiques/certificats` - Statistiques certificats
- `GET /api/statistiques/certificats/par-statut` - Par statut
- `GET /api/statistiques/certificats/par-exportateur` - Par exportateur
- `GET /api/statistiques/certificats/par-partenaire` - Par partenaire
- `GET /api/statistiques/facturations` - Statistiques facturations
- `GET /api/statistiques/abonnements` - Statistiques abonnements

---

## 🔐 RÈGLES MÉTIER ET VALIDATIONS

### ⚠️ IMPORTANT : Pas de valeurs hardcodées

Toutes les validations doivent être basées sur les données persistées dans la base de données, pas sur des valeurs hardcodées.

### Validations par Type de Certificat

#### Types de Certificats par Formule
- **CO** : `formule = 'CO'` - Certificat d'Origine standard
- **CO + Formule A cargo** : `formule = 'B'` - Pour cargaisons communes (Pointe-Noire)
- **EUR-1** : `formule = 'EUR-1'` - Pour Union Européenne (Ouesso uniquement)
- **CO + ALC** : `formule = 'CO+ALC'` - Pour pays du Maghreb (Ouesso uniquement)
- **Formule A** : `is_formule_a = true` - Workflow spécifique (Ouesso uniquement)

#### Validation Formule A
**Prérequis** (vérifiés depuis DB) :
1. CO doit être validé (statut 8)
2. CO doit appartenir à une chambre autorisée :
   - Vérifier `TypePartenaire` du partenaire du CO (depuis DB)
   - Type doit permettre Formule A (pas hardcoder partner_id=3)
3. Exportateur autorisé :
   - `TypeExportateur = 3` (depuis DB), OU
   - Exportateur est propriétaire du CO (`ExportateurId = User.OrganisationId`)
4. Vérification mot de passe exportateur

### Validations Workflow

#### Transitions CO Standard
- **1 → 2** : Exportateur soumet (vérifie champs obligatoires)
- **2 → 4** : Contrôleur/Superviseur (rôles 3 ou 4 depuis Auth Service)
- **2 → 5** : Rejet (commentaire obligatoire)
- **4 → 7** : Contrôleur/Superviseur approuve (rôles 3 ou 4)
- **4 → 5** : Rejet (commentaire obligatoire)
- **7 → 8** : Président valide (rôle 6 depuis Auth Service + même organisation depuis DB)
- **7 → 5** : Rejet (commentaire obligatoire)
- **8 → 10** : Demande modification (exportateur)
- **10 → 7** : Approbation modification (rôles 3 ou 4)
- **10 → 5** : Rejet modification (commentaire obligatoire)

#### Transitions Formule A
- **12 → 13** : Contrôleur/Superviseur contrôle (rôles 3 ou 4)
- **12 → 5** : Rejet (commentaire obligatoire)
- **13 → 14** : Contrôleur/Superviseur approuve (rôles 3 ou 4)
- **13 → 5** : Rejet (commentaire obligatoire)
- **14 → 15** : Président valide (rôle 6 + même organisation depuis DB)
- **14 → 5** : Rejet (commentaire obligatoire)

### Validations Organisation

**Vérification même organisation** :
- Récupérer `PartenaireId` du certificat (depuis DB)
- Récupérer `OrganisationId` de l'utilisateur (depuis Auth Service)
- Comparer : `Certificat.PartenaireId == User.OrganisationId`

**Vérification type partenaire** :
- Récupérer `TypePartenaireId` du partenaire (depuis DB)
- Vérifier les permissions selon le type (depuis DB, pas hardcodé)

### Génération Numéros

**Format** : `CO{Numéro}{Date}{CodeDépartement}`

**Processus** :
1. Récupérer le partenaire (depuis DB)
2. Récupérer le département du partenaire (depuis DB : `Partenaire.DepartementId`)
3. Récupérer le code département (depuis DB : `Departement.Code`)
4. Formater date (ddmmyy)
5. Rechercher dernier numéro pour ce partenaire et cette date (depuis DB)
6. Extraire et incrémenter numéro séquentiel
7. Construire le numéro final

**Pas de hardcoding** :
- ❌ Ne pas hardcoder "PNR" ou "OUE"
- ✅ Récupérer depuis `Partenaire → Departement → Code`

---

## 🔧 SERVICES UTILITAIRES

### Services d'Infrastructure

#### FileStorageService
**Interface** : `IFileStorageService`  
**Localisation** : `COService.Infrastructure/Services/`

**Fonctions** :
- `Task<string> SauvegarderFichierAsync(IFormFile file, string dossier)`
- `Task<FileStream> LireFichierAsync(string chemin)`
- `Task SupprimerFichierAsync(string chemin)`
- `Task<bool> FichierExisteAsync(string chemin)`

#### QRCodeService
**Interface** : `IQRCodeService`  
**Localisation** : `COService.Infrastructure/Services/`

**Fonctions** :
- `Task<byte[]> GenererQRCodeAsync(string contenu)`
- `Task<string> GenererQRCodeBase64Async(string contenu)`

#### ServiceDiscovery
**Interface** : `IServiceDiscovery`  
**Localisation** : `COService.Infrastructure/Services/`  
**Statut** : ✅ Déjà implémenté

**Fonctions** :
- `Task<string?> DiscoverServiceAsync(string serviceName)`
- `Task<List<string>> DiscoverServiceInstancesAsync(string serviceName)`

---

## 📊 RÉSUMÉ PAR CATÉGORIE

### Services Métier (Application) : ~26 services
- Services CRUD : 15 services
- Services Workflow : 3 services (WorkflowService, ValidationCertificatService, FormuleAService)
- Services Génération : 2 services
- Services Synchronisation : 2 services
- Services Notification : 1 service

### Clients Externes (Infrastructure) : 5 clients
- EnrolementServiceClient
- ReferentielServiceClient
- AuthServiceClient
- NotificationServiceClient
- DocumentServiceClient

### Repositories (Infrastructure) : ~17 repositories
- Un repository par entité avec méthodes CRUD + méthodes spécifiques

### Endpoints API : ~80+ endpoints
- Répartis sur ~15 groupes d'endpoints

### Services Utilitaires : 3 services
- FileStorageService
- QRCodeService
- ServiceDiscovery (déjà implémenté)

---

**Document créé le** : 2025-01-XX  
**Version** : 1.0  
**Statut** : Planification complète
