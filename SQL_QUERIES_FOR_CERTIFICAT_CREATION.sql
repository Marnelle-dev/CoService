-- =====================================================
-- REQUÊTES SQL POUR RÉCUPÉRER LES IDs NÉCESSAIRES
-- À LA CRÉATION D'UN CERTIFICAT D'ORIGINE
-- =====================================================

-- 1. EXPORTATEURS (ExportateurId)
-- =====================================================
SELECT 
    Id,
    CodeExportateur,
    Nom,
    RaisonSociale,
    NIU,
    Actif
FROM Exportateurs
WHERE Actif = 1
ORDER BY Nom;

-- 2. PARTENAIRES / CHAMBRES DE COMMERCE (PartenaireId)
-- =====================================================
SELECT 
    Id,
    CodePartenaire,
    Nom,
    Actif
FROM Partenaires
WHERE Actif = 1
ORDER BY Nom;

-- Exemple pour Pointe-Noire et Ouesso :
-- SELECT Id, CodePartenaire, Nom FROM Partenaires WHERE CodePartenaire IN ('CCIAM-PNR', 'CCIAM-OUE');

-- 3. PAYS (PaysDestinationId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom,
    Actif
FROM Pays
WHERE Actif = 1
ORDER BY Nom;

-- 4. PORTS (PortSortieId, PortCongoId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom,
    Type, -- 'MA' pour maritime, 'FL' pour fluvial
    Actif
FROM Ports
WHERE Actif = 1
ORDER BY Nom;

-- 5. TYPES DE CERTIFICATS (TypeId)
-- =====================================================
SELECT 
    Id,
    Code,
    Designation
FROM TypesCertificats
ORDER BY Code;

-- 6. CARNET D'ADRESSES (CarnetAdresseId)
-- =====================================================
SELECT 
    Id,
    Nom,
    RaisonSociale,
    Coordonnees,
    Adresse
FROM CarnetsAdresses
ORDER BY Nom;

-- 7. ZONES DE PRODUCTION (ZoneProductionId)
-- =====================================================
SELECT 
    zp.Id,
    zp.Nom,
    zp.Description,
    zp.PartenaireId,
    p.Nom AS PartenaireNom,
    p.CodePartenaire
FROM ZonesProductions zp
INNER JOIN Partenaires p ON zp.PartenaireId = p.Id
ORDER BY zp.Nom;

-- 8. BUREAUX DE DOUANE (BureauDedouanementId)
-- =====================================================
SELECT 
    Id,
    Code,
    Description,
    Actif
FROM BureauxDedouanements
WHERE Actif = 1
ORDER BY Description;

-- 9. MODULES DE TRANSPORT (ModuleId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom,
    Actif
FROM Modules
WHERE Actif = 1
ORDER BY Nom;

-- 10. DEVISES (DeviseId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom,
    Actif
FROM Devises
WHERE Actif = 1
ORDER BY Code;

-- 11. STATUTS DE CERTIFICAT (StatutCertificatId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom
FROM StatutsCertificats
ORDER BY Code;

-- Exemple pour le statut initial "ELABORE" :
-- SELECT Id, Code, Nom FROM StatutsCertificats WHERE Code = 'ELABORE';

-- =====================================================
-- REQUÊTES POUR LES LIGNES DE CERTIFICAT
-- =====================================================

-- 12. POSITIONS TARIFAIRES / CODES HS (PositionTarifaireId)
-- =====================================================
SELECT 
    pt.Id,
    pt.Code,
    pt.Description,
    pt.Actif,
    ct.Description AS CategorieDescription
FROM PositionsTariffaires pt
LEFT JOIN CategoriesTariffaires ct ON pt.CategorieCodeId = ct.Id
WHERE pt.Actif = 1
ORDER BY pt.Code;

-- 13. UNITÉS STATISTIQUES (UniteStatistiqueId)
-- =====================================================
SELECT 
    Id,
    Code,
    Nom,
    Actif
FROM UniteStatistiques
WHERE Actif = 1
ORDER BY Nom;

-- 14. INCOTERMS (IncotermId)
-- =====================================================
SELECT 
    i.Id,
    i.Code,
    i.Description,
    i.Actif,
    m.Nom AS ModuleNom
FROM Incoterms i
LEFT JOIN Modules m ON i.ModuleId = m.Id
WHERE i.Actif = 1
ORDER BY i.Code;

-- 15. DEVISES (pour les lignes - DeviseId)
-- (Déjà listé ci-dessus dans la section 10)

-- =====================================================
-- REQUÊTES COMBINÉES POUR UN APERÇU COMPLET
-- =====================================================

-- Vue d'ensemble des données nécessaires pour créer un certificat
SELECT 
    'Exportateurs' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Exportateurs
WHERE Actif = 1

UNION ALL

SELECT 
    'Partenaires' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Partenaires
WHERE Actif = 1

UNION ALL

SELECT 
    'Pays' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Pays
WHERE Actif = 1

UNION ALL

SELECT 
    'Ports' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Ports
WHERE Actif = 1

UNION ALL

SELECT 
    'TypesCertificats' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM TypesCertificats

UNION ALL

SELECT 
    'CarnetsAdresses' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM CarnetsAdresses

UNION ALL

SELECT 
    'ZonesProductions' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM ZonesProductions

UNION ALL

SELECT 
    'BureauxDedouanements' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM BureauxDedouanements
WHERE Actif = 1

UNION ALL

SELECT 
    'Modules' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Modules
WHERE Actif = 1

UNION ALL

SELECT 
    'Devises' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM Devises
WHERE Actif = 1

UNION ALL

SELECT 
    'StatutsCertificats' AS TableName,
    COUNT(*) AS NombreEnregistrements
FROM StatutsCertificats;
