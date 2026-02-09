# 🚀 PLAN D'ACTION - IMPLÉMENTATION COService

## 📊 État Actuel

### ✅ Déjà implémenté
- Entités : `CertificatOrigine`, `CertificateLine`, `CertificateValidation`, `Commentaire`, `Abonnement`, `CertificateType`
- Repositories pour ces entités
- Services de base
- Endpoints de base

### ⚠️ À corriger
- Tables en anglais : `certificates`, `certificate_lines`, etc.
- Doivent être renommées en français PascalCase pluriel : `Certificats`, `LignesCertificats`, etc.

---

## 🎯 PLAN D'ACTION PAR PRIORITÉ

### 🔴 PHASE 1 : Correction des Tables Existantes (PRIORITÉ HAUTE)

**Objectif** : Renommer toutes les tables existantes en français PascalCase pluriel

**Actions** :
1. Créer une migration pour renommer les tables
2. Mettre à jour les configurations EF Core
3. Tester que tout fonctionne

**Tables à renommer** :
- `certificates` → `Certificats`
- `certificate_lines` → `LignesCertificats`
- `certificate_validations` → `ValidationsCertificats`
- `certificate_types` → `TypesCertificats`
- `commentaires` → `Commentaires` (déjà en français mais pas PascalCase)
- `abonnements` → `Abonnements` (déjà en français mais pas PascalCase)

**Durée estimée** : 1-2 heures

---

### 🟠 PHASE 2 : Organisations (PRIORITÉ HAUTE)

**Objectif** : Implémenter les entités Partenaire et Exportateur synchronisées depuis Enrolement

**Actions** :
1. Créer les entités `Partenaire` et `Exportateur`
2. Créer les configurations EF Core
3. Créer les repositories (lecture seule)
4. Créer les DTOs
5. Créer les services (lecture seule)
6. Créer les endpoints (GET uniquement)
7. Créer le client API Enrolement (`IEnrolementServiceClient`)
8. Créer le service de synchronisation (`EnrolementSyncService`)

**Durée estimée** : 3-4 heures

---

### 🟡 PHASE 3 : Référentiels Locaux (PRIORITÉ MOYENNE)

**Objectif** : Implémenter les référentiels synchronisés depuis le référentiel global

**Actions** :
1. Créer les entités référentiels (22+ tables)
2. Créer les configurations EF Core
3. Créer les repositories
4. Créer les DTOs
5. Créer les services
6. Créer les endpoints
7. Créer le client API Référentiel (`IReferentielServiceClient`)
8. Créer le service de synchronisation (`ReferentielSyncService`)

**Tables prioritaires** :
- `Departements` (utilisé pour génération numéros)
- `Pays` (utilisé dans certificats)
- `Ports`, `Aeroports` (utilisés dans certificats)
- Puis les autres selon besoin

**Durée estimée** : 4-6 heures

---

### 🟢 PHASE 4 : Entités Propres au CO (PRIORITÉ MOYENNE)

**Objectif** : Implémenter les entités gérées localement par COService

**Actions** :
1. Créer l'entité `ZoneProduction`
2. Créer les autres entités manquantes (DestinataireProduit, etc.)
3. Créer les configurations EF Core
4. Créer les repositories
5. Créer les DTOs
6. Créer les services (CRUD complet)
7. Créer les endpoints

**Durée estimée** : 2-3 heures

---

### 🔵 PHASE 5 : Workflows par Chambre (PRIORITÉ HAUTE)

**Objectif** : Implémenter les workflows spécifiques par chambre de commerce

**Actions** :
1. Créer les services de workflow par chambre (codés en dur)
   - `PointeNoireWorkflowService`
   - `OuessoWorkflowService`
2. Implémenter les validations de transitions
3. Implémenter les règles métier
4. Tester les workflows

**Durée estimée** : 4-5 heures

---

### 🟣 PHASE 6 : Services Métier (PRIORITÉ HAUTE)

**Objectif** : Implémenter les services métier essentiels

**Actions** :
1. `NumeroGenerationService` - Génération des numéros de certificats
2. `PDFGenerationService` - Génération des PDFs
3. `FormuleAService` - Gestion des Formules A
4. `ValidationCertificatService` - Validations workflow
5. `NotificationService` - Notifications

**Durée estimée** : 6-8 heures

---

## 🎯 PAR OÙ COMMENCER ?

### Recommandation : Phase 1 d'abord

**Pourquoi** :
- ✅ Corriger les noms de tables avant d'ajouter de nouvelles entités
- ✅ Éviter de devoir renommer plus tard
- ✅ Base solide pour la suite

**Ensuite** : Phase 2 (Organisations) car elles sont référencées par les certificats

---

## 📝 PROCHAINES ÉTAPES IMMÉDIATES

1. **Créer la migration de renommage** des tables existantes
2. **Tester** que tout fonctionne après le renommage
3. **Puis** commencer Phase 2 (Partenaire, Exportateur)

---

**Document créé le** : 2025-01-XX  
**Version** : 1.0  
**Statut** : Plan d'action prêt
