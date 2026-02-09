# 🏗️ ARCHITECTURE FLEXIBLE DES WORKFLOWS PAR CHAMBRE DE COMMERCE

## 🎯 Objectif

Concevoir une architecture flexible permettant de gérer **plusieurs chambres de commerce**, chacune avec son **propre workflow configurable**, sans hardcoding.

---

## 📊 Problématique

- ❌ **Pas de hardcoding** : Ne pas coder en dur les workflows par chambre
- ✅ **Flexibilité** : Chaque chambre peut avoir son workflow unique
- ✅ **Extensibilité** : Facile d'ajouter de nouvelles chambres
- ✅ **Configuration** : Workflows configurables depuis la base de données

---

## 🏛️ Architecture Proposée

### 1. Entités de Configuration

#### 1.1 WorkflowConfiguration (Configuration de Workflow)
**Entité** : `WorkflowConfiguration`  
**Table** : `WorkflowsConfigurations`

**Champs** :
- `Id` (Guid)
- `TypePartenaireId` (Guid, FK vers `TypesPartenaires`) - Type de chambre de commerce
- `NomWorkflow` (string) - Ex: "Workflow CO Standard", "Workflow Formule A"
- `TypeCertificat` (string) - Ex: "CO", "FormuleA", "EUR1", "ALC"
- `EstActif` (bool)
- Champs d'audit

**Relations** :
- `BelongsTo` : TypePartenaire
- `HasMany` : EtapesWorkflow

#### 1.2 EtapeWorkflow (Étape d'un Workflow)
**Entité** : `EtapeWorkflow`  
**Table** : `EtapesWorkflow`

**Champs** :
- `Id` (Guid)
- `WorkflowConfigurationId` (Guid, FK)
- `Ordre` (int) - Ordre dans le workflow (1, 2, 3, ...)
- `StatutSource` (int) - Statut de départ (ex: 1 pour "Élaboré")
- `StatutCible` (int) - Statut d'arrivée (ex: 2 pour "Soumis")
- `LibelleStatutSource` (string) - Ex: "Élaboré"
- `LibelleStatutCible` (string) - Ex: "Soumis"
- `RolesAutorises` (string) - JSON array des rôles autorisés : `["3", "4"]` ou `["6"]`
- `RequiertMotDePasse` (bool) - Vérification mot de passe obligatoire
- `RequiertCommentaire` (bool) - Commentaire obligatoire (pour rejets)
- `RequiertMemeOrganisation` (bool) - Vérification même organisation (pour Président)
- `PeutRejeter` (bool) - Permet le rejet depuis cet état
- `EstRejet` (bool) - Cette transition est un rejet (→ statut 5)
- Champs d'audit

**Relations** :
- `BelongsTo` : WorkflowConfiguration

#### 1.3 TypeCertificatAutorise (Types de Certificats par Chambre)
**Entité** : `TypeCertificatAutorise`  
**Table** : `TypesCertificatsAutorises`

**Champs** :
- `Id` (Guid)
- `TypePartenaireId` (Guid, FK vers `TypesPartenaires`)
- `CodeFormule` (string) - Ex: "CO", "B", "EUR-1", "CO+ALC"
- `Libelle` (string) - Ex: "Certificat d'Origine", "EUR.1"
- `EstFormuleA` (bool) - Indique si c'est une Formule A (is_formule_a = true)
- `EstActif` (bool)
- Champs d'audit

**Relations** :
- `BelongsTo` : TypePartenaire

---

## 🔄 Exemple de Configuration

### Exemple 1 : Chambre de Commerce Standard (Type 1)

**WorkflowConfiguration** :
- `TypePartenaireId` : Type 1
- `NomWorkflow` : "Workflow CO Standard"
- `TypeCertificat` : "CO"

**EtapesWorkflow** :
1. Ordre 1 : `1 (Élaboré) → 2 (Soumis)` - Rôles: Exportateur, RequiertMotDePasse: false
2. Ordre 2 : `2 (Soumis) → 4 (Contrôlé)` - Rôles: ["3", "4"], RequiertMotDePasse: true
3. Ordre 3 : `2 (Soumis) → 5 (Rejeté)` - Rôles: ["3", "4"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true
4. Ordre 4 : `4 (Contrôlé) → 7 (Approuvé)` - Rôles: ["3", "4"], RequiertMotDePasse: true
5. Ordre 5 : `4 (Contrôlé) → 5 (Rejeté)` - Rôles: ["3", "4"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true
6. Ordre 6 : `7 (Approuvé) → 8 (Validé)` - Rôles: ["6"], RequiertMotDePasse: true, RequiertMemeOrganisation: true
7. Ordre 7 : `7 (Approuvé) → 5 (Rejeté)` - Rôles: ["6"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true
8. Ordre 8 : `8 (Validé) → 10 (Modification)` - Rôles: Exportateur, RequiertMotDePasse: false
9. Ordre 9 : `10 (Modification) → 7 (Approuvé)` - Rôles: ["3", "4"], RequiertMotDePasse: true
10. Ordre 10 : `10 (Modification) → 5 (Rejeté)` - Rôles: ["3", "4"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true

**TypesCertificatsAutorises** :
- `CodeFormule` : "CO", `Libelle` : "Certificat d'Origine"
- `CodeFormule` : "B", `Libelle` : "CO + Formule A Cargo Commun"

### Exemple 2 : Chambre de Commerce Ouesso (Type 3)

**WorkflowConfiguration** :
- `TypePartenaireId` : Type 3
- `NomWorkflow` : "Workflow CO Standard"
- `TypeCertificat` : "CO"

**EtapesWorkflow** : (Identique au workflow standard)

**TypesCertificatsAutorises** :
- `CodeFormule` : "CO", `Libelle` : "Certificat d'Origine"
- `CodeFormule` : "EUR-1", `Libelle` : "Certificat EUR.1"
- `CodeFormule` : "CO+ALC", `Libelle` : "CO + Attestation de Libre Commercialisation"

**WorkflowConfiguration Formule A** :
- `TypePartenaireId` : Type 3
- `NomWorkflow` : "Workflow Formule A"
- `TypeCertificat` : "FormuleA"

**EtapesWorkflow Formule A** :
1. Ordre 1 : `12 (Formule A soumise) → 13 (Formule A contrôlée)` - Rôles: ["3", "4"], RequiertMotDePasse: true
2. Ordre 2 : `12 (Formule A soumise) → 5 (Rejetée)` - Rôles: ["3", "4"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true
3. Ordre 3 : `13 (Formule A contrôlée) → 14 (Formule A approuvée)` - Rôles: ["3", "4"], RequiertMotDePasse: true
4. Ordre 4 : `13 (Formule A contrôlée) → 5 (Rejetée)` - Rôles: ["3", "4"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true
5. Ordre 5 : `14 (Formule A approuvée) → 15 (Formule A validée)` - Rôles: ["6"], RequiertMotDePasse: true, RequiertMemeOrganisation: true
6. Ordre 6 : `14 (Formule A approuvée) → 5 (Rejetée)` - Rôles: ["6"], RequiertMotDePasse: true, RequiertCommentaire: true, EstRejet: true

---

## 🔧 Services à Implémenter

### 1. WorkflowConfigurationService
**Interface** : `IWorkflowConfigurationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<WorkflowConfigurationDto?> GetWorkflowParTypePartenaireAsync(Guid typePartenaireId, string typeCertificat)`
- `Task<List<EtapeWorkflowDto>> GetEtapesWorkflowAsync(Guid workflowConfigurationId)`
- `Task<List<EtapeWorkflowDto>> GetEtapesWorkflowParTypePartenaireAsync(Guid typePartenaireId, string typeCertificat)`
- `Task<List<TypeCertificatAutoriseDto>> GetTypesCertificatsAutorisesAsync(Guid typePartenaireId)`
- `Task<bool> EstTypeCertificatAutoriseAsync(Guid typePartenaireId, string codeFormule)`
- `Task<bool> PeutCreerFormuleAAsync(Guid typePartenaireId)` - Vérifie si le type permet Formule A

### 2. WorkflowValidationService (Mise à jour)
**Interface** : `IWorkflowValidationService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<bool> EstTransitionValideAsync(Guid certificatId, StatutCertificat statutSource, StatutCertificat statutCible, string userId)`
  - Récupère le workflow depuis la DB selon le type de partenaire
  - Vérifie si la transition existe dans les étapes
  - Vérifie les rôles, mot de passe, organisation
- `Task<List<StatutCertificat>> GetTransitionsPossiblesAsync(Guid certificatId, string userId)`
  - Récupère le workflow depuis la DB
  - Filtre selon le statut actuel et les permissions utilisateur
- `Task<EtapeWorkflowDto?> GetEtapeWorkflowAsync(Guid certificatId, StatutCertificat statutSource, StatutCertificat statutCible)`
- `Task<bool> VerifierPermissionsTransitionAsync(Guid certificatId, StatutCertificat nouveauStatut, string userId, List<string> roles)`

### 3. WorkflowService (Mise à jour)
**Interface** : `IWorkflowService`  
**Localisation** : `COService.Application/Services/`

**Fonctions** :
- `Task<CertificatOrigineDto> ExecuterTransitionAsync(Guid certificatId, StatutCertificat nouveauStatut, string userId, string password, string? commentaire)`
  - Récupère le workflow depuis la DB
  - Valide la transition selon la configuration
  - Exécute la transition
- `Task<WorkflowConfigurationDto> GetWorkflowPourCertificatAsync(Guid certificatId)`
  - Récupère le certificat
  - Récupère le partenaire
  - Récupère le type de partenaire
  - Récupère le workflow approprié

---

## 📋 Structure de la Base de Données

### Table : WorkflowsConfigurations

```sql
CREATE TABLE WorkflowsConfigurations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TypePartenaireId UNIQUEIDENTIFIER NOT NULL,
    NomWorkflow NVARCHAR(255) NOT NULL,
    TypeCertificat NVARCHAR(50) NOT NULL, -- "CO", "FormuleA", "EUR1", etc.
    EstActif BIT NOT NULL DEFAULT 1,
    CreeLe DATETIME2(7),
    CreePar NVARCHAR(MAX),
    ModifierLe DATETIME2(7),
    ModifiePar NVARCHAR(MAX),
    FOREIGN KEY (TypePartenaireId) REFERENCES TypesPartenaires(Id)
);

CREATE INDEX IX_WorkflowsConfigurations_TypePartenaireId ON WorkflowsConfigurations(TypePartenaireId);
CREATE INDEX IX_WorkflowsConfigurations_TypeCertificat ON WorkflowsConfigurations(TypeCertificat);
```

### Table : EtapesWorkflow

```sql
CREATE TABLE EtapesWorkflow (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    WorkflowConfigurationId UNIQUEIDENTIFIER NOT NULL,
    Ordre INT NOT NULL,
    StatutSource INT NOT NULL,
    StatutCible INT NOT NULL,
    LibelleStatutSource NVARCHAR(100),
    LibelleStatutCible NVARCHAR(100),
    RolesAutorises NVARCHAR(MAX), -- JSON array: ["3", "4"] ou ["6"]
    RequiertMotDePasse BIT NOT NULL DEFAULT 1,
    RequiertCommentaire BIT NOT NULL DEFAULT 0,
    RequiertMemeOrganisation BIT NOT NULL DEFAULT 0,
    PeutRejeter BIT NOT NULL DEFAULT 0,
    EstRejet BIT NOT NULL DEFAULT 0, -- Si true, statutCible = 5
    CreeLe DATETIME2(7),
    CreePar NVARCHAR(MAX),
    ModifierLe DATETIME2(7),
    ModifiePar NVARCHAR(MAX),
    FOREIGN KEY (WorkflowConfigurationId) REFERENCES WorkflowsConfigurations(Id) ON DELETE CASCADE
);

CREATE INDEX IX_EtapesWorkflow_WorkflowConfigurationId ON EtapesWorkflow(WorkflowConfigurationId);
CREATE INDEX IX_EtapesWorkflow_StatutSource ON EtapesWorkflow(StatutSource);
```

### Table : TypesCertificatsAutorises

```sql
CREATE TABLE TypesCertificatsAutorises (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TypePartenaireId UNIQUEIDENTIFIER NOT NULL,
    CodeFormule NVARCHAR(50) NOT NULL, -- "CO", "B", "EUR-1", "CO+ALC"
    Libelle NVARCHAR(255) NOT NULL,
    EstFormuleA BIT NOT NULL DEFAULT 0,
    EstActif BIT NOT NULL DEFAULT 1,
    CreeLe DATETIME2(7),
    CreePar NVARCHAR(MAX),
    ModifierLe DATETIME2(7),
    ModifiePar NVARCHAR(MAX),
    FOREIGN KEY (TypePartenaireId) REFERENCES TypesPartenaires(Id),
    UNIQUE (TypePartenaireId, CodeFormule)
);

CREATE INDEX IX_TypesCertificatsAutorises_TypePartenaireId ON TypesCertificatsAutorises(TypePartenaireId);
```

---

## 🔄 Flux d'Exécution d'une Transition

### Exemple : Contrôler un Certificat (2 → 4)

1. **Récupération du certificat**
   ```csharp
   var certificat = await _certificatRepository.GetByIdAsync(certificatId);
   ```

2. **Récupération du partenaire**
   ```csharp
   var partenaire = await _partenaireRepository.GetByIdAsync(certificat.PartenaireId);
   ```

3. **Récupération du type de partenaire**
   ```csharp
   var typePartenaire = partenaire.TypePartenaireId;
   ```

4. **Récupération du workflow**
   ```csharp
   var workflow = await _workflowConfigService.GetWorkflowParTypePartenaireAsync(
       typePartenaire, 
       certificat.IsFormuleA ? "FormuleA" : "CO"
   );
   ```

5. **Récupération de l'étape de transition**
   ```csharp
   var etape = await _workflowConfigService.GetEtapeWorkflowAsync(
       workflow.Id,
       StatutCertificat.Soumis, // 2
       StatutCertificat.Controle // 4
   );
   ```

6. **Validation des permissions**
   ```csharp
   // Vérifier les rôles
   var rolesAutorises = JsonSerializer.Deserialize<List<string>>(etape.RolesAutorises);
   if (!rolesUtilisateur.Any(r => rolesAutorises.Contains(r)))
       throw new UnauthorizedException();
   
   // Vérifier mot de passe si requis
   if (etape.RequiertMotDePasse)
       await _authService.VerifierMotDePasseAsync(userId, password);
   
   // Vérifier même organisation si requis
   if (etape.RequiertMemeOrganisation)
       await VerifierMemeOrganisationAsync(userId, certificat.PartenaireId);
   ```

7. **Exécution de la transition**
   ```csharp
   certificat.Statut = StatutCertificat.Controle;
   await _certificatRepository.UpdateAsync(certificat);
   
   // Enregistrer la validation
   await _validationService.EnregistrerValidationAsync(...);
   ```

---

## 🎯 Avantages de cette Architecture

### ✅ Flexibilité
- Chaque chambre peut avoir son workflow unique
- Facile d'ajouter de nouvelles chambres
- Modifications de workflow sans changer le code

### ✅ Extensibilité
- Ajout de nouveaux types de certificats
- Ajout de nouvelles transitions
- Modification des règles par chambre

### ✅ Maintenabilité
- Configuration centralisée dans la DB
- Pas de hardcoding
- Historique des configurations possible

### ✅ Testabilité
- Services testables avec des configurations mockées
- Validation des workflows isolée

---

## 📝 Migration depuis l'Existant

### Étape 1 : Créer les entités
- `WorkflowConfiguration`
- `EtapeWorkflow`
- `TypeCertificatAutorise`

### Étape 2 : Créer les migrations
- Tables avec relations
- Données initiales pour les chambres existantes

### Étape 3 : Créer les services
- `WorkflowConfigurationService`
- Mise à jour de `WorkflowValidationService`
- Mise à jour de `WorkflowService`

### Étape 4 : Migrer les workflows existants
- Pointe-Noire (Type 1) : Workflow CO + Types autorisés
- Ouesso (Type 3) : Workflow CO + Workflow Formule A + Types autorisés
- Autres chambres : Configurer selon leurs besoins

---

## 🔍 Exemple d'Utilisation

### Vérifier si une transition est possible

```csharp
public async Task<bool> PeutControleAsync(string userId, Guid certificatId)
{
    var certificat = await _certificatRepository.GetByIdAsync(certificatId);
    var partenaire = await _partenaireRepository.GetByIdAsync(certificat.PartenaireId);
    var workflow = await _workflowConfigService.GetWorkflowParTypePartenaireAsync(
        partenaire.TypePartenaireId, 
        certificat.IsFormuleA ? "FormuleA" : "CO"
    );
    
    var etapes = await _workflowConfigService.GetEtapesWorkflowAsync(workflow.Id);
    var etapePossible = etapes.FirstOrDefault(e => 
        e.StatutSource == (int)certificat.Statut && 
        e.StatutCible == (int)StatutCertificat.Controle
    );
    
    if (etapePossible == null) return false;
    
    var rolesUtilisateur = await _authService.GetRolesAsync(userId);
    var rolesAutorises = JsonSerializer.Deserialize<List<string>>(etapePossible.RolesAutorises);
    
    return rolesUtilisateur.Any(r => rolesAutorises.Contains(r));
}
```

---

## 📊 Résumé

Cette architecture permet de :
1. ✅ Gérer **plusieurs chambres de commerce** sans limite
2. ✅ Chaque chambre a son **workflow configurable** depuis la DB
3. ✅ **Pas de hardcoding** : tout est basé sur les données
4. ✅ **Extensible** : facile d'ajouter de nouvelles chambres ou workflows
5. ✅ **Maintenable** : modifications sans changer le code

---

**Document créé le** : 2025-01-XX  
**Version** : 1.0  
**Statut** : Architecture proposée
