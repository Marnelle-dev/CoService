-- Script pour vérifier l'état de la base de données
-- Vérifie si les modifications du modèle d'abonnement ont été appliquées

-- 1. Vérifier les colonnes de la table abonnements
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'abonnements'
ORDER BY ORDINAL_POSITION;

-- 2. Vérifier les colonnes de la table certificates (notamment AbonnementId)
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'certificates' AND COLUMN_NAME = 'AbonnementId';

-- 3. Vérifier les clés étrangères
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName,
    COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferencedColumnName
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fc ON fk.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) IN ('abonnements', 'certificates')
   OR OBJECT_NAME(fk.referenced_object_id) IN ('abonnements', 'certificates')
ORDER BY TableName, ForeignKeyName;

-- 4. Vérifier si l'ancienne colonne certificate_id existe encore
SELECT 
    COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'abonnements' AND COLUMN_NAME = 'certificate_id';

