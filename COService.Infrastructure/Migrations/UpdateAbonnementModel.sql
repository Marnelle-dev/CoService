-- Migration pour mettre à jour le modèle d'abonnement
-- Date: 2025-01-27
-- Description: 
--   - Supprime l'ancienne relation (abonnements -> certificates)
--   - Ajoute les colonnes exportateur, partenaire, type_co à abonnements
--   - Ajoute la colonne AbonnementId à certificates
--   - Crée la nouvelle relation (certificates -> abonnements)

BEGIN TRANSACTION;

-- 1. Supprimer l'ancienne clé étrangère
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_abonnements_certificates_certificate_id')
BEGIN
    ALTER TABLE [abonnements] DROP CONSTRAINT [FK_abonnements_certificates_certificate_id];
END

-- 2. Supprimer l'ancienne colonne certificate_id
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'certificate_id')
BEGIN
    ALTER TABLE [abonnements] DROP COLUMN [certificate_id];
END

-- 3. Ajouter les nouvelles colonnes à abonnements
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'exportateur')
BEGIN
    ALTER TABLE [abonnements] ADD [exportateur] nvarchar(255) NOT NULL DEFAULT '';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'partenaire')
BEGIN
    ALTER TABLE [abonnements] ADD [partenaire] nvarchar(255) NOT NULL DEFAULT '';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('abonnements') AND name = 'type_co')
BEGIN
    ALTER TABLE [abonnements] ADD [type_co] nvarchar(100) NULL;
END

-- 4. Ajouter la colonne AbonnementId à certificates
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('certificates') AND name = 'AbonnementId')
BEGIN
    ALTER TABLE [certificates] ADD [AbonnementId] uniqueidentifier NULL;
END

-- 5. Créer l'index pour la nouvelle clé étrangère
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_certificates_AbonnementId' AND object_id = OBJECT_ID('certificates'))
BEGIN
    CREATE INDEX [IX_certificates_AbonnementId] ON [certificates] ([AbonnementId]);
END

-- 6. Créer la nouvelle clé étrangère
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_certificates_abonnements_AbonnementId')
BEGIN
    ALTER TABLE [certificates] 
    ADD CONSTRAINT [FK_certificates_abonnements_AbonnementId] 
    FOREIGN KEY ([AbonnementId]) 
    REFERENCES [abonnements] ([id]) 
    ON DELETE SET NULL;
END

COMMIT TRANSACTION;

