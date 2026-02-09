-- =====================================================
-- REQUÊTES INSERT POUR LES TABLES RÉFÉRENTIELLES
-- Types de Certificats, Carnet d'Adresses, Zones de Production,
-- Bureaux de Douane, Modules, Positions Tarifaires,
-- Unités Statistiques, Incoterms
-- =====================================================

-- =====================================================
-- 1. TYPES DE CERTIFICATS (TypesCertificats)
-- Note: Les colonnes "code" et "designation" sont en minuscules
-- =====================================================
INSERT INTO TypesCertificats (id, code, designation, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), 'CO', 'Certificat d''Origine', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'FA', 'Formule A', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'EUR1', 'EUR.1', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'ALC', 'ALC (Accord de Libre-Échange)', GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 2. CARNET D'ADRESSES (CarnetsAdresses)
-- =====================================================
INSERT INTO CarnetsAdresses (id, Nom, RaisonSociale, Coordonnees, Adresse, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), 'Importateur France 1', 'SARL Import France', 'Tel: +33 1 23 45 67 89, Email: contact@importfrance.fr', '123 Rue de la République, 75001 Paris, France', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'Importateur Belgique 1', 'NV Import Belgique', 'Tel: +32 2 123 45 67, Email: info@importbelgique.be', '456 Avenue de la Liberté, 1000 Bruxelles, Belgique', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'Importateur Allemagne 1', 'GmbH Import Deutschland', 'Tel: +49 30 12345678, Email: kontakt@importdeutschland.de', '789 Hauptstraße, 10115 Berlin, Allemagne', GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'Importateur Cameroun 1', 'SARL Import Cameroun', 'Tel: +237 2 12 34 56, Email: contact@importcameroun.cm', '12 Boulevard de l''Indépendance, Douala, Cameroun', GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 3. ZONES DE PRODUCTION (ZonesProductions)
-- Note: Nécessite un PartenaireId existant
-- Remplacez les PartenaireId par les IDs réels de vos partenaires
-- =====================================================
-- D'abord, récupérez les IDs des partenaires :
-- SELECT Id, CodePartenaire, Nom FROM Partenaires WHERE CodePartenaire IN ('CCIAM-PNR', 'CCIAM-OUE');

-- Ensuite, insérez les zones de production :
-- Remplacez @PartenaireId_PNR et @PartenaireId_OUE par les IDs réels
INSERT INTO ZonesProductions (id, PartenaireId, Nom, Description, CreeLe, CreePar, ModifierLe, ModifiePar)
SELECT 
    NEWID(),
    p.id,
    'Zone Production Pointe-Noire',
    'Zone de production principale de Pointe-Noire',
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Partenaires p
WHERE p.CodePartenaire = 'CCIAM-PNR'

UNION ALL

SELECT 
    NEWID(),
    p.Id,
    'Zone Production Ouesso',
    'Zone de production principale d''Ouesso',
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Partenaires p
WHERE p.CodePartenaire = 'CCIAM-OUE'

UNION ALL

SELECT 
    NEWID(),
    p.Id,
    'Zone Production Brazzaville',
    'Zone de production de Brazzaville',
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Partenaires p
WHERE p.CodePartenaire = 'CCIAM-PNR';

-- =====================================================
-- 4. BUREAUX DE DOUANE (BureauxDedouanements)
-- =====================================================
INSERT INTO BureauxDedouanements (id, Code, Description, Actif, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), 'BD-PNR-001', 'Bureau de Douane de Pointe-Noire - Port Autonome', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'BD-PNR-002', 'Bureau de Douane de Pointe-Noire - Aéroport', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'BD-BZV-001', 'Bureau de Douane de Brazzaville - Port', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'BD-BZV-002', 'Bureau de Douane de Brazzaville - Aéroport', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'BD-OUE-001', 'Bureau de Douane d''Ouesso', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'BD-DOL-001', 'Bureau de Douane de Dolisie', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 5. MODULES DE TRANSPORT (Modules)
-- =====================================================
INSERT INTO Modules (id, Code, Nom, Description, Actif, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), 'MAR', 'Maritime', 'Transport maritime', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'AER', 'Aérien', 'Transport aérien', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'ROU', 'Routier', 'Transport routier', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'FLU', 'Fluvial', 'Transport fluvial', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'FER', 'Ferroviaire', 'Transport ferroviaire', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 6. POSITIONS TARIFAIRES / CODES HS (PositionsTariffaires)
-- Note: Nécessite un CategorieCodeId existant (peut être NULL)
-- =====================================================
-- Exemples de codes HS courants
INSERT INTO PositionsTariffaires (id, Code, Description, CategorieCodeId, Actif, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), '0901', 'Café, même torréfié ou décaféiné; coques et balles de café; succédanés du café contenant du café', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '0902', 'Thé, même parfumé, et succédanés du thé', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '0801', 'Noix de coco, noix du Brésil et noix de cajou, fraîches ou séchées, même sans coque ou décortiquées', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '0802', 'Autres fruits à coque, frais ou séchés, même sans coque ou décortiqués', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '4403', 'Bois en grumes, simplement équarri', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '4407', 'Bois de sciage ou de débitage, scié longitudinalement, tranché ou déroulé, d''une épaisseur supérieure à 6 mm', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '2601', 'Minerais de fer et leurs concentrés, y compris les pyrites de fer grillées', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '2603', 'Minerais de cuivre et leurs concentrés', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '2701', 'Charbon; briquettes, boulets et combustibles solides similaires obtenus à partir du charbon', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), '2709', 'Pétrole brut et huiles brutes provenant de schistes bitumineux ou d''argiles bitumineuses', NULL, 1, GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 7. UNITÉS STATISTIQUES (UniteStatistiques)
-- =====================================================
INSERT INTO UniteStatistiques (id, Code, Nom, Actif, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES
    (NEWID(), 'KG', 'Kilogramme', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'T', 'Tonne', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'G', 'Gramme', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'M3', 'Mètre cube', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'L', 'Litre', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'M2', 'Mètre carré', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'M', 'Mètre', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'PCE', 'Pièce', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'PAQ', 'Paquet', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'CART', 'Carton', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'PAL', 'Palette', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL),
    (NEWID(), 'CTN', 'Conteneur', 1, GETUTCDATE(), 'SYSTEM', NULL, NULL);

-- =====================================================
-- 8. INCOTERMS (Incoterms)
-- Note: Nécessite un ModuleId existant
-- =====================================================
-- D'abord, récupérez les IDs des modules :
-- SELECT Id, Code, Nom FROM Modules;

-- Ensuite, insérez les incoterms :
-- Remplacez les ModuleId par les IDs réels des modules
INSERT INTO Incoterms (id, Code, Description, ModuleId, Actif, CreeLe, CreePar, ModifierLe, ModifiePar)
SELECT 
    NEWID(),
    'EXW',
    'Ex Works (À l''usine)',
    m.id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'FCA',
    'Free Carrier (Franco transporteur)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'FOB',
    'Free On Board (Franco à bord)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'MAR'

UNION ALL

SELECT 
    NEWID(),
    'CFR',
    'Cost and Freight (Coût et fret)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'MAR'

UNION ALL

SELECT 
    NEWID(),
    'CIF',
    'Cost, Insurance and Freight (Coût, assurance et fret)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'MAR'

UNION ALL

SELECT 
    NEWID(),
    'CPT',
    'Carriage Paid To (Port payé jusqu''à)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'CIP',
    'Carriage and Insurance Paid To (Port payé, assurance comprise, jusqu''à)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'DAP',
    'Delivered At Place (Rendu au lieu de destination)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'DPU',
    'Delivered at Place Unloaded (Rendu au lieu de destination déchargé)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU'

UNION ALL

SELECT 
    NEWID(),
    'DDP',
    'Delivered Duty Paid (Rendu droits acquittés)',
    m.Id,
    1,
    GETUTCDATE(),
    'SYSTEM',
    NULL,
    NULL
FROM Modules m
WHERE m.Code = 'ROU';

-- =====================================================
-- 9. DOCUMENTS (DocumentsId)
-- Note: DocumentsId dans CertificatOrigine est une référence
-- au microservice Documents externe. Ces GUIDs sont des exemples
-- pour les tests. En production, ces IDs viendront du microservice Documents.
-- =====================================================
-- Exemples de GUIDs pour DocumentsId (à utiliser dans les tests)
-- Ces GUIDs représentent des documents fictifs dans le microservice Documents

-- Pour utiliser ces GUIDs dans vos tests de création de certificat :
-- DocumentsId = '11111111-1111-1111-1111-111111111111' (Document 1)
-- DocumentsId = '22222222-2222-2222-2222-222222222222' (Document 2)
-- etc.

-- Exemples de GUIDs pour DocumentsId :
DECLARE @DocumentId1 UNIQUEIDENTIFIER = '11111111-1111-1111-1111-111111111111';
DECLARE @DocumentId2 UNIQUEIDENTIFIER = '22222222-2222-2222-2222-222222222222';
DECLARE @DocumentId3 UNIQUEIDENTIFIER = '33333333-3333-3333-3333-333333333333';
DECLARE @DocumentId4 UNIQUEIDENTIFIER = '44444444-4444-4444-4444-444444444444';
DECLARE @DocumentId5 UNIQUEIDENTIFIER = '55555555-5555-5555-5555-555555555555';

-- Affichage des GUIDs pour copier-coller dans vos tests :
SELECT 
    'Document 1' AS Description,
    @DocumentId1 AS DocumentsId
UNION ALL
SELECT 
    'Document 2',
    @DocumentId2
UNION ALL
SELECT 
    'Document 3',
    @DocumentId3
UNION ALL
SELECT 
    'Document 4',
    @DocumentId4
UNION ALL
SELECT 
    'Document 5',
    @DocumentId5;

-- =====================================================
-- VÉRIFICATION DES INSERTIONS
-- =====================================================
-- Vérifiez que les données ont été insérées correctement :

SELECT 'TypesCertificats' AS TableName, COUNT(*) AS Nombre FROM TypesCertificats
UNION ALL
SELECT 'CarnetsAdresses', COUNT(*) FROM CarnetsAdresses
UNION ALL
SELECT 'ZonesProductions', COUNT(*) FROM ZonesProductions
UNION ALL
SELECT 'BureauxDedouanements', COUNT(*) FROM BureauxDedouanements
UNION ALL
SELECT 'Modules', COUNT(*) FROM Modules
UNION ALL
SELECT 'PositionsTariffaires', COUNT(*) FROM PositionsTariffaires
UNION ALL
SELECT 'UniteStatistiques', COUNT(*) FROM UniteStatistiques
UNION ALL
SELECT 'Incoterms', COUNT(*) FROM Incoterms;
