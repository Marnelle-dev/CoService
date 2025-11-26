# Plan de Réalisation - Microservice COService

## Vue d'ensemble

Ce document présente le plan de réalisation étape par étape pour le développement du microservice COService.

---

## Phase 1 : Initialisation et Structure du Projet

### Étape 1.1 : Création de la Solution et des Projets
- [ ] Créer la solution .NET 8
- [ ] Créer les projets (API, Application, Domain, Infrastructure, Shared)
- [ ] Configurer les références entre projets
- [ ] Configurer le fichier .gitignore
- [ ] Initialiser le dépôt Git

**Livrables :**
- Solution .NET avec tous les projets
- Structure de dossiers conforme à l'architecture

**Durée estimée :** 1-2 heures

---

### Étape 1.2 : Configuration des Packages NuGet
- [ ] Installer les packages dans chaque projet
- [ ] Configurer les versions compatibles
- [ ] Vérifier les dépendances

**Livrables :**
- Fichiers .csproj avec tous les packages nécessaires

**Durée estimée :** 30 minutes

---

## Phase 2 : Modèle de Données et Base de Données

### Étape 2.1 : Définition des Entités Domain
- [ ] Créer les entités (DemandeCO, CertificatOrigine, StatutValidation, HistoriqueValidation)
- [ ] Définir les enums (StatutDemande, TypeCertificat, RoleUtilisateur)
- [ ] Créer les Value Objects si nécessaire
- [ ] Ajouter les propriétés de navigation

**Livrables :**
- Toutes les entités dans COService.Domain/Entities
- Tous les enums dans COService.Domain/Enums

**Durée estimée :** 2-3 heures

---

### Étape 2.2 : Configuration Entity Framework Core
- [ ] Créer le DbContext (COServiceDbContext)
- [ ] Créer les configurations EF Core pour chaque entité
- [ ] Configurer les relations et contraintes
- [ ] Configurer les index et clés uniques

**Livrables :**
- COServiceDbContext.cs
- Toutes les configurations dans Infrastructure/Data/Configurations

**Durée estimée :** 2-3 heures

---

### Étape 2.3 : Création de la Base de Données
- [ ] Configurer la chaîne de connexion (appsettings.json)
- [ ] Créer la première migration
- [ ] Appliquer la migration
- [ ] Vérifier la structure de la base de données

**Livrables :**
- Migration initiale
- Base de données SQL Server créée avec toutes les tables

**Durée estimée :** 1-2 heures

---

## Phase 3 : Infrastructure et Repositories

### Étape 3.1 : Implémentation des Repositories
- [ ] Créer l'interface IRepository générique
- [ ] Implémenter Repository<T> générique
- [ ] Créer les interfaces spécifiques (ICertificatOrigineRepository, etc.)
- [ ] Implémenter les repositories spécifiques

**Livrables :**
- Pattern Repository implémenté
- Tous les repositories dans Infrastructure/Repositories

**Durée estimée :** 2-3 heures

---

### Étape 3.2 : Clients pour Microservices Externes
- [ ] Créer les interfaces pour chaque service externe
- [ ] Implémenter AuthServiceClient (avec Refit ou HttpClient)
- [ ] Implémenter VisaDossierServiceClient
- [ ] Implémenter DocumentServiceClient
- [ ] Implémenter NotificationServiceClient
- [ ] Implémenter FacturationServiceClient
- [ ] Ajouter la gestion des erreurs et retry policies (Polly)

**Livrables :**
- Tous les clients dans Infrastructure/ExternalServices
- Configuration des URLs et authentification

**Durée estimée :** 4-5 heures

---

### Étape 3.3 : Services Infrastructure
- [ ] Implémenter NotificationService (wrapper)
- [ ] Implémenter BarreProgressionService
- [ ] Configurer l'injection de dépendances

**Livrables :**
- Services dans Infrastructure/Services
- Configuration DI dans Program.cs

**Durée estimée :** 1-2 heures

---

## Phase 4 : Couche Application

### Étape 4.1 : DTOs et Mappings
- [ ] Créer tous les DTOs nécessaires
- [ ] Configurer AutoMapper
- [ ] Créer les profils de mapping
- [ ] Tester les mappings

**Livrables :**
- Tous les DTOs dans Application/DTOs
- MappingProfile configuré

**Durée estimée :** 2-3 heures

---

### Étape 4.2 : Validators
- [ ] Créer les validators FluentValidation
- [ ] Valider DemandeCODto
- [ ] Valider les autres DTOs si nécessaire
- [ ] Intégrer dans le pipeline de validation

**Livrables :**
- Validators dans Application/Validators

**Durée estimée :** 1-2 heures

---

### Étape 4.3 : Services Application
- [ ] Créer ICertificatOrigineService
- [ ] Implémenter CertificatOrigineService
  - [ ] Créer une demande CO
  - [ ] Récupérer les demandes par exportateur
  - [ ] Télécharger un certificat validé
  - [ ] Filtrer par pays et rôle
- [ ] Créer IDemandeCOService
- [ ] Implémenter DemandeCOService
  - [ ] Suivre le workflow de validation
  - [ ] Récupérer la barre de progression
  - [ ] Gérer les notifications

**Livrables :**
- Tous les services dans Application/Services
- Logique métier complète

**Durée estimée :** 6-8 heures

---

## Phase 5 : API et Endpoints

### Étape 5.1 : Configuration de l'API
- [ ] Configurer Program.cs (API minimales)
- [ ] Configurer Swagger/OpenAPI
- [ ] Configurer CORS
- [ ] Configurer l'authentification (JWT si nécessaire)
- [ ] Configurer le logging (Serilog)

**Livrables :**
- API configurée et fonctionnelle
- Swagger accessible

**Durée estimée :** 2-3 heures

---

### Étape 5.2 : Middleware
- [ ] Créer ExceptionHandlingMiddleware
- [ ] Créer LoggingMiddleware
- [ ] Intégrer dans le pipeline

**Livrables :**
- Middleware dans API/Middleware

**Durée estimée :** 1-2 heures

---

### Étape 5.3 : Endpoints API
- [ ] Créer CertificatOrigineEndpoints
  - [ ] POST /api/certificats-origine/demandes (Créer demande)
  - [ ] GET /api/certificats-origine/demandes (Liste des demandes)
  - [ ] GET /api/certificats-origine/demandes/{id} (Détails demande)
  - [ ] GET /api/certificats-origine/{id}/download (Télécharger CO)
  - [ ] GET /api/certificats-origine/progression/{id} (Barre progression)
- [ ] Créer les autres endpoints nécessaires
- [ ] Ajouter la validation des DTOs
- [ ] Ajouter la gestion des erreurs

**Livrables :**
- Tous les endpoints dans API/Endpoints
- API complète et testable

**Durée estimée :** 4-5 heures

---

## Phase 6 : Intégration et Tests

### Étape 6.1 : Tests Unitaires
- [ ] Tests des services Application
- [ ] Tests des repositories
- [ ] Tests des validators
- [ ] Couverture de code > 70%

**Livrables :**
- Tests unitaires dans COService.Tests/Unit

**Durée estimée :** 4-6 heures

---

### Étape 6.2 : Tests d'Intégration
- [ ] Tests des endpoints API
- [ ] Tests d'intégration avec la base de données
- [ ] Tests d'intégration avec les microservices (mocks)

**Livrables :**
- Tests d'intégration dans COService.Tests/Integration

**Durée estimée :** 3-4 heures

---

### Étape 6.3 : Tests Manuels
- [ ] Tester le workflow complet
- [ ] Tester les intégrations avec les microservices
- [ ] Vérifier les notifications
- [ ] Vérifier la barre de progression

**Livrables :**
- Checklist de tests validée

**Durée estimée :** 2-3 heures

---

## Phase 7 : Documentation

### Étape 7.1 : Documentation API
- [ ] Compléter les annotations Swagger
- [ ] Ajouter des exemples dans Swagger
- [ ] Documenter les codes de retour

**Livrables :**
- Documentation Swagger complète

**Durée estimée :** 1-2 heures

---

### Étape 7.2 : Documentation Technique
- [ ] Mettre à jour le README.md
- [ ] Documenter l'architecture
- [ ] Documenter les variables d'environnement
- [ ] Documenter le déploiement

**Livrables :**
- README.md complet

**Durée estimée :** 1-2 heures

---

## Phase 8 : Docker et Déploiement

### Étape 8.1 : Dockerisation
- [ ] Créer le Dockerfile
- [ ] Créer le .dockerignore
- [ ] Créer docker-compose.yml pour développement
- [ ] Tester l'image Docker localement

**Livrables :**
- Dockerfile fonctionnel
- docker-compose.yml pour dev

**Durée estimée :** 2-3 heures

---

### Étape 8.2 : Configuration GitHub
- [ ] Créer le dépôt GitHub
- [ ] Configurer .gitignore
- [ ] Créer les branches (main, develop)
- [ ] Configurer les workflows GitHub Actions (optionnel)
- [ ] Push du code

**Livrables :**
- Code sur GitHub
- Structure de branches configurée

**Durée estimée :** 1 heure

---

### Étape 8.3 : Déploiement sur Portainer
- [ ] Créer le stack file (docker-compose.yml pour production)
- [ ] Configurer les variables d'environnement
- [ ] Configurer la connexion à la base de données
- [ ] Déployer sur Portainer
- [ ] Vérifier le déploiement

**Livrables :**
- Application déployée sur Portainer
- Documentation de déploiement

**Durée estimée :** 2-3 heures

---

## Estimation Totale

**Durée totale estimée :** 40-55 heures (5-7 jours de travail)

---

## Points d'Attention

### Concernant les Acteurs (Contrôleur, Superviseur, Signataire)

**Recommandation :** Conserver ces rôles dans le modèle de données car :

1. **Traçabilité** : Le COService doit enregistrer qui a validé à chaque étape
2. **Notifications** : Les notifications doivent être envoyées aux bons rôles
3. **Affichage** : L'interface doit afficher les validations par rôle
4. **Intégration** : Même si visaDossier gère le workflow, COService doit connaître les rôles pour :
   - Afficher la barre de progression
   - Envoyer les notifications appropriées
   - Filtrer les certificats par rôle
   - Historiser les actions

**Approche suggérée :**
- Les rôles sont définis dans le microservice Auth
- COService interroge Auth pour vérifier les permissions
- COService enregistre les validations avec le rôle du validateur
- Le workflow de validation est orchestré par visaDossier
- COService suit les statuts via visaDossier et met à jour son état interne

---

## Prochaines Étapes

Une fois ce plan validé :

1. **Phase 1** : Initialisation et structure
2. **Phase 2** : Modèle de données
3. **Phase 3** : Infrastructure
4. **Phase 4** : Application
5. **Phase 5** : API
6. **Phase 6** : Tests
7. **Phase 7** : Documentation
8. **Phase 8** : Déploiement

---

## Questions à Valider

1. ✅ Les rôles Contrôleur, Superviseur, Signataire sont-ils conservés dans le modèle ?
2. Comment sont gérées les permissions ? (via Auth service uniquement ?)
3. Format des certificats PDF ? (template à définir)
4. Comment CO-Exchange communique-t-il avec COService ? (API REST ? Message queue ?)
5. Stratégie de cache ? (Redis ?)
6. Stratégie de logs ? (centralisés ?)

