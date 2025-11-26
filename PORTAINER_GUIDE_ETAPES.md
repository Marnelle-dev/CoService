# Guide étape par étape : Déploiement COService sur Portainer

## 📋 Prérequis

- ✅ Portainer installé et accessible
- ✅ Accès à GitHub (repository : `https://github.com/Marnelle-dev/CoService.git`)
- ✅ Base de données SQL Server accessible
- ✅ Accès réseau au serveur Consul (si activé)

---

## 🚀 ÉTAPE 1 : Préparer le dépôt GitHub

### 1.1. Vérifier les fichiers Docker

Assurez-vous que ces fichiers sont dans votre dépôt :
- ✅ `Dockerfile`
- ✅ `.dockerignore`
- ✅ `docker-compose.yml`

### 1.2. Pousser les modifications

```bash
git add Dockerfile .dockerignore docker-compose.yml
git commit -m "feat: Ajout des fichiers Docker pour déploiement Portainer"
git push origin main
```

---

## 🐳 ÉTAPE 2 : Accéder à Portainer

1. Ouvrez votre navigateur
2. Accédez à l'URL de Portainer (ex: `http://votre-serveur:9000`)
3. Connectez-vous avec vos identifiants

---

## 📦 ÉTAPE 3 : Créer une nouvelle Stack

### 3.1. Navigation

1. Dans le menu de gauche, cliquez sur **Stacks**
2. Cliquez sur le bouton **+ Add stack**
3. Donnez un nom à votre stack : `coservice`

### 3.2. Choisir la méthode de déploiement

**Option A : Depuis Git Repository (Recommandé)**

1. Sélectionnez **Repository**
2. Remplissez les champs :
   - **Repository URL** : `https://github.com/Marnelle-dev/CoService.git`
   - **Repository reference** : `refs/heads/main`
   - **Compose path** : `docker-compose.yml`
   - **Auto-update** : ✅ Activé (optionnel, pour les mises à jour automatiques)

**Option B : Depuis Web Editor**

1. Sélectionnez **Web editor**
2. Collez le contenu du fichier `docker-compose.yml`
3. Modifiez selon vos besoins

---

## ⚙️ ÉTAPE 4 : Configurer les variables d'environnement

Dans la section **Environment variables**, ajoutez :

### Variables obligatoires

```env
DB_CONNECTION_STRING=Server=192.168.2.118;Database=GUOT_TE_PROD;User ID=msuser;Password=9$SViSWexRn5hWq;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;
```

**⚠️ Important** : Remplacez les valeurs par vos propres identifiants de base de données.

### Variables optionnelles

```env
CONSUL_ENABLED=true
CONSUL_ADDRESS=http://srv-guot-cont.gumar.local:8500
ASPNETCORE_ENVIRONMENT=Production
```

---

## 🌐 ÉTAPE 5 : Configurer le réseau

### 5.1. Créer un réseau (si nécessaire)

1. Dans Portainer, allez dans **Networks**
2. Cliquez sur **Add network**
3. Nom : `coservice-network`
4. Driver : `bridge`
5. Cliquez sur **Create the network**

### 5.2. Vérifier la connectivité

Assurez-vous que le conteneur pourra accéder à :
- ✅ SQL Server (192.168.2.118:1433)
- ✅ Consul (srv-guot-cont.gumar.local:8500)

---

## 🔌 ÉTAPE 6 : Configurer les ports

Dans la section **Port mapping** :

- **Container port** : `8700`
- **Host port** : `8700` (ou un autre port disponible)

**Exemple** : Si le port 8700 est déjà utilisé, utilisez `8701:8700`

---

## 🚀 ÉTAPE 7 : Déployer la stack

1. Vérifiez toutes les configurations
2. Cliquez sur **Deploy the stack**
3. Attendez que Portainer télécharge l'image et démarre le conteneur

**⏱️ Temps estimé** : 2-5 minutes selon la vitesse de téléchargement

---

## ✅ ÉTAPE 8 : Vérifier le déploiement

### 8.1. Vérifier les logs

1. Allez dans **Stacks** → `coservice`
2. Cliquez sur le conteneur `coservice`
3. Onglet **Logs**
4. Vérifiez qu'il n'y a pas d'erreurs

**Logs attendus** :
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8700
```

### 8.2. Tester l'API

**Health Check** :
```bash
curl http://votre-serveur:8700/sante
```

**Swagger** :
```
http://votre-serveur:8700/swagger
```

### 8.3. Vérifier Consul (si activé)

1. Accédez à : `http://srv-guot-cont.gumar.local:8500`
2. Allez dans **Services**
3. Vérifiez que `coservice` apparaît dans la liste

---

## 🔧 ÉTAPE 9 : Configuration avancée (Optionnel)

### 9.1. Variables d'environnement complètes

Si vous voulez personnaliser davantage :

```env
# Base de données
DB_CONNECTION_STRING=Server=192.168.2.118;Database=GUOT_TE_PROD;User ID=msuser;Password=9$SViSWexRn5hWq;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;

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

### 9.2. Health Check

Le health check est automatiquement configuré dans `docker-compose.yml` :
- Endpoint : `/sante`
- Interval : 30 secondes
- Timeout : 10 secondes

---

## 🔄 ÉTAPE 10 : Mise à jour de l'application

### Mise à jour manuelle

1. Dans **Stacks** → `coservice`
2. Cliquez sur **Editor**
3. Modifiez le `docker-compose.yml` ou les variables d'environnement
4. Cliquez sur **Update the stack**

### Mise à jour automatique (si Auto-update activé)

Portainer mettra à jour automatiquement la stack lors des push sur GitHub.

---

## 🐛 Dépannage

### ❌ Le conteneur ne démarre pas

**Solution** :
1. Vérifiez les logs dans Portainer
2. Vérifiez la chaîne de connexion à la base de données
3. Vérifiez que le port 8700 n'est pas déjà utilisé

### ❌ Erreur de connexion à la base de données

**Solution** :
1. Vérifiez que SQL Server est accessible depuis le conteneur
2. Testez la connexion avec `sqlcmd` depuis le conteneur
3. Vérifiez les credentials dans la chaîne de connexion
4. Vérifiez que la base de données existe

**Commande de test** :
```bash
# Depuis Portainer, exécutez une commande dans le conteneur
sqlcmd -S 192.168.2.118 -U msuser -P '9$SViSWexRn5hWq' -Q "SELECT 1"
```

### ❌ Erreur Consul

**Solution** :
1. Vérifiez que Consul est accessible : `curl http://srv-guot-cont.gumar.local:8500`
2. Vérifiez l'adresse Consul dans les variables d'environnement
3. Désactivez Consul temporairement : `CONSUL_ENABLED=false`

### ❌ Port déjà utilisé

**Solution** :
1. Changez le port host dans le mapping de ports
2. Exemple : `8701:8700` au lieu de `8700:8700`
3. Accédez à l'API via le nouveau port

---

## 📊 Monitoring

### Vérifier les métriques

1. Dans Portainer, allez dans **Containers**
2. Sélectionnez `coservice`
3. Onglet **Stats** pour voir :
   - Utilisation CPU
   - Utilisation mémoire
   - Utilisation réseau

### Logs en temps réel

1. Onglet **Logs**
2. Cliquez sur **Auto-refresh** pour voir les logs en temps réel

---

## 🔒 Sécurité

### Bonnes pratiques

1. ✅ Ne commitez jamais les mots de passe dans le code
2. ✅ Utilisez les secrets de Portainer pour les informations sensibles
3. ✅ Activez HTTPS en production
4. ✅ Configurez un reverse proxy (nginx, traefik) si nécessaire
5. ✅ Limitez l'accès réseau aux services nécessaires

### Utiliser les secrets Portainer

1. Allez dans **Secrets**
2. Créez un secret pour votre chaîne de connexion
3. Référencez-le dans `docker-compose.yml` :

```yaml
environment:
  - ConnectionStrings__chaine=/run/secrets/db_connection_string
secrets:
  - db_connection_string
```

---

## 📝 Checklist de déploiement

- [ ] Fichiers Docker créés et poussés sur GitHub
- [ ] Stack créée dans Portainer
- [ ] Variables d'environnement configurées
- [ ] Réseau configuré
- [ ] Ports mappés
- [ ] Stack déployée
- [ ] Logs vérifiés (pas d'erreurs)
- [ ] Health check fonctionnel (`/sante`)
- [ ] Swagger accessible (`/swagger`)
- [ ] Consul enregistré (si activé)
- [ ] Base de données accessible

---

## 🎉 Félicitations !

Votre microservice COService est maintenant déployé sur Portainer ! 🚀

Pour toute question ou problème, consultez les logs dans Portainer ou le fichier `DEPLOIEMENT_PORTAINER.md`.

