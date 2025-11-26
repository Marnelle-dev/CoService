# Décisions Architecturales - COService

## 1. Concernant les Acteurs (Contrôleur, Superviseur, Signataire)

### Question
Les acteurs Contrôleur, Superviseur et Signataire sont-ils toujours nécessaires si le workflow de validation est géré par le microservice visaDossier ?

### Décision
**OUI, ces rôles doivent être conservés** dans le modèle de données et la logique du COService.

### Justification

#### 1.1 Traçabilité et Historique
- Le COService doit enregistrer qui a validé à chaque étape
- L'historique des validations doit être consultable
- Les certificats doivent contenir les informations de validation

#### 1.2 Notifications
- Les notifications doivent être envoyées aux bons rôles à chaque étape
- Chaque rôle doit recevoir des notifications spécifiques
- Le COService orchestre les notifications même si visaDossier gère le workflow

#### 1.3 Affichage et UX
- La barre de progression doit afficher les étapes par rôle
- L'interface doit montrer qui a validé et quand
- Les filtres peuvent être basés sur les rôles

#### 1.4 Intégration avec visaDossier
- **visaDossier** : Gère le workflow, les règles de validation, l'ordre des validations
- **COService** : 
  - Consulte visaDossier pour connaître le statut actuel
  - Enregistre les validations avec le rôle du validateur
  - Met à jour son état interne basé sur les statuts de visaDossier
  - Orchestre les notifications et l'affichage

### Modèle de Données Proposé

```csharp
// Dans StatutValidation
public class StatutValidation
{
    public Guid Id { get; set; }
    public Guid DemandeCOId { get; set; }
    public StatutDemande Statut { get; set; } // Contrôlé, Approuvé, Validé
    public RoleUtilisateur RoleValidateur { get; set; } // Contrôleur, Superviseur, Signataire
    public Guid ValidateurId { get; set; }
    public DateTime DateValidation { get; set; }
    public string? Commentaire { get; set; }
    public int OrdreValidation { get; set; }
    public Guid? DossierId { get; set; } // Référence au dossier dans visaDossier
}
```

### Flux de Validation

1. **Exportateur** crée une demande CO dans COService
2. **COService** crée un dossier dans visaDossier
3. **visaDossier** gère le workflow et notifie les validateurs
4. **Contrôleur** valide dans visaDossier → visaDossier notifie COService
5. **COService** enregistre la validation avec le rôle Contrôleur
6. **COService** envoie une notification à l'exportateur
7. Répéter pour Superviseur et Signataire
8. **Signataire** valide → visaDossier notifie COService
9. **COService** génère le certificat et le met à disposition

---

## 2. Architecture Technique

### 2.1 Clean Architecture
- Séparation claire des responsabilités
- Indépendance de la base de données
- Testabilité accrue

### 2.2 API Minimales (.NET 8)
- Performance optimale
- Code plus léger
- Endpoints organisés par fonctionnalité

### 2.3 Entity Framework Core
- ORM standard pour .NET
- Migrations automatiques
- Support SQL Server natif

### 2.4 Pattern Repository
- Abstraction de l'accès aux données
- Facilité de test
- Possibilité de changer de source de données

---

## 3. Intégration avec les Microservices

### 3.1 Communication
- **HTTP/REST** avec Refit pour les appels synchrones
- **Retry Policies** avec Polly pour la résilience
- **Circuit Breaker** pour éviter les cascades d'erreurs

### 3.2 Authentification
- **JWT** pour l'authentification
- Vérification des rôles via Auth Service
- Middleware d'authentification dans l'API

### 3.3 Gestion des Erreurs
- Exceptions personnalisées
- Middleware de gestion d'erreurs global
- Logs structurés avec Serilog

---

## 4. Base de Données

### 4.1 SQL Server
- Base de données relationnelle
- Support des transactions
- Performance pour les requêtes complexes

### 4.2 Migrations
- Migrations EF Core pour versionner le schéma
- Scripts de migration pour la production

### 4.3 Index et Performance
- Index sur les colonnes fréquemment interrogées
- Clés uniques pour les numéros de demande et certificats

---

## 5. Déploiement

### 5.1 Docker
- Containerisation pour la portabilité
- Dockerfile multi-stage pour optimiser la taille

### 5.2 Portainer
- Orchestration via docker-compose
- Gestion des variables d'environnement
- Déploiement simplifié

### 5.3 GitHub
- Versionnement du code
- CI/CD possible avec GitHub Actions
- Documentation dans le dépôt

---

## 6. Sécurité

### 6.1 Authentification et Autorisation
- Vérification des rôles via Auth Service
- Validation des permissions avant chaque action
- JWT pour l'authentification

### 6.2 Validation des Données
- FluentValidation pour valider les DTOs
- Validation côté serveur obligatoire
- Protection contre les injections SQL (EF Core)

### 6.3 Logs et Audit
- Logs de toutes les actions importantes
- Historique des modifications
- Traçabilité complète

---

## 7. Performance

### 7.1 Optimisations
- Pagination pour les listes
- Index sur les colonnes de recherche
- Requêtes optimisées avec EF Core

### 7.2 Cache (Futur)
- Possibilité d'ajouter Redis pour le cache
- Cache des rôles et permissions
- Cache des configurations de workflow

---

## 8. Monitoring et Observabilité

### 8.1 Logging
- Serilog pour les logs structurés
- Niveaux de log appropriés
- Logs centralisés (futur)

### 8.2 Health Checks
- Endpoint /health pour vérifier l'état
- Vérification de la connexion à la base de données
- Vérification des microservices externes

---

## Questions Ouvertes

1. **Format des certificats PDF** : Template à définir, bibliothèque à utiliser ?
2. **CO-Exchange** : Communication synchrone ou asynchrone ? Message queue ?
3. **Cache** : Redis nécessaire dès le début ou plus tard ?
4. **Logs centralisés** : Solution à utiliser (ELK, Seq, etc.) ?
5. **Monitoring** : Solution de monitoring (Prometheus, Application Insights, etc.) ?

