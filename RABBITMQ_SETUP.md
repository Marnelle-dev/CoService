# Configuration RabbitMQ pour COService

## ✅ Ce qui est créé automatiquement par l'application

L'application crée automatiquement :
- **Exchange** : `coservice` (type Topic, durable)
- **Queues** :
  - `coservice.partenaires` (bindée à `partenaire.*`)
  - `coservice.exportateurs` (bindée à `exportateur.*`)
  - `coservice.referentiels` (bindée à `referentiel.*`)

## 🔧 Ce qu'il faut vérifier/créer dans RabbitMQ

### 1. Vérifier que l'utilisateur existe

1. Connecte-toi à l'interface web RabbitMQ : `http://192.168.2.119:15672`
2. Va dans l'onglet **"Admin"** → **"Users"**
3. Vérifie que l'utilisateur **`sysguot`** existe

### 2. Si l'utilisateur n'existe pas, le créer

Dans l'interface web RabbitMQ :
1. Clique sur **"Add a user"**
2. Renseigne :
   - **Username** : `sysguot`
   - **Password** : `MyS3cur3Passwor_d`
   - **Tags** : Laisse vide ou ajoute `administrator` si nécessaire

### 3. Vérifier les permissions sur le VirtualHost "/"

1. Va dans **"Admin"** → **"Users"**
2. Clique sur l'utilisateur **`sysguot`**
3. Vérifie que dans la section **"Virtual Host Permissions"**, le VirtualHost **"/"** est listé avec les permissions :
   - **Configure** : `.*`
   - **Write** : `.*`
   - **Read** : `.*`

### 4. Si les permissions n'existent pas, les ajouter

1. Va dans **"Admin"** → **"Virtual Hosts"**
2. Vérifie que le VirtualHost **"/"** existe (il devrait exister par défaut)
3. Clique sur **"/"** → **"Permissions"**
4. Clique sur **"Add / set permission"**
5. Sélectionne l'utilisateur **`sysguot`**
6. Configure les permissions :
   - **Configure regexp** : `.*`
   - **Write regexp** : `.*`
   - **Read regexp** : `.*`
7. Clique sur **"Set permission"**

## 📋 Résumé des permissions nécessaires

L'utilisateur `sysguot` doit avoir sur le VirtualHost `/` :
- ✅ **Configure** : `.*` (pour créer des exchanges, queues, bindings)
- ✅ **Write** : `.*` (pour publier des messages)
- ✅ **Read** : `.*` (pour consommer des messages)

## 🧪 Test de connexion

Une fois configuré, redémarre l'API. Tu devrais voir dans les logs :
```
Connexion RabbitMQ établie : 192.168.2.119:5672, Exchange: coservice
Consumer configuré pour la queue : coservice.partenaires
Consumer configuré pour la queue : coservice.exportateurs
Consumer configuré pour la queue : coservice.referentiels
```

## ⚠️ Erreurs courantes

### "Access refused"
- **Cause** : L'utilisateur n'a pas les permissions sur le VirtualHost
- **Solution** : Vérifier les permissions comme indiqué ci-dessus

### "Login failed"
- **Cause** : Mauvais mot de passe ou utilisateur inexistant
- **Solution** : Vérifier les identifiants dans `appsettings.json` et dans RabbitMQ

### "Virtual host not found"
- **Cause** : Le VirtualHost "/" n'existe pas
- **Solution** : Le créer dans **"Admin"** → **"Virtual Hosts"** → **"Add a new virtual host"**
