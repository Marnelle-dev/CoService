# Configuration Apache APISIX pour COService

## Vue d'ensemble

Apache APISIX est utilisé comme API Gateway pour router les requêtes vers les microservices.
APISIX utilise Consul pour la découverte de services via le plugin `consul-kv` et etcd pour la configuration dynamique.

**Caractéristiques principales** :
- Configuration dynamique en temps réel via etcd
- Service discovery via Consul
- Load balancing automatique
- Plugins extensibles (JWT, rate limiting, logging, etc.)
- Haute performance et faible latence

---

## Architecture APISIX

```
Client → APISIX:9080 → Upstream (Service) → Response
```

**Ports APISIX** :
- **9080** : Port proxy (requêtes entrantes)
- **9180** : Admin API (configuration)
- **9443** : Port proxy HTTPS
- **9091** : Prometheus metrics

---

## Configuration via etcd

APISIX utilise **etcd** comme backend de configuration. Toutes les routes, services et plugins sont stockés dans etcd.

### Structure dans etcd

```
/apisix/
  ├── routes/          # Routes configurées
  ├── services/        # Services (upstreams)
  ├── upstreams/      # Upstreams avec load balancing
  └── plugins/        # Plugins globaux
```

---

## Configuration des Services

### 1. Service: Enrolement

#### Via Admin API
```bash
curl http://apisix:9180/apisix/admin/services/enrolement-service \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "upstream": {
      "type": "roundrobin",
      "nodes": {
        "enrolement-service:8080": 1
      }
    }
  }'
```

#### Via etcd (direct)
```json
{
  "key": "/apisix/services/enrolement-service",
  "value": {
    "upstream": {
      "type": "roundrobin",
      "nodes": {
        "enrolement-service:8080": 1
      }
    }
  }
}
```

### 2. Service: Referentiel

```bash
curl http://apisix:9180/apisix/admin/services/referentiel-service \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "upstream": {
      "type": "roundrobin",
      "nodes": {
        "referentiel-service:8080": 1
      }
    }
  }'
```

### 3. Service: COService

```bash
curl http://apisix:9180/apisix/admin/services/coservice \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "upstream": {
      "type": "roundrobin",
      "nodes": {
        "coservice:8700": 1
      }
    }
  }'
```

---

## Configuration des Routes

### Route: Enrolement

```bash
curl http://apisix:9180/apisix/admin/routes/enrolement-route \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "uri": "/api/enrolement/*",
    "methods": ["GET", "POST", "PUT", "DELETE", "PATCH"],
    "service_id": "enrolement-service",
    "plugins": {
      "proxy-rewrite": {
        "regex_uri": ["^/api/enrolement/(.*)", "/$1"]
      }
    }
  }'
```

### Route: Referentiel

```bash
curl http://apisix:9180/apisix/admin/routes/referentiel-route \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "uri": "/api/referentiel/*",
    "methods": ["GET", "POST", "PUT", "DELETE", "PATCH"],
    "service_id": "referentiel-service",
    "plugins": {
      "proxy-rewrite": {
        "regex_uri": ["^/api/referentiel/(.*)", "/$1"]
      }
    }
  }'
```

### Route: COService

```bash
curl http://apisix:9180/apisix/admin/routes/coservice-route \
  -H 'X-API-KEY: your-api-key' \
  -X PUT -d '{
    "uri": "/api/coservice/*",
    "methods": ["GET", "POST", "PUT", "DELETE", "PATCH"],
    "service_id": "coservice",
    "plugins": {
      "proxy-rewrite": {
        "regex_uri": ["^/api/coservice/(.*)", "/$1"]
      },
      "limit-req": {
        "rate": 100,
        "burst": 50,
        "key": "remote_addr"
      }
    }
  }'
```

---

## Service Discovery avec Consul

APISIX peut utiliser Consul pour la découverte de services via le plugin `consul-kv`.

### Configuration du plugin consul-kv

```bash
curl http://apisix:9180/apisix/admin/routes/enrolement-route \
  -H 'X-API-KEY: your-api-key' \
  -X PATCH -d '{
    "plugins": {
      "consul-kv": {
        "prefix": "upstreams/enrolement-service",
        "fetch_interval": 3
      }
    }
  }'
```

**Configuration dans Consul** :
```bash
# Stocker la configuration dans Consul KV
curl -X PUT http://consul:8500/v1/kv/upstreams/enrolement-service \
  -d '{
    "host": "enrolement-service",
    "port": 8080,
    "weight": 100
  }'
```

APISIX récupère automatiquement les services depuis Consul et met à jour les upstreams en temps réel.

---

## Authentification (JWT)

### Activer le plugin JWT

```bash
curl http://apisix:9180/apisix/admin/routes/enrolement-route \
  -H 'X-API-KEY: your-api-key' \
  -X PATCH -d '{
    "plugins": {
      "jwt-auth": {
        "key": "user-key",
        "secret": "my-secret-key"
      }
    }
  }'
```

**Utilisation** :
```bash
# Obtenir un token
curl http://apisix:9080/apisix/plugin/jwt/sign?key=user-key

# Utiliser le token
curl http://apisix:9080/api/enrolement/api/partenaires \
  -H 'Authorization: Bearer <token>'
```

---

## Rate Limiting

### Plugin limit-req

```bash
curl http://apisix:9180/apisix/admin/routes/coservice-route \
  -H 'X-API-KEY: your-api-key' \
  -X PATCH -d '{
    "plugins": {
      "limit-req": {
        "rate": 100,
        "burst": 50,
        "key": "remote_addr",
        "rejected_code": 429
      }
    }
  }'
```

---

## Configuration Déclarative (YAML)

Pour une configuration versionnée, utiliser la configuration déclarative :

### config.yaml
```yaml
apisix:
  node_listen: 9080
  admin_key:
    - name: admin
      key: your-api-key
      role: admin

etcd:
  host:
    - http://etcd:2379
  prefix: /apisix

plugins:
  - real-ip
  - client-control
  - proxy-control
  - request-id
  - zipkin
  - ext-plugin-pre-req
  - fault-injection
  - serverless-pre-function
  - serverless-post-function
  - openid-connect
  - casbin
  - authz-casbin
  - wolf-rbac
  - ldap-auth
  - hmac-auth
  - basic-auth
  - jwt-auth
  - oauth
  - key-auth
  - consumer-restriction
  - authz-keycloak
  - ip-restriction
  - ua-restriction
  - referer-restriction
  - cors
  - csrf
  - uri-blocker
  - server-info
  - traffic-split
  - request-validation
  - openapi
  - api-breaker
  - limit-req
  - limit-count
  - limit-conn
  - grpc-transcode
  - serverless-pre-function
  - serverless-post-function
  - redirect
  - post-propagation
  - grpc-web
  - batch-requests
  - websocket
  - dubbo-proxy
  - http-logger
  - tcp-logger
  - udp-logger
  - kafka-logger
  - rocketmq-logger
  - skywalking-logger
  - opentelemetry
  - zipkin
  - prometheus
  - file-logger
  - loggly
  - splunk-hec-logging
  - google-cloud-logging
  - sls-logger
  - datadog
  - loki-logger
  - node-status
  - example-plugin
  - echo
  - http-to-https
  - real-ip
  - ip-restriction
  - serverless-pre-function
  - serverless-post-function
  - ext-plugin-pre-req
  - ext-plugin-post-req
  - ext-plugin-post-resp
  - ext-plugin-log
  - opentelemetry
  - opa
  - forward-proxy
  - opentelemetry
  - opa
  - forward-proxy
```

---

## URLs des Services depuis COService

Depuis COService, les appels vers les autres services doivent utiliser APISIX :

### Configuration dans appsettings.json
```json
{
  "ApiGateway": {
    "BaseUrl": "http://apisix:9080"
  },
  "ExternalServices": {
    "EnrolementService": {
      "Path": "/api/enrolement",
      "Timeout": 30
    },
    "ReferentielService": {
      "Path": "/api/referentiel",
      "Timeout": 30
    }
  }
}
```

### Exemple d'appel
```
GET http://apisix:9080/api/enrolement/api/partenaires
Headers:
  Authorization: Bearer <JWT_TOKEN>
```

APISIX route automatiquement vers le service Enrolement.

---

## Monitoring et Observabilité

### Prometheus Metrics

APISIX expose des métriques Prometheus sur le port 9091 :

```bash
curl http://apisix:9091/apisix/prometheus/metrics
```

### Plugins de Logging

#### HTTP Logger
```bash
curl http://apisix:9180/apisix/admin/routes/coservice-route \
  -H 'X-API-KEY: your-api-key' \
  -X PATCH -d '{
    "plugins": {
      "http-logger": {
        "uri": "http://log-service:8080/logs",
        "timeout": 3,
        "name": "http logger",
        "batch_max_size": 1
      }
    }
  }'
```

#### Kafka Logger
```bash
curl http://apisix:9180/apisix/admin/routes/coservice-route \
  -H 'X-API-KEY: your-api-key' \
  -X PATCH -d '{
    "plugins": {
      "kafka-logger": {
        "broker_list": {
          "kafka:9092": 1
        },
        "topic": "apisix-logs",
        "key": "coservice"
      }
    }
  }'
```

---

## Health Checks

APISIX peut utiliser les health checks de Consul pour déterminer si un service est disponible.

**Configuration dans Consul** :
- Le service s'enregistre avec un health check
- APISIX (via plugin consul-kv) vérifie le statut dans Consul
- Seules les instances "healthy" sont utilisées

---

## Notes Importantes

1. **APISIX utilise etcd** : La configuration est stockée dans etcd pour une mise à jour en temps réel
2. **Service Discovery** : Le plugin consul-kv permet à APISIX de découvrir les services automatiquement
3. **Routes avec proxy-rewrite** : Le plugin proxy-rewrite permet de réécrire les URLs
4. **Health Checks** : APISIX respecte les health checks de Consul
5. **Load Balancing** : APISIX fait du load balancing automatique entre les instances d'un service
6. **Configuration dynamique** : Les changements dans etcd sont appliqués immédiatement sans redémarrage

---

## Exemple de Requête Complète

### Depuis COService vers Enrolement
```
GET http://apisix:9080/api/enrolement/api/partenaires
Headers:
  Authorization: Bearer <JWT_TOKEN>
```

### Traitement par APISIX
1. APISIX reçoit la requête sur le port 9080
2. APISIX match la route `/api/enrolement/*`
3. APISIX vérifie le JWT (si plugin activé)
4. APISIX applique le rate limiting (si plugin activé)
5. APISIX découvre l'instance Enrolement via Consul (si plugin consul-kv activé)
6. APISIX route vers `http://enrolement-service:8080/api/partenaires` (proxy-rewrite retire `/api/enrolement`)
7. APISIX retourne la réponse au client

---

## Installation et Démarrage

### Docker Compose
```yaml
version: '3'
services:
  etcd:
    image: bitnami/etcd:latest
    environment:
      - ALLOW_NONE_AUTHENTICATION=yes
    ports:
      - "2379:2379"

  apisix:
    image: apache/apisix:latest
    ports:
      - "9080:9080"
      - "9180:9180"
      - "9091:9091"
    volumes:
      - ./config.yaml:/usr/local/apisix/conf/config.yaml:ro
    depends_on:
      - etcd
```

### Configuration de base
```yaml
# config.yaml
apisix:
  node_listen: 9080
  admin_key:
    - name: admin
      key: your-api-key
      role: admin

etcd:
  host:
    - http://etcd:2379
  prefix: /apisix
```
