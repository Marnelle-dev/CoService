# Migration : UpdateAbonnementModel

## État actuel

**⚠️ La migration n'a PAS encore été appliquée à la base de données.**

La migration `20250127163119_UpdateAbonnementModel.cs` a été créée mais n'a pas pu être appliquée car l'application était en cours d'exécution.

## Modifications apportées au modèle

### Table `abonnements`
- ✅ **Ajout** : `exportateur` (nvarchar(255), NOT NULL) - Exportateur bénéficiaire
- ✅ **Ajout** : `partenaire` (nvarchar(255), NOT NULL) - Chambre de commerce qui crée l'abonnement
- ✅ **Ajout** : `type_co` (nvarchar(100), NULL) - Type de certificat (EUR.1, Formule A, etc.)
- ❌ **Suppression** : `certificate_id` (ancienne relation)

### Table `certificates`
- ✅ **Ajout** : `AbonnementId` (uniqueidentifier, NULL) - Référence vers l'abonnement

### Relations
- ❌ **Suppression** : `FK_abonnements_certificates_certificate_id` (ancienne relation 1-N)
- ✅ **Ajout** : `FK_certificates_abonnements_AbonnementId` (nouvelle relation 1-N : un abonnement peut avoir plusieurs certificats)

## Comment appliquer la migration

### Option 1 : Via Entity Framework (Recommandé)

1. **Arrêter l'application** (Visual Studio ou terminal)
2. Exécuter la commande :
```powershell
cd "G:\PROJET 2025\REFONTE SEG\CO"
dotnet ef database update --project COService.Infrastructure --startup-project COService.API
```

### Option 2 : Via script SQL direct

Exécuter le script `UpdateAbonnementModel.sql` directement dans SQL Server Management Studio ou Azure Data Studio.

## Vérification

Pour vérifier l'état de la base de données, exécutez le script `CheckDatabaseState.sql`.

## ⚠️ Important

Si vous avez des données existantes dans la table `abonnements`, vous devrez peut-être :
1. Sauvegarder les données existantes
2. Remplir les valeurs pour `exportateur` et `partenaire` avant d'appliquer la migration
3. Migrer les relations existantes (si `certificate_id` avait des valeurs)

