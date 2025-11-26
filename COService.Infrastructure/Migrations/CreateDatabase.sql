-- =============================================
-- Script de création de la base de données COService
-- SQL Server
-- =============================================

-- Créer la base de données si elle n'existe pas
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'COServiceDb')
BEGIN
    CREATE DATABASE COServiceDb;
END
GO

USE COServiceDb;
GO

-- =============================================
-- Table: certificate_types
-- Types de certificats d'origine (EUR.1, Formule A, etc.)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'certificate_types')
BEGIN
    CREATE TABLE certificate_types (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        designation nvarchar(255) NOT NULL,
        code nvarchar(50) NOT NULL,
        CreeLe datetime2(7) NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index unique sur le code
    CREATE UNIQUE INDEX IX_certificate_types_code 
        ON certificate_types(code);
END
GO

-- =============================================
-- Table: certificates
-- Certificats d'origine
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'certificates')
BEGIN
    CREATE TABLE certificates (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        CertificateNo nvarchar(255) NOT NULL,
        Exportateur nvarchar(200) NOT NULL,
        Partenaire nvarchar(200) NULL,
        PaysDestination nvarchar(200) NULL,
        PortSortie nvarchar(200) NULL,
        PortCongo nvarchar(200) NULL,
        TypeId uniqueidentifier NULL,
        Formule nvarchar(200) NULL,
        Mandataire nvarchar(200) NULL,
        Statut nvarchar(30) NOT NULL,
        Observation nvarchar(max) NULL,
        ProductsRecipientName nvarchar(255) NULL,
        ProductsRecipientAddress1 nvarchar(255) NULL,
        ProductsRecipientAddress2 nvarchar(255) NULL,
        navire nvarchar(255) NULL,
        documents_id uniqueidentifier NULL,
        abonnement_id uniqueidentifier NULL,
        CreeLe datetime2(7) NOT NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index unique sur CertificateNo
    CREATE UNIQUE INDEX IX_certificates_CertificateNo 
        ON certificates(CertificateNo);

    -- Index sur TypeId
    CREATE INDEX IX_certificates_TypeId 
        ON certificates(TypeId);

    -- Index sur abonnement_id
    CREATE INDEX IX_certificates_abonnement_id 
        ON certificates(abonnement_id);

    -- Contrainte CHECK sur Statut
    ALTER TABLE certificates
        ADD CONSTRAINT CK_certificates_Statut 
        CHECK (Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé'));

    -- Clé étrangère vers certificate_types
    ALTER TABLE certificates
        ADD CONSTRAINT FK_certificates_certificate_types_TypeId
        FOREIGN KEY (TypeId) REFERENCES certificate_types(id)
        ON DELETE SET NULL;
END
GO

-- =============================================
-- Table: abonnements
-- Abonnements créés par la chambre de commerce
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'abonnements')
BEGIN
    CREATE TABLE abonnements (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        exportateur nvarchar(255) NOT NULL,
        partenaire nvarchar(255) NOT NULL,
        type_co nvarchar(100) NULL,
        factureNo nvarchar(255) NULL,
        formule nvarchar(200) NULL,
        numero nvarchar(255) NULL,
        Statut nvarchar(200) NULL,
        certificate_id uniqueidentifier NULL,
        CreeLe datetime2(7) NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index unique sur certificate_id (quand non NULL)
    CREATE UNIQUE INDEX IX_abonnements_certificate_id 
        ON abonnements(certificate_id)
        WHERE certificate_id IS NOT NULL;

    -- Clé étrangère vers certificates (certificat principal)
    ALTER TABLE abonnements
        ADD CONSTRAINT FK_abonnements_certificates_certificate_id
        FOREIGN KEY (certificate_id) REFERENCES certificates(id)
        ON DELETE SET NULL;
END
GO

-- Ajouter la clé étrangère de certificates vers abonnements (si pas déjà créée)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_certificates_abonnements_abonnement_id')
BEGIN
    ALTER TABLE certificates
        ADD CONSTRAINT FK_certificates_abonnements_abonnement_id
        FOREIGN KEY (abonnement_id) REFERENCES abonnements(id)
        ON DELETE SET NULL;
END
GO

-- =============================================
-- Table: certificate_lines
-- Lignes de produits des certificats
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'certificate_lines')
BEGIN
    CREATE TABLE certificate_lines (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        certificate_id uniqueidentifier NOT NULL,
        HSCode nvarchar(50) NULL,
        LineNatureOfProduct nvarchar(255) NULL,
        LineQuantity nvarchar(50) NULL,
        LineUnits nvarchar(50) NULL,
        LineGrossWeight nvarchar(50) NULL,
        LineNetWeight nvarchar(50) NULL,
        LineFOBValue nvarchar(50) NULL,
        LineVolume nvarchar(50) NULL,
        CreeLe datetime2(7) NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index sur certificate_id
    CREATE INDEX IX_certificate_lines_certificate_id 
        ON certificate_lines(certificate_id);

    -- Clé étrangère vers certificates
    ALTER TABLE certificate_lines
        ADD CONSTRAINT FK_certificate_lines_certificates_certificate_id
        FOREIGN KEY (certificate_id) REFERENCES certificates(id)
        ON DELETE CASCADE;
END
GO

-- =============================================
-- Table: certificate_validations
-- Validations des certificats
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'certificate_validations')
BEGIN
    CREATE TABLE certificate_validations (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        certificate_id uniqueidentifier NOT NULL,
        Etape nvarchar(20) NOT NULL,
        RoleVisa nvarchar(20) NOT NULL,
        VisaPar nvarchar(200) NULL,
        Commentaire nvarchar(255) NULL,
        CreeLe datetime2(7) NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index sur certificate_id
    CREATE INDEX IX_certificate_validations_certificate_id 
        ON certificate_validations(certificate_id);

    -- Clé étrangère vers certificates
    ALTER TABLE certificate_validations
        ADD CONSTRAINT FK_certificate_validations_certificates_certificate_id
        FOREIGN KEY (certificate_id) REFERENCES certificates(id)
        ON DELETE CASCADE;
END
GO

-- =============================================
-- Table: commentaires
-- Commentaires sur les certificats
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'commentaires')
BEGIN
    CREATE TABLE commentaires (
        id uniqueidentifier NOT NULL PRIMARY KEY,
        certificate_id uniqueidentifier NOT NULL,
        Commentaire nvarchar(255) NULL,
        CreeLe datetime2(7) NULL,
        CreePar nvarchar(max) NULL,
        ModifierLe datetime2(7) NULL,
        ModifiePar nvarchar(max) NULL
    );

    -- Index sur certificate_id
    CREATE INDEX IX_commentaires_certificate_id 
        ON commentaires(certificate_id);

    -- Clé étrangère vers certificates
    ALTER TABLE commentaires
        ADD CONSTRAINT FK_commentaires_certificates_certificate_id
        FOREIGN KEY (certificate_id) REFERENCES certificates(id)
        ON DELETE CASCADE;
END
GO

-- =============================================
-- Données de référence (optionnel)
-- =============================================

-- Insérer quelques types de certificats de référence
IF NOT EXISTS (SELECT * FROM certificate_types WHERE code = 'EUR1')
BEGIN
    INSERT INTO certificate_types (id, designation, code, CreeLe, CreePar)
    VALUES (NEWID(), 'Certificat EUR.1', 'EUR1', GETUTCDATE(), 'SYSTEM');
END
GO

IF NOT EXISTS (SELECT * FROM certificate_types WHERE code = 'FORM_A')
BEGIN
    INSERT INTO certificate_types (id, designation, code, CreeLe, CreePar)
    VALUES (NEWID(), 'Formule A', 'FORM_A', GETUTCDATE(), 'SYSTEM');
END
GO

IF NOT EXISTS (SELECT * FROM certificate_types WHERE code = 'CO')
BEGIN
    INSERT INTO certificate_types (id, designation, code, CreeLe, CreePar)
    VALUES (NEWID(), 'Certificat d''Origine', 'CO', GETUTCDATE(), 'SYSTEM');
END
GO

-- =============================================
-- Fin du script
-- =============================================
PRINT 'Base de données COService créée avec succès!';
PRINT 'Tables créées:';
PRINT '  - certificate_types';
PRINT '  - certificates';
PRINT '  - abonnements';
PRINT '  - certificate_lines';
PRINT '  - certificate_validations';
PRINT '  - commentaires';
GO

