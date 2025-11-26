-- =============================================
-- Script de migration pour mettre à jour le modèle d'abonnement
-- Date: 2025-01-27
-- Description: 
--   - Ajoute les colonnes exportateur, partenaire, type_co à la table abonnements
--   - Supprime l'ancienne colonne certificate_id de abonnements
--   - Ajoute la colonne AbonnementId à la table certificates
--   - Met à jour les relations (1-N : un abonnement peut avoir plusieurs certificats)
-- =============================================

USE [COServiceDb];
GO

BEGIN TRANSACTION;

PRINT 'Début de la migration...';

-- =============================================
-- ÉTAPE 1: Vérifier et supprimer l'ancienne clé étrangère
-- =============================================
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_abonnements_certificates_certificate_id')
BEGIN
    PRINT 'Suppression de l''ancienne clé étrangère FK_abonnements_certificates_certificate_id...';
    ALTER TABLE [abonnements] DROP CONSTRAINT [FK_abonnements_certificates_certificate_id];
    PRINT 'Ancienne clé étrangère supprimée.';
END
ELSE
BEGIN
    PRINT 'L''ancienne clé étrangère n''existe pas (déjà supprimée ou n''a jamais existé).';
END

-- =============================================
-- ÉTAPE 2: Supprimer l'ancienne colonne certificate_id
-- =============================================
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'certificate_id')
BEGIN
    PRINT 'Suppression de l''ancienne colonne certificate_id...';
    ALTER TABLE [abonnements] DROP COLUMN [certificate_id];
    PRINT 'Colonne certificate_id supprimée.';
END
ELSE
BEGIN
    PRINT 'La colonne certificate_id n''existe pas (déjà supprimée ou n''a jamais existé).';
END

-- =============================================
-- ÉTAPE 3: Ajouter les nouvelles colonnes à abonnements
-- =============================================

-- Colonne exportateur
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'exportateur')
BEGIN
    PRINT 'Ajout de la colonne exportateur...';
    ALTER TABLE [abonnements] ADD [exportateur] nvarchar(255) NOT NULL DEFAULT '';
    PRINT 'Colonne exportateur ajoutée.';
END
ELSE
BEGIN
    PRINT 'La colonne exportateur existe déjà.';
END

-- Colonne partenaire
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'partenaire')
BEGIN
    PRINT 'Ajout de la colonne partenaire...';
    ALTER TABLE [abonnements] ADD [partenaire] nvarchar(255) NOT NULL DEFAULT '';
    PRINT 'Colonne partenaire ajoutée.';
END
ELSE
BEGIN
    PRINT 'La colonne partenaire existe déjà.';
END

-- Colonne type_co
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'type_co')
BEGIN
    PRINT 'Ajout de la colonne type_co...';
    ALTER TABLE [abonnements] ADD [type_co] nvarchar(100) NULL;
    PRINT 'Colonne type_co ajoutée.';
END
ELSE
BEGIN
    PRINT 'La colonne type_co existe déjà.';
END

-- =============================================
-- ÉTAPE 4: Ajouter la colonne AbonnementId à certificates
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('certificates') AND name = 'AbonnementId')
BEGIN
    PRINT 'Ajout de la colonne AbonnementId à certificates...';
    ALTER TABLE [certificates] ADD [AbonnementId] uniqueidentifier NULL;
    PRINT 'Colonne AbonnementId ajoutée.';
END
ELSE
BEGIN
    PRINT 'La colonne AbonnementId existe déjà.';
END

-- =============================================
-- ÉTAPE 5: Créer l'index pour la nouvelle clé étrangère
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_certificates_AbonnementId' AND object_id = OBJECT_ID('certificates'))
BEGIN
    PRINT 'Création de l''index IX_certificates_AbonnementId...';
    CREATE INDEX [IX_certificates_AbonnementId] ON [certificates] ([AbonnementId]);
    PRINT 'Index créé.';
END
ELSE
BEGIN
    PRINT 'L''index IX_certificates_AbonnementId existe déjà.';
END

-- =============================================
-- ÉTAPE 6: Créer la nouvelle clé étrangère
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_certificates_abonnements_AbonnementId')
BEGIN
    PRINT 'Création de la nouvelle clé étrangère FK_certificates_abonnements_AbonnementId...';
    ALTER TABLE [certificates] 
    ADD CONSTRAINT [FK_certificates_abonnements_AbonnementId] 
    FOREIGN KEY ([AbonnementId]) 
    REFERENCES [abonnements] ([id]) 
    ON DELETE SET NULL;
    PRINT 'Clé étrangère créée.';
END
ELSE
BEGIN
    PRINT 'La clé étrangère FK_certificates_abonnements_AbonnementId existe déjà.';
END

-- =============================================
-- Vérification finale
-- =============================================
PRINT '';
PRINT 'Vérification de la structure finale...';

-- Vérifier les colonnes de abonnements
PRINT 'Colonnes de la table abonnements:';
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'abonnements'
ORDER BY ORDINAL_POSITION;

-- Vérifier la colonne AbonnementId dans certificates
PRINT '';
PRINT 'Vérification de la colonne AbonnementId dans certificates:';
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('certificates') AND name = 'AbonnementId')
    PRINT '✓ La colonne AbonnementId existe dans certificates.';
ELSE
    PRINT '✗ La colonne AbonnementId n''existe pas dans certificates.';

-- Vérifier les clés étrangères
PRINT '';
PRINT 'Clés étrangères:';
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fc ON fk.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) IN ('abonnements', 'certificates')
   OR OBJECT_NAME(fk.referenced_object_id) IN ('abonnements', 'certificates')
ORDER BY TableName, ForeignKeyName;

COMMIT TRANSACTION;

PRINT '';
PRINT 'Migration terminée avec succès!';
PRINT 'Note: Si vous aviez des données existantes dans abonnements,';
PRINT 'vous devrez peut-être mettre à jour les valeurs de exportateur et partenaire.';

GO

