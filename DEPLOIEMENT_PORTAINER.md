# Guide de déploiement COService sur Portainer

Ce guide vous explique comment déployer le microservice COService sur Portainer depuis GitHub.

## Prérequis

- Portainer installé et accessible
- Accès à un registre Docker (Docker Hub, GitHub Container Registry, ou registre privé)
- Base de données SQL Server accessible depuis Portainer
- Accès au serveur Consul (si activé)

## Étape 1 : Créer et pousser l'image Docker

### Option A : Utiliser GitHub Actions (Recommandé)

Créez le fichier `.github/workflows/docker-build.yml` :

```yaml
name: Build and Push Docker Image

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@v2
      
      - name: Login to Docker Hub
        uses: docker/login-action@v2
        with:
          username: ${{ secrets.DOCKER_USERNAME }}
          password: ${{ secrets.DOCKER_PASSWORD }}
      
      - name: Build and push
        uses: docker/build-push-action@v4
        with:
          context: .
          push: true
          tags: votre-username/coservice:latest
```

### Option B : Build local et push manuel

```bash
# Build l'image
docker build -t votre-username/coservice:latest .

# Tag pour Docker Hub
docker tag votre-username/coservice:latest votre-username/coservice:latest

# Push vers Docker Hub
docker push votre-username/coservice:latest
```

## Étape 2 : Déployer sur Portainer

### 2.1. Créer une nouvelle stack dans Portainer

1. Connectez-vous à Portainer
2. Allez dans **Stacks** (ou **Applications** selon votre version)
3. Cliquez sur **Add stack**
4. Donnez un nom à votre stack : `coservice`

### 2.2. Configuration du déploiement

#### Option 1 : Déploiement depuis Git Repository (Recommandé)

1. Sélectionnez **Repository**
2. **Repository URL** : `https://github.com/Marnelle-dev/CoService.git`
3. **Repository reference** : `refs/heads/main`
4. **Compose path** : `docker-compose.yml`
5. **Auto-update** : Activez si vous voulez les mises à jour automatiques

#### Option 2 : Déploiement depuis Docker Compose (Web editor)

1. Sélectionnez **Web editor**
2. Collez le contenu de `docker-compose.yml`
3. Modifiez les variables d'environnement selon vos besoins

### 2.3. Configurer les variables d'environnement

Dans Portainer, ajoutez les variables d'environnement suivantes :

#### Variables obligatoires

```env
DB_CONNECTION_STRING=Server=votre-serveur-sql;Database=COServiceDb;User Id=votre-user;Password=votre-password;TrustServerCertificate=True;
```

#### Variables optionnelles

```env
CONSUL_ENABLED=true
CONSUL_ADDRESS=http://srv-guot-cont.gumar.local:8500
ASPNETCORE_ENVIRONMENT=Production
```

### 2.4. Configuration réseau

1. Créez un réseau Docker si nécessaire :
   - **Name** : `coservice-network`
   - **Driver** : `bridge`

2. Assurez-vous que le conteneur peut accéder à :
   - La base de données SQL Server
   - Le serveur Consul (si activé)

### 2.5. Configuration des ports

- **Port container** : `8700`
- **Port host** : `8700` (ou un autre port disponible)

### 2.6. Configuration du health check

Portainer utilisera le health check défini dans `docker-compose.yml` :
- Endpoint : `http://localhost:8700/sante`
- Interval : 30 secondes
- Timeout : 10 secondes
- Retries : 3

## Étape 3 : Déployer la stack

1. Cliquez sur **Deploy the stack**
2. Attendez que le conteneur démarre
3. Vérifiez les logs dans Portainer pour confirmer le démarrage

## Étape 4 : Vérification

### 4.1. Vérifier les logs

Dans Portainer :
1. Allez dans **Containers**
2. Sélectionnez le conteneur `coservice`
3. Cliquez sur **Logs**
4. Vérifiez qu'il n'y a pas d'erreurs

### 4.2. Tester l'API

```bash
# Health check
curl http://votre-serveur:8700/sante

# Swagger
http://votre-serveur:8700/swagger
```

### 4.3. Vérifier la connexion à la base de données

Les logs doivent afficher :
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand ...
```

## Étape 5 : Configuration Consul (Optionnel)

Si Consul est activé :

1. Vérifiez que le service est enregistré dans Consul
2. Accédez à l'interface Consul : `http://srv-guot-cont.gumar.local:8500`
3. Vérifiez que `coservice` apparaît dans les services

## Dépannage

### Le conteneur ne démarre pas

1. Vérifiez les logs dans Portainer
2. Vérifiez la chaîne de connexion à la base de données
3. Vérifiez que le port 8700 n'est pas déjà utilisé

### Erreur de connexion à la base de données

1. Vérifiez que SQL Server est accessible depuis le conteneur
2. Vérifiez les credentials dans la chaîne de connexion
3. Vérifiez que la base de données `COServiceDb` existe

### Erreur Consul

1. Vérifiez que Consul est accessible depuis le conteneur
2. Vérifiez l'adresse Consul dans les variables d'environnement
3. Désactivez Consul temporairement si nécessaire : `CONSUL_ENABLED=false`

## Mise à jour

### Mise à jour manuelle

1. Dans Portainer, allez dans **Stacks**
2. Sélectionnez `coservice`
3. Cliquez sur **Editor**
4. Mettez à jour l'image ou le docker-compose.yml
5. Cliquez sur **Update the stack**

### Mise à jour automatique (si configuré)

Portainer mettra à jour automatiquement la stack lors des push sur GitHub.

## Variables d'environnement complètes

```env
# Base de données
DB_CONNECTION_STRING=Server=sql-server;Database=COServiceDb;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;

# Application
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8700

# Consul
CONSUL_ENABLED=true
CONSUL_ADDRESS=http://srv-guot-cont.gumar.local:8500
CONSUL_SERVICE_NAME=coservice
CONSUL_SERVICE_ID=coservice-1
CONSUL_SERVICE_ADDRESS=http://coservice:8700
```

## Sécurité

- Ne commitez jamais les mots de passe dans le code
- Utilisez les secrets de Portainer pour les informations sensibles
- Activez HTTPS en production
- Configurez un reverse proxy (nginx, traefik) si nécessaire

