# COService - Microservice Certificats d'Origine

## Description

Le microservice **COService** permet aux exportateurs d'effectuer des demandes de certificats d'origine (CO) pour leurs marchandises, de suivre un workflow de validation selon les chambres de commerce, et de télécharger les CO validés.

## Architecture

Ce microservice suit une architecture en couches (Clean Architecture) avec :

- **COService.API** : Couche API avec API minimales
- **COService.Application** : Logique métier et services
- **COService.Domain** : Entités et modèles métier
- **COService.Infrastructure** : Accès aux données, repositories, clients externes
- **COService.Shared** : Éléments partagés (constantes, exceptions)

## Technologies

- **.NET 8** avec API minimales
- **Entity Framework Core** pour l'accès aux données
- **SQL Server** comme base de données
- **AutoMapper** pour le mapping d'objets
- **FluentValidation** pour la validation
- **Refit** pour les appels HTTP aux microservices
- **Polly** pour la résilience
- **Serilog** pour le logging

## Fonctionnalités Principales

1. **Gestion des demandes de CO**
   - Création de demandes par les exportateurs
   - Suivi du statut des demandes
   - Historique des validations

2. **Workflow de validation**
   - Intégration avec le microservice visaDossier
   - Validation par Contrôleur, Superviseur, Signataire
   - Barre de progression des validations

3. **Génération et téléchargement**
   - Génération des certificats validés
   - Téléchargement filtré par pays et rôle

4. **Notifications**
   - Notifications à chaque étape du workflow
   - Intégration avec le microservice Notifications

## Intégrations

Ce microservice s'intègre avec :

- **Auth Service** : Authentification et gestion des rôles
- **VisaDossier Service** : Workflow de validation
- **Document Service** : Gestion des pièces justificatives
- **Notification Service** : Envoi des notifications
- **Facturation Service** : Gestion des paiements
- **CO-Exchange** : Échange de certificats avec d'autres pays

## Documentation

- [Structure du Projet](STRUCTURE_PROJET.md)
- [Plan de Réalisation](PLAN_REALISATION.md)
- [Décisions Architecturales](ARCHITECTURE_DECISIONS.md)

## Démarrage Rapide

### Prérequis

- .NET 8 SDK
- SQL Server (local ou distant)
- Visual Studio 2022 ou VS Code

### Installation

```bash
# Cloner le dépôt
git clone <repository-url>
cd COService

# Restaurer les packages
dotnet restore

# Configurer la chaîne de connexion dans appsettings.json

# Appliquer les migrations
dotnet ef database update --project COService.Infrastructure --startup-project COService.API

# Lancer l'application
dotnet run --project COService.API
```

L'API sera accessible sur `https://localhost:5001` avec Swagger sur `/swagger`.

## Structure du Projet

```
COService/
├── COService.API/              # API Minimales
├── COService.Application/      # Logique métier
├── COService.Domain/           # Entités et modèles
├── COService.Infrastructure/   # Infrastructure
├── COService.Shared/           # Éléments partagés
└── COService.Tests/            # Tests
```

## Déploiement

### Docker

```bash
docker build -t coservice .
docker run -p 8080:80 coservice
```

### Portainer

Voir la documentation de déploiement dans le plan de réalisation.

## Contribution

Voir le [Plan de Réalisation](PLAN_REALISATION.md) pour les étapes de développement.

## Licence

[À définir]

