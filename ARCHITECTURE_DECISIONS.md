# Décisions Architecturales - COService

## Outils et Technologies Utilisés

### 1. RabbitMQ - Messaging Asynchrone
**Rôle** : Gestion des messages et événements entre microservices

**Impact sur COService** :
- ✅ **Synchronisation événementielle** : Les mises à jour des organisations (Partenaires, Exportateurs) et référentiels seront reçues via RabbitMQ
- ✅ **Notifications** : Les notifications seront envoyées via RabbitMQ vers le service de notifications
- ✅ **Événements métier** : Les événements de changement de statut de certificat seront publiés via RabbitMQ

**À retirer/modifier** :
- ❌ Synchronisation périodique via HTTP (remplacée par événements RabbitMQ)
- ❌ Appels HTTP directs pour les notifications (remplacés par messages RabbitMQ)

**À implémenter** :
- 📦 Client RabbitMQ pour publier/consommer des messages
- 📦 Handlers d'événements pour les mises à jour d'organisations et référentiels
- 📦 Publication d'événements lors des changements de statut de certificat

---

### 2. API Gateway avec Apache APISIX - Service Discovery
**Rôle** : Point d'entrée unique et routage vers les microservices

**Impact sur COService** :
- ✅ **Service Discovery** : APISIX gère la découverte de services via Consul, pas besoin de découverte côté client
- ✅ **Routage** : Les appels entre microservices passent par l'API Gateway APISIX
- ✅ **Authentification centralisée** : APISIX gère l'authentification (JWT, OAuth2, etc.)
- ✅ **Rate Limiting** : APISIX peut gérer le rate limiting
- ✅ **Load Balancing** : APISIX fournit le load balancing dynamique
- ✅ **Logging et Monitoring** : APISIX fournit des logs et métriques
- ✅ **Configuration dynamique** : APISIX utilise etcd pour la configuration en temps réel

**À retirer/modifier** :
- ❌ Découverte de service via Consul dans les clients HTTP (APISIX s'en charge)
- ❌ Configuration de base URLs dynamiques via Consul (utilisation d'URLs statiques vers APISIX)

**À conserver** :
- ✅ Enregistrement du service dans Consul (pour qu'APISIX puisse le découvrir via le plugin Consul)
- ✅ Health checks Consul (pour qu'APISIX vérifie la disponibilité)

**À adapter** :
- 📝 Les clients HTTP (Enrolement, Referentiel) doivent appeler APISIX, pas directement les services
- 📝 Configuration des URLs des services externes doit pointer vers APISIX
- 📝 APISIX utilise Consul pour la découverte de services (plugin consul-kv)

---

### 3. GitLab CI/CD - Déploiement Automatique
**Rôle** : Pipeline de build, test et déploiement

**Impact sur COService** :
- ✅ **Build automatique** : Compilation et création des artefacts
- ✅ **Tests automatiques** : Exécution des tests unitaires et d'intégration
- ✅ **Déploiement** : Déploiement automatique vers les environnements (dev, staging, prod)
- ✅ **Docker** : Build et push des images Docker

**À préparer** :
- 📝 Fichier `.gitlab-ci.yml` pour définir le pipeline
- 📝 Configuration des variables d'environnement dans GitLab
- 📝 Scripts de déploiement

**À retirer** :
- ❌ Scripts de déploiement manuels (remplacés par GitLab CI/CD)

---

## Architecture Adaptée

### Communication Inter-Services

#### Avant (avec Consul direct)
```
COService → Consul Discovery → EnrolementService
COService → Consul Discovery → ReferentielService
```

#### Après (avec API Gateway)
```
COService → API Gateway (APISIX) → EnrolementService
COService → API Gateway (APISIX) → ReferentielService
```

### Synchronisation des Données

#### Avant (synchronisation périodique HTTP)
```
COService → HTTP Polling → EnrolementService (toutes les heures)
COService → HTTP Polling → ReferentielService (toutes les heures)
```

#### Après (événements RabbitMQ)
```
EnrolementService → RabbitMQ Event → COService (en temps réel)
ReferentielService → RabbitMQ Event → COService (en temps réel)
```

### Notifications

#### Avant (appels HTTP directs)
```
COService → HTTP → NotificationService
```

#### Après (messages RabbitMQ)
```
COService → RabbitMQ Message → NotificationService
```

---

## Modifications à Apporter au Code

### 1. Clients HTTP (Enrolement, Referentiel)
- ✅ **Modifier** : Utiliser l'URL de l'API Gateway au lieu de la découverte Consul
- ✅ **Conserver** : La structure des clients Refit reste identique
- ✅ **Configuration** : URL de l'API Gateway dans `appsettings.json`

### 2. Service de Synchronisation
- ✅ **Modifier** : Remplacer la synchronisation périodique par des handlers d'événements RabbitMQ
- ✅ **Conserver** : Les méthodes de synchronisation peuvent être appelées manuellement via API
- ✅ **Ajouter** : Handlers pour consommer les événements RabbitMQ

### 3. Service de Notification
- ✅ **Modifier** : Publier des messages RabbitMQ au lieu d'appels HTTP
- ✅ **Ajouter** : Client RabbitMQ pour la publication de messages

### 4. Consul
- ✅ **Conserver** : Enregistrement du service et health checks
- ❌ **Retirer** : Découverte de service côté client (gérée par APISIX via plugin consul-kv)

---

## Prochaines Étapes

1. ✅ **Adapter les clients HTTP** : Utiliser Apache APISIX API Gateway
2. ✅ **Intégrer RabbitMQ** : Client et handlers d'événements
3. ✅ **Créer le pipeline GitLab CI/CD** : `.gitlab-ci.yml`
4. ✅ **Documenter les événements RabbitMQ** : Format des messages
5. ✅ **Adapter la configuration** : URLs API Gateway dans `appsettings.json`

---

## Événements RabbitMQ à Implémenter

### Événements Consommés (Reçus)
- `partenaire.creé` - Création d'un partenaire
- `partenaire.modifié` - Modification d'un partenaire
- `partenaire.supprimé` - Suppression d'un partenaire
- `exportateur.creé` - Création d'un exportateur
- `exportateur.modifié` - Modification d'un exportateur
- `exportateur.supprimé` - Suppression d'un exportateur
- `referentiel.pays.mis-a-jour` - Mise à jour des pays
- `referentiel.port.mis-a-jour` - Mise à jour des ports
- `referentiel.devise.mis-a-jour` - Mise à jour des devises
- (etc. pour tous les référentiels)

### Événements Publiés (Envoyés)
- `certificat.statut.changé` - Changement de statut d'un certificat
- `certificat.creé` - Création d'un certificat
- `certificat.validé` - Validation d'un certificat
- `certificat.rejeté` - Rejet d'un certificat
- `notification.demande` - Demande de notification

---

## Configuration Recommandée

### appsettings.json
```json
{
  "ApiGateway": {
    "BaseUrl": "http://apisix:9080"
  },
  "RabbitMQ": {
    "HostName": "rabbitmq",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "Exchange": "coservice"
  },
  "Consul": {
    "Enabled": true,
    "Address": "http://consul:8500",
    "ServiceName": "coservice",
    "ServiceId": "coservice-1",
    "ServiceAddress": "http://coservice:8700",
    "HealthCheck": {
      "Endpoint": "/sante",
      "Interval": 10,
      "Timeout": 5
    }
  }
}
```

---

## Notes Importantes

1. **APISIX gère le service discovery** : APISIX utilise le plugin consul-kv pour découvrir les services automatiquement
2. **RabbitMQ pour la synchronisation** : Plus besoin de polling HTTP, les événements arrivent en temps réel
3. **API Gateway comme point d'entrée** : Tous les appels HTTP passent par APISIX
4. **Consul reste pour l'enregistrement** : Le service s'enregistre dans Consul pour qu'APISIX puisse le découvrir via le plugin consul-kv
5. **APISIX Configuration** : Les routes et services sont configurés dans APISIX via etcd (configuration dynamique en temps réel)
6. **etcd** : APISIX utilise etcd comme backend de configuration pour la gestion dynamique des routes
