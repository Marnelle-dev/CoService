# Instructions pour appliquer la migration de la table abonnements

## ⚠️ IMPORTANT : Arrêter l'application d'abord

Avant d'appliquer la migration, **vous devez arrêter l'application** qui est en cours d'exécution.

## Option 1 : Exécuter le script SQL directement (Recommandé)

1. Ouvrez **SQL Server Management Studio** ou **Azure Data Studio**
2. Connectez-vous à votre serveur SQL Server
3. Sélectionnez la base de données `COServiceDb`
4. Ouvrez le fichier `ApplyAbonnementModelUpdate.sql`
5. Exécutez le script (F5)

Le script est **idempotent** : il vérifie l'existence de chaque élément avant de le créer/supprimer, donc vous pouvez l'exécuter plusieurs fois sans problème.

## Option 2 : Via Entity Framework (si l'application est arrêtée)

```powershell
cd "G:\PROJET 2025\REFONTE SEG\CO"
dotnet ef database update --project COService.Infrastructure --startup-project COService.API
```

## Ce que fait la migration

### Table `abonnements`
- ✅ **Ajoute** : `exportateur` (nvarchar 255, NOT NULL)
- ✅ **Ajoute** : `partenaire` (nvarchar 255, NOT NULL)  
- ✅ **Ajoute** : `type_co` (nvarchar 100, NULL)
- ❌ **Supprime** : `certificate_id` (ancienne relation)

### Table `certificates`
- ✅ **Ajoute** : `AbonnementId` (uniqueidentifier, NULL)

### Relations
- ❌ **Supprime** : `FK_abonnements_certificates_certificate_id`
- ✅ **Crée** : `FK_certificates_abonnements_AbonnementId` (1-N : un abonnement → plusieurs certificats)

## ⚠️ Attention aux données existantes

Si vous avez des données existantes dans la table `abonnements`, vous devrez :
1. Mettre à jour les valeurs de `exportateur` et `partenaire` (actuellement définies à `''` par défaut)
2. Migrer les relations si nécessaire

## Vérification après migration

Exécutez le script `CheckDatabaseState.sql` pour vérifier que tout a été appliqué correctement.

