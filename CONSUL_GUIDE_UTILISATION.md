# Guide d'Utilisation de Consul avec COService

## Installation de Consul

### Option 1 : Téléchargement Direct
1. Téléchargez Consul depuis : https://www.consul.io/downloads
2. Extrayez l'archive
3. Ajoutez le répertoire au PATH

### Option 2 : Docker
```bash
docker run -d --name=consul -p 8500:8500 consul
```

### Option 3 : Chocolatey (Windows)
```powershell
choco install consul
```

## Démarrer Consul

### Mode Développement (Standalone)
```bash
consul agent -dev
```

Consul sera accessible sur : `http://localhost:8500`

### Interface Web
Une fois Consul démarré, accédez à l'interface web :
- URL : `http://localhost:8500/ui`
- Vous verrez les services enregistrés, leur santé, etc.

## Configuration dans COService

### 1. Configuration dans appsettings.json

La configuration Consul est déjà présente dans `appsettings.json` :

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

### 2. Fonctionnement Automatique

Lorsque vous démarrez COService :
1. ✅ Le service s'enregistre automatiquement dans Consul
2. ✅ Consul vérifie périodiquement la santé via `/health`
3. ✅ Si le service tombe, Consul le retire automatiquement

## Découverte de Services

### Utilisation dans le Code

Pour découvrir un autre microservice (ex: AuthService) :

```csharp
public class AuthServiceClient
{
    private readonly IServiceDiscovery _serviceDiscovery;
    private readonly HttpClient _httpClient;

    public AuthServiceClient(IServiceDiscovery serviceDiscovery, HttpClient httpClient)
    {
        _serviceDiscovery = serviceDiscovery;
        _httpClient = httpClient;
    }

    public async Task<UserDto> GetUserAsync(Guid userId)
    {
        // Découvrir l'adresse d'AuthService via Consul
        var authServiceUrl = await _serviceDiscovery.DiscoverServiceAsync("auth-service");
        
        if (string.IsNullOrEmpty(authServiceUrl))
        {
            throw new Exception("AuthService non disponible");
        }

        // Appeler AuthService
        var response = await _httpClient.GetAsync($"{authServiceUrl}/api/users/{userId}");
        // ...
    }
}
```

## Vérification

### 1. Vérifier que Consul est démarré
```bash
curl http://localhost:8500/v1/status/leader
```

### 2. Vérifier que COService est enregistré
```bash
curl http://localhost:8500/v1/agent/services
```

Vous devriez voir `coservice` dans la liste.

### 3. Vérifier la santé du service
```bash
curl http://localhost:8500/v1/health/service/coservice
```

### 4. Interface Web
Ouvrez `http://localhost:8500/ui` et vérifiez :
- Services → `coservice` doit être présent
- Health → Le health check doit être vert

## Avantages pour COService

✅ **Découverte automatique** : Plus besoin de hardcoder les URLs des microservices
✅ **Haute disponibilité** : Si un service tombe, Consul le retire automatiquement
✅ **Load balancing** : Si plusieurs instances d'un service existent, Consul peut les distribuer
✅ **Monitoring** : Consul surveille la santé de tous les services

## Prochaines Étapes

1. **Démarrer Consul** : `consul agent -dev`
2. **Démarrer COService** : Le service s'enregistrera automatiquement
3. **Vérifier dans l'UI** : `http://localhost:8500/ui`
4. **Utiliser la découverte** : Dans les clients de microservices, utiliser `IServiceDiscovery`

## Notes Importantes

- ⚠️ En développement, utilisez `consul agent -dev` (mode standalone)
- ⚠️ En production, configurez un cluster Consul pour la haute disponibilité
- ⚠️ Le `ServiceId` doit être unique pour chaque instance (ex: `coservice-1`, `coservice-2`)
- ⚠️ Le `ServiceAddress` doit correspondre à l'URL réelle du service

## Dépannage

### Le service ne s'enregistre pas
- Vérifiez que Consul est démarré : `http://localhost:8500`
- Vérifiez la configuration dans `appsettings.json`
- Vérifiez les logs de l'application

### Le health check échoue
- Vérifiez que l'endpoint `/health` est accessible
- Vérifiez la configuration du health check dans `appsettings.json`

### Impossible de découvrir un service
- Vérifiez que le service est enregistré dans Consul
- Vérifiez que le nom du service correspond (ex: `auth-service`)
- Vérifiez que le service est en bonne santé (health check vert)

