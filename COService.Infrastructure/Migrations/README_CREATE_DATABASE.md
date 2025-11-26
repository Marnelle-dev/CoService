# Script de création de la base de données COService

## Description

Ce script SQL (`CreateDatabase.sql`) permet de créer la base de données **COService** et toutes ses tables sur SQL Server.

## Tables créées

1. **certificate_types** - Types de certificats d'origine (EUR.1, Formule A, etc.)
2. **certificates** - Certificats d'origine
3. **abonnements** - Abonnements créés par la chambre de commerce
4. **certificate_lines** - Lignes de produits des certificats
5. **certificate_validations** - Validations des certificats
6. **commentaires** - Commentaires sur les certificats

## Utilisation

### Option 1 : Via SQL Server Management Studio (SSMS)

1. Ouvrez SQL Server Management Studio
2. Connectez-vous à votre instance SQL Server
3. Ouvrez le fichier `CreateDatabase.sql`
4. Exécutez le script (F5 ou bouton Exécuter)

### Option 2 : Via sqlcmd (ligne de commande)

```bash
sqlcmd -S votre_serveur -U votre_utilisateur -P votre_mot_de_passe -i CreateDatabase.sql
```

### Option 3 : Via Azure Data Studio

1. Ouvrez Azure Data Studio
2. Connectez-vous à votre instance SQL Server
3. Ouvrez le fichier `CreateDatabase.sql`
4. Exécutez le script

## Structure des relations

```
certificate_types (1) ──< (N) certificates
abonnements (1) ──< (N) certificates
certificates (1) ──< (N) certificate_lines
certificates (1) ──< (N) certificate_validations
certificates (1) ──< (N) commentaires
abonnements (1) ──< (1) certificates (certificate_id - certificat principal)
```

## Données de référence

Le script insère automatiquement quelques types de certificats de référence :
- **EUR1** : Certificat EUR.1
- **FORM_A** : Formule A
- **CO** : Certificat d'Origine

## Notes importantes

- Le script vérifie l'existence de la base de données et des tables avant de les créer
- Il est donc possible de l'exécuter plusieurs fois sans erreur
- Les contraintes de clés étrangères sont créées avec `ON DELETE SET NULL` ou `ON DELETE CASCADE` selon les relations
- La contrainte CHECK sur le statut des certificats utilise les valeurs avec accents : 'Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé'

## Vérification

Après l'exécution du script, vous pouvez vérifier que tout est correct avec :

```sql
USE COService;
GO

-- Lister toutes les tables
SELECT name FROM sys.tables ORDER BY name;

-- Vérifier les contraintes
SELECT 
    OBJECT_NAME(parent_object_id) AS TableName,
    name AS ConstraintName,
    type_desc AS ConstraintType
FROM sys.objects
WHERE type_desc LIKE '%CONSTRAINT%'
ORDER BY TableName, ConstraintName;
```

## Prochaines étapes

Après avoir créé la base de données avec ce script, vous pouvez :

1. Configurer la chaîne de connexion dans `appsettings.json`
2. Exécuter l'application COService
3. Utiliser les migrations EF Core pour les futures modifications (optionnel)

