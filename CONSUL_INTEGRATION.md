# Intégration de Consul dans COService

## Qu'est-ce que Consul ?

**Consul** est un outil développé par HashiCorp qui facilite la gestion des microservices. Il offre plusieurs fonctionnalités essentielles :

### 1. Service Discovery (Découverte de Services)
- **Problème résolu** : Dans une architecture microservices, les services changent d'adresse (IP, port) dynamiquement
- **Solution Consul** : Les services s'enregistrent automatiquement dans Consul avec leur adresse
- **Avantage** : Les autres services découvrent automatiquement où se trouvent les services sans configuration manuelle

**Exemple** :
```
Sans Consul :
- COService doit connaître l'URL exacte : "https://api.example.com/auth"
- Si AuthService change d'adresse, il faut modifier la configuration

Avec Consul :
- COService demande à Consul : "Où est AuthService ?"
- Consul répond : "AuthService est à http://192.168.1.10:5000"
- Si AuthService change, il se réenregistre automatiquement
```

### 2. Health Checks (Vérifications de Santé)
- Consul vérifie périodiquement si les services sont en ligne
- Si un service tombe, Consul le retire automatiquement de la liste des services disponibles
- Permet le load balancing et la haute disponibilité

### 3. Configuration Distribuée (KV Store)
- Stocke des configurations partagées entre services
- Permet de modifier la configuration sans redéployer les services
- Exemple : URLs des microservices, timeouts, retry policies

### 4. Service Segmentation
- Gestion des politiques de sécurité réseau
- Contrôle de l'accès entre services

## Pourquoi utiliser Consul dans COService ?

Dans votre architecture microservices, Consul vous permet de :

1. **Découvrir automatiquement les autres microservices**
   - AuthService
   - VisaDossierService
   - DocumentService
   - NotificationService
   - FacturationService
   - COExchangeService

2. **Éviter les configurations statiques**
   - Plus besoin de hardcoder les URLs dans `appsettings.json`
   - Les services se découvrent dynamiquement

3. **Gérer la haute disponibilité**
   - Si un service a plusieurs instances, Consul peut faire du load balancing
   - Si une instance tombe, Consul la retire automatiquement

4. **Centraliser la configuration**
   - Configuration partagée dans Consul KV Store
   - Modification sans redéploiement

## Architecture avec Consul

```
┌─────────────────────────────────────────────────────────┐
│                    Consul Server                        │
│  - Service Registry (Registre des services)             │
│  - Health Checks                                        │
│  - KV Store (Configuration)                             │
└─────────────────────────────────────────────────────────┘
         ▲                    ▲                    ▲
         │                    │                    │
    ┌────┴────┐          ┌────┴────┐          ┌────┴────┐
    │COService│          │Auth     │          │Visa     │
    │         │          │Service  │          │Dossier  │
    │         │          │         │          │Service  │
    └─────────┘          └─────────┘          └─────────┘
    S'enregistre      S'enregistre      S'enregistre
    Découvre les      Découvre les      Découvre les
    autres services   autres services   autres services
```

## Flux de Fonctionnement

### 1. Au Démarrage du Service
```
COService démarre
    ↓
S'enregistre dans Consul avec :
  - Nom : "coservice"
  - Adresse : "http://localhost:5114"
  - Health Check : "/health"
    ↓
Consul vérifie périodiquement la santé
```

### 2. Découverte d'un Service
```
COService a besoin d'appeler AuthService
    ↓
Demande à Consul : "Où est auth-service ?"
    ↓
Consul répond : "http://192.168.1.10:5000"
    ↓
COService appelle AuthService à cette adresse
```

### 3. Si un Service Tombe
```
AuthService tombe en panne
    ↓
Consul détecte que le health check échoue
    ↓
Consul retire AuthService de la liste
    ↓
COService ne reçoit plus cette adresse lors de la découverte
```

## Packages NuGet pour .NET

Pour intégrer Consul dans .NET, nous utiliserons :

1. **Consul** - Bibliothèque cliente officielle pour .NET
2. **Microsoft.Extensions.ServiceDiscovery** (optionnel, .NET 8+) - Support natif de service discovery

## Configuration

### Dans appsettings.json
```json
{
  "Consul": {
    "Address": "http://localhost:8500",
    "ServiceName": "coservice",
    "ServiceId": "coservice-1",
    "ServiceAddress": "http://localhost:5114",
    "HealthCheck": {
      "Endpoint": "/health",
      "Interval": 10,
      "Timeout": 5,
      "DeregisterCriticalServiceAfter": 30
    }
  }
}
```

### Dans Program.cs
- Enregistrement du service au démarrage
- Configuration de la découverte de services
- Health checks

## Avantages pour COService

✅ **Découverte automatique** des microservices (Auth, VisaDossier, etc.)
✅ **Configuration centralisée** dans Consul KV Store
✅ **Haute disponibilité** avec health checks
✅ **Pas de configuration statique** des URLs
✅ **Load balancing** automatique si plusieurs instances
✅ **Monitoring** de l'état des services

## Prochaines Étapes

1. Installer Consul (serveur)
2. Installer les packages NuGet Consul
3. Configurer l'enregistrement du service
4. Configurer la découverte des autres services
5. Remplacer les URLs statiques par la découverte Consul

