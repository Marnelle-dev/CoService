# Événements RabbitMQ - COService

## Vue d'ensemble

COService utilise RabbitMQ pour la communication asynchrone avec les autres microservices.
Les événements sont échangés via un Exchange nommé `coservice`.

---

## Événements Consommés (Reçus par COService)

### Organisations (Enrolement Service)

#### `partenaire.creé`
**Routing Key**: `partenaire.creé`  
**Queue**: `coservice.partenaires`  
**Description**: Événement émis lorsqu'un partenaire (Chambre de Commerce) est créé dans le service Enrolement.

**Payload**:
```json
{
  "id": "guid",
  "codePartenaire": "string",
  "nom": "string",
  "adresse": "string?",
  "telephone": "string?",
  "email": "string?",
  "typePartenaireId": "guid?",
  "departementId": "guid?",
  "actif": true,
  "timestamp": "datetime"
}
```

**Action**: Créer ou mettre à jour le partenaire dans la base de données locale.

---

#### `partenaire.modifié`
**Routing Key**: `partenaire.modifié`  
**Queue**: `coservice.partenaires`  
**Description**: Événement émis lorsqu'un partenaire est modifié.

**Payload**: Identique à `partenaire.creé`

**Action**: Mettre à jour le partenaire dans la base de données locale.

---

#### `partenaire.supprimé`
**Routing Key**: `partenaire.supprimé`  
**Queue**: `coservice.partenaires`  
**Description**: Événement émis lorsqu'un partenaire est supprimé.

**Payload**:
```json
{
  "id": "guid",
  "timestamp": "datetime"
}
```

**Action**: Marquer le partenaire comme inactif ou le supprimer (selon la stratégie).

---

#### `exportateur.creé`
**Routing Key**: `exportateur.creé`  
**Queue**: `coservice.exportateurs`  
**Description**: Événement émis lorsqu'un exportateur est créé.

**Payload**:
```json
{
  "id": "guid",
  "codeExportateur": "string",
  "nom": "string",
  "raisonSociale": "string?",
  "niu": "string?",
  "rccm": "string?",
  "codeActivite": "string?",
  "adresse": "string?",
  "telephone": "string?",
  "email": "string?",
  "partenaireId": "guid?",
  "departementId": "guid?",
  "typeExportateur": "int?",
  "actif": true,
  "timestamp": "datetime"
}
```

**Action**: Créer ou mettre à jour l'exportateur dans la base de données locale.

---

#### `exportateur.modifié`
**Routing Key**: `exportateur.modifié`  
**Queue**: `coservice.exportateurs`  
**Description**: Événement émis lorsqu'un exportateur est modifié.

**Payload**: Identique à `exportateur.creé`

**Action**: Mettre à jour l'exportateur dans la base de données locale.

---

#### `exportateur.supprimé`
**Routing Key**: `exportateur.supprimé`  
**Queue**: `coservice.exportateurs`  
**Description**: Événement émis lorsqu'un exportateur est supprimé.

**Payload**:
```json
{
  "id": "guid",
  "timestamp": "datetime"
}
```

**Action**: Marquer l'exportateur comme inactif ou le supprimer.

---

### Référentiels (Referentiel Service)

#### `referentiel.pays.mis-a-jour`
**Routing Key**: `referentiel.pays.mis-a-jour`  
**Queue**: `coservice.referentiels`  
**Description**: Événement émis lors de la mise à jour des pays.

**Payload**:
```json
{
  "pays": [
    {
      "id": "guid",
      "code": "string",
      "nom": "string",
      "actif": true
    }
  ],
  "timestamp": "datetime"
}
```

**Action**: Synchroniser les pays dans la base de données locale.

---

#### `referentiel.port.mis-a-jour`
**Routing Key**: `referentiel.port.mis-a-jour`  
**Queue**: `coservice.referentiels`  
**Description**: Événement émis lors de la mise à jour des ports.

**Payload**: Liste de ports avec leurs propriétés.

**Action**: Synchroniser les ports dans la base de données locale.

---

#### `referentiel.devise.mis-a-jour`
**Routing Key**: `referentiel.devise.mis-a-jour`  
**Queue**: `coservice.referentiels`  
**Description**: Événement émis lors de la mise à jour des devises.

**Payload**: Liste de devises avec leurs propriétés.

**Action**: Synchroniser les devises dans la base de données locale.

---

*(Autres événements référentiels similaires : module, incoterm, bureau-dedouanement, unite-statistique, position-tarifaire, aeroport, departement)*

---

## Événements Publiés (Envoyés par COService)

### Certificats

#### `certificat.creé`
**Routing Key**: `certificat.creé`  
**Exchange**: `coservice`  
**Description**: Événement émis lorsqu'un certificat est créé.

**Payload**:
```json
{
  "certificatId": "guid",
  "certificateNo": "string",
  "exportateurId": "guid",
  "partenaireId": "guid?",
  "statut": "string",
  "timestamp": "datetime"
}
```

**Consommateurs**: NotificationService, FacturationService

---

#### `certificat.statut.changé`
**Routing Key**: `certificat.statut.changé`  
**Exchange**: `coservice`  
**Description**: Événement émis lorsqu'un certificat change de statut.

**Payload**:
```json
{
  "certificatId": "guid",
  "ancienStatut": "string",
  "nouveauStatut": "string",
  "timestamp": "datetime"
}
```

**Consommateurs**: NotificationService

---

#### `certificat.validé`
**Routing Key**: `certificat.validé`  
**Exchange**: `coservice`  
**Description**: Événement émis lorsqu'un certificat est validé.

**Payload**:
```json
{
  "certificatId": "guid",
  "certificateNo": "string",
  "timestamp": "datetime"
}
```

**Consommateurs**: NotificationService, DocumentService (génération PDF)

---

#### `certificat.rejeté`
**Routing Key**: `certificat.rejeté`  
**Exchange**: `coservice`  
**Description**: Événement émis lorsqu'un certificat est rejeté.

**Payload**:
```json
{
  "certificatId": "guid",
  "certificateNo": "string",
  "raison": "string",
  "timestamp": "datetime"
}
```

**Consommateurs**: NotificationService

---

### Notifications

#### `notification.demande`
**Routing Key**: `notification.demande`  
**Exchange**: `coservice`  
**Description**: Demande d'envoi de notification.

**Payload**:
```json
{
  "type": "string", // "email", "sms", "push"
  "destinataire": "string",
  "sujet": "string",
  "corps": "string",
  "timestamp": "datetime"
}
```

**Consommateurs**: NotificationService

---

## Configuration RabbitMQ

### Structure des Exchanges et Queues

```
Exchange: coservice (topic)
  ├── Queue: coservice.partenaires
  │   └── Routing Keys: partenaire.*
  ├── Queue: coservice.exportateurs
  │   └── Routing Keys: exportateur.*
  ├── Queue: coservice.referentiels
  │   └── Routing Keys: referentiel.*
  └── Queue: coservice.notifications (pour recevoir les réponses)
```

### Durabilité

- **Exchanges**: Durables
- **Queues**: Durables
- **Messages**: Persistent (delivery mode = 2)

### Acknowledgment

- **Mode**: Manual acknowledgment
- **Reconnaissance**: Après traitement réussi
- **Rejet**: En cas d'erreur, message retourné à la queue ou envoyé à une DLQ (Dead Letter Queue)

---

## Implémentation

### Packages NuGet Requis

```xml
<PackageReference Include="RabbitMQ.Client" Version="6.8.1" />
<PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
```

### Structure des Handlers

```
COService.Infrastructure/
  └── Messaging/
      ├── RabbitMQClient.cs (Client RabbitMQ)
      ├── Handlers/
      │   ├── PartenaireEventHandler.cs
      │   ├── ExportateurEventHandler.cs
      │   └── ReferentielEventHandler.cs
      └── Publishers/
          └── CertificateEventPublisher.cs
```

---

## Notes Importantes

1. **Idempotence**: Les handlers doivent être idempotents (gérer les doublons)
2. **Retry**: Implémenter une stratégie de retry pour les échecs
3. **DLQ**: Configurer une Dead Letter Queue pour les messages en échec
4. **Monitoring**: Surveiller les queues et les taux de traitement
5. **Versioning**: Gérer la compatibilité des versions de payload
