-- =====================================================
-- INSERTION DES STATUTS DE CERTIFICATS INITIAUX
-- =====================================================
-- Ce script insère tous les statuts nécessaires pour les workflows
-- des chambres de commerce de Pointe-Noire et Ouesso
-- =====================================================

USE COServiceDb;
GO

-- Statuts pour Certificat d'Origine (CO)
INSERT INTO StatutsCertificats (id, Code, Nom, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES 
    (NEWID(), N'ELABORE', N'Élaboré', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'SOUMIS', N'Soumis', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'CONTROLE', N'Contrôlé', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'REJETE', N'Rejeté', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'APPROUVE', N'Approuvé', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'VALIDE', N'Validé', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'MODIFICATION', N'Modification', GETUTCDATE(), N'SYSTEM', NULL, NULL);

-- Statuts pour Formule A (Ouesso uniquement)
INSERT INTO StatutsCertificats (id, Code, Nom, CreeLe, CreePar, ModifierLe, ModifiePar)
VALUES 
    (NEWID(), N'FORMULE_A_SOUMISE', N'Formule A soumise', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'FORMULE_A_CONTROLEE', N'Formule A contrôlée', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'FORMULE_A_APPROUVEE', N'Formule A approuvée', GETUTCDATE(), N'SYSTEM', NULL, NULL),
    (NEWID(), N'FORMULE_A_VALIDEE', N'Formule A validée', GETUTCDATE(), N'SYSTEM', NULL, NULL);

GO

-- Vérification
SELECT 
    Code,
    Nom,
    CreeLe,
    CreePar,
    ModifierLe,
    ModifiePar
FROM StatutsCertificats
ORDER BY Code;

GO
