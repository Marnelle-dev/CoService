# 🔄 WORKFLOWS COMPLETS PAR CHAMBRE DE COMMERCE
## GECO - Système de Gestion des Certificats d'Origine

---

## 📋 VUE D'ENSEMBLE

Le système GECO gère **deux chambres de commerce principales** avec des workflows et des types de certificats différents :

1. **Chambre de Commerce de Pointe-Noire** (partner_id = 2, type = 1)
2. **Chambre de Commerce d'Ouesso** (partner_id = 3, type = 3)

---

## 🏢 CHAMBRE DE COMMERCE DE POINTE-NOIRE

### Caractéristiques

- **ID Partenaire :** 2
- **Type :** 1 (Chambre de Commerce standard)
- **Code Département :** PNR (utilisé dans la génération des numéros de certificats)

### Types de Certificats Disponibles

1. **CO Simple** (formule = 'CO')
   - Certificat d'Origine standard
   - Format : `CO{Numéro}{Date}{PNR}`

2. **CO + Formule A Cargo Commun** (formule = 'B')
   - Certificat d'Origine avec Formule A intégrée
   - Pour les cargaisons communes

### Workflow Complet - Certificat d'Origine (CO)

#### Étape 1 : Création par l'Exportateur

**Acteur :** Exportateur (team_id = 84)  
**Action :** Création d'un nouveau certificat

**Données requises :**
- Sélection de la chambre : Pointe-Noire (partner_id = 2)
- Type de certificat : CO ou CO + Formule A cargo commun
- Exportateur
- Destinataire (ProductsRecipient)
- Pays de destination
- Zone de production
- Module de transport (Aérien, Maritime, Fluvial, Routier)
- Port/Aéroport/Fleuve/Corridor selon le module
- Port du Congo
- Informations de transport (navire, pavillon)
- Lignes de certificat (produits, quantités, valeurs)

**Statut initial :** 1 (Élaboré)

**Génération du numéro :**
- Format : `CO{Numéro}{Date}{PNR}`
- Exemple : `CO100000241031224PNR`
- Le numéro séquentiel est incrémenté par date et partenaire

---

#### Étape 2 : Soumission

**Acteur :** Exportateur  
**Action :** Soumission du certificat pour validation

**Conditions :**
- Tous les champs obligatoires remplis
- Documents joints (si requis)
- Lignes de certificat complètes

**Transition :** Statut 1 → Statut 2 (Soumis)

**Notifications :**
- Notification à la chambre de commerce
- Email de confirmation à l'exportateur

---

#### Étape 3 : Contrôle

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce de Pointe-Noire (team_id = 1, organisation_id = 2)

**Actions possibles :**
1. **Valider le contrôle**
   - Transition : Statut 2 → Statut 4 (Contrôlé)
   - Vérification du mot de passe requise
   - Enregistrement de la validation dans l'historique

2. **Rejeter**
   - Transition : Statut 2 → Statut 5 (Rejeté)
   - Commentaire obligatoire
   - Notification à l'exportateur avec motif du rejet

**Règles de validation :**
- Seuls les rôles 3 (Contrôleur) et 4 (Superviseur) peuvent effectuer cette action
- Vérification du mot de passe obligatoire
- L'utilisateur doit appartenir à la chambre de commerce (team_id = 1)

---

#### Étape 4 : Approbation

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce de Pointe-Noire

**Action :** Approbation du certificat contrôlé

**Transition :** Statut 4 → Statut 7 (Approuvé)

**Conditions :**
- Le certificat doit être au statut 4 (Contrôlé)
- Vérification du mot de passe requise
- Seuls les rôles 3 et 4 peuvent approuver

**Notifications :**
- Notification au Président pour validation finale
- Email de confirmation

---

#### Étape 5 : Validation Finale

**Acteur :** Président (rôle 6)  
**Organisation :** Chambre de Commerce de Pointe-Noire (même organisation que le certificat)

**Action :** Validation définitive du certificat

**Transition :** Statut 7 → Statut 8 (Validé)

**Conditions strictes :**
- Le certificat doit être au statut 7 (Approuvé)
- L'utilisateur doit avoir le rôle 6 (Président)
- L'utilisateur doit appartenir à la même organisation que le certificat (partner_id = 2)
- Vérification du mot de passe obligatoire

**Résultat :**
- ✅ Certificat validé définitivement
- ✅ PDF générable
- ✅ Signature numérique enregistrée
- ✅ QR Code généré

---

#### Étape 6 : Génération PDF

**Acteurs :** Exportateur ou Mandataire  
**Action :** Génération du PDF du certificat

**Routes disponibles :**
- `GET /certiprint/{id}` : Génération CO standard
- `GET /certigenerate/{id}` : Génération CO

**Contenu du PDF :**
- Informations complètes du certificat
- Lignes de produits détaillées
- QR Code pour vérification
- Signature numérique du Président
- Logo de la chambre de commerce

---

### Workflow - Rejet

**Statuts de rejet :** 5 (Rejeté)

**Qui peut rejeter :**
- Contrôleur/Superviseur (rôles 3 ou 4) depuis statut 2, 4 ou 7
- Président (rôle 6) depuis statut 7

**Conditions :**
- Commentaire obligatoire expliquant le motif du rejet
- Vérification du mot de passe

**Conséquences :**
- Notification à l'exportateur
- Email avec motif du rejet
- Le certificat peut être modifié et resoumis

---

### Workflow - Modification

**Statut :** 10 (Modification)

**Processus :**
1. Exportateur demande une modification sur un certificat validé
2. Statut passe à 10 (Modification)
3. Contrôleur/Superviseur examine les modifications
4. Transition possible : 10 → 7 (Approuvé) ou 10 → 5 (Rejeté)
5. Si approuvé, retour au workflow normal (7 → 8)

---

## 🌳 CHAMBRE DE COMMERCE D'OUESSO

### Caractéristiques

- **ID Partenaire :** 3
- **Type :** 3 (Chambre de Commerce Ouesso)
- **Code Département :** OUE (utilisé dans la génération des numéros de certificats)
- **Spécificité :** Seule chambre autorisée à délivrer des Formules A

### Types de Certificats Disponibles

1. **Certificat d'Origine** (formule = 'CO')
   - Format : `CO{Numéro}{Date}{OUE}`

2. **Certificat d'Origine + EUR-1** (formule = 'EUR-1')
   - Certificat d'Origine avec certificat EUR.1 intégré
   - Pour les échanges avec l'Union Européenne

3. **CO + Attestation de Libre Commercialisation** (formule = 'CO+ALC')
   - Certificat d'Origine avec ALC
   - Pour les pays du Maghreb

4. **Formule A** (is_formule_a = true)
   - Créée à partir d'un CO validé
   - Workflow spécifique (statuts 12 à 15)

---

### Workflow Complet - Certificat d'Origine (CO) - Ouesso

#### Étape 1 : Création par l'Exportateur

**Acteur :** Exportateur  
**Action :** Création d'un nouveau certificat

**Données requises :**
- Sélection de la chambre : Ouesso (partner_id = 3)
- Type de certificat : CO, EUR-1, ou CO+ALC
- Exportateur
- Destinataire
- Pays de destination
- Zone de production
- Module de transport
- Informations de transport
- Lignes de certificat

**Statut initial :** 1 (Élaboré)

**Génération du numéro :**
- Format : `CO{Numéro}{Date}{OUE}`
- Exemple : `CO100000241031224OUE`

---

#### Étape 2 : Soumission

**Transition :** Statut 1 → Statut 2 (Soumis)

**Identique au workflow Pointe-Noire**

---

#### Étape 3 : Contrôle

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce d'Ouesso (team_id = 1, organisation_id = 3)

**Transition :** Statut 2 → Statut 4 (Contrôlé) ou Statut 2 → Statut 5 (Rejeté)

**Identique au workflow Pointe-Noire**

---

#### Étape 4 : Approbation

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce d'Ouesso

**Transition :** Statut 4 → Statut 7 (Approuvé)

**Identique au workflow Pointe-Noire**

---

#### Étape 5 : Validation Finale

**Acteur :** Président (rôle 6)  
**Organisation :** Chambre de Commerce d'Ouesso (même organisation que le certificat)

**Transition :** Statut 7 → Statut 8 (Validé)

**Identique au workflow Pointe-Noire**

---

#### Étape 6 : Génération PDF

**Routes spécifiques Ouesso :**
- `GET /certigenerate-ouesso/{id}` : Génération CO Ouesso
- `GET /eur1generate/{id}` : Génération EUR.1
- `GET /alcgenerate/{id}` : Génération ALC

**Templates PDF spécifiques :**
- `ouesso_print.blade.php` : Template CO Ouesso
- `eur1_print.blade.php` : Template EUR.1
- Template ALC spécifique

---

### Workflow Spécifique - Formule A (Ouesso uniquement)

**⚠️ IMPORTANT :** Seule la Chambre de Commerce d'Ouesso peut délivrer des Formules A.

#### Prérequis pour créer une Formule A

1. **CO validé requis :**
   - Le certificat d'origine doit être au statut 8 (Validé)
   - Le CO doit appartenir à Ouesso (partner_id = 3)

2. **Autorisation :**
   - Exportateur de type 3 (exportertype = 3), OU
   - Propriétaire du CO (exporter_id = user.organisation_id)

3. **Vérification :**
   - Vérification du mot de passe de l'exportateur
   - Validation que le CO est bien validé

---

#### Étape 1 : Création de la Formule A

**Acteur :** Exportateur  
**Action :** Création d'une Formule A à partir d'un CO validé

**Processus :**
1. Exportateur sélectionne un CO validé (statut 8)
2. Vérification que le CO appartient à Ouesso
3. Vérification des autorisations
4. Vérification du mot de passe
5. Création de la Formule A

**Modifications sur le certificat :**
- `is_formule_a` = true
- `statut` = 12 (Formule A soumise)
- Le `certificate_status_id` reste à 8 (pour référence au CO)

**Transition :** CO validé → Formule A soumise (statut 12)

**Notifications :**
- Notification à la chambre de commerce
- Email de confirmation

---

#### Étape 2 : Contrôle Formule A

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce d'Ouesso

**Actions possibles :**
1. **Contrôler la Formule A**
   - Transition : Statut 12 → Statut 13 (Formule A contrôlée)
   - Vérification du mot de passe requise

2. **Rejeter**
   - Transition : Statut 12 → Statut 5 (Rejetée)
   - Commentaire obligatoire

**Règles :**
- Seuls les rôles 3 et 4 peuvent contrôler
- Vérification du mot de passe obligatoire
- Enregistrement dans CertificateValidation

---

#### Étape 3 : Approbation Formule A

**Acteur :** Contrôleur ou Superviseur (rôles 3 ou 4)  
**Organisation :** Chambre de Commerce d'Ouesso

**Action :** Approbation de la Formule A contrôlée

**Transition :** Statut 13 → Statut 14 (Formule A approuvée)

**Conditions :**
- Le certificat doit être au statut 13
- Vérification du mot de passe requise
- Seuls les rôles 3 et 4 peuvent approuver

**Notifications :**
- Notification au Président pour validation finale

---

#### Étape 4 : Validation Finale Formule A

**Acteur :** Président (rôle 6)  
**Organisation :** Chambre de Commerce d'Ouesso (même organisation)

**Action :** Validation définitive de la Formule A

**Transition :** Statut 14 → Statut 15 (Formule A validée)

**Conditions strictes :**
- Le certificat doit être au statut 14
- L'utilisateur doit avoir le rôle 6 (Président)
- L'utilisateur doit appartenir à Ouesso (organisation_id = 3)
- Vérification du mot de passe obligatoire

**Résultat :**
- ✅ Formule A validée définitivement
- ✅ PDF générable
- ✅ Signature numérique enregistrée
- ✅ QR Code généré

---

#### Étape 5 : Génération PDF Formule A

**Acteur :** Exportateur ou Mandataire  
**Action :** Génération du PDF de la Formule A

**Route :** `GET /formule-a/{id}/generate`

**Conditions :**
- Le certificat doit être au statut 15 (Formule A validée)
- `is_formule_a` = true

**Contenu du PDF :**
- Informations de la Formule A
- Référence au CO original
- Lignes de produits
- QR Code
- Signature numérique du Président
- Logo de la chambre de commerce d'Ouesso

**Template :** `formule_a_print.blade.php`

---

### Workflow - Rejet Formule A

**Statuts de rejet :** 5 (Rejetée)

**Qui peut rejeter :**
- Contrôleur/Superviseur (rôles 3 ou 4) depuis statut 12, 13 ou 14
- Président (rôle 6) depuis statut 14

**Conditions :**
- Commentaire obligatoire
- Vérification du mot de passe

**Conséquences :**
- Notification à l'exportateur
- Email avec motif du rejet
- La Formule A peut être modifiée et resoumie

---

## 📊 COMPARAISON DES WORKFLOWS

### Tableau Comparatif

| Étape | Pointe-Noire | Ouesso |
|-------|--------------|--------|
| **Types de certificats** | CO, CO+Formule A cargo | CO, EUR-1, CO+ALC, Formule A |
| **Création CO** | ✅ | ✅ |
| **Workflow CO** | 1→2→4→7→8 | 1→2→4→7→8 |
| **Formule A** | ❌ Non disponible | ✅ Disponible (12→13→14→15) |
| **EUR-1** | ❌ | ✅ |
| **ALC** | ❌ | ✅ |
| **Code département** | PNR | OUE |

---

## 🔐 RÈGLES DE VALIDATION COMMUNES

### Rôles et Permissions

**Rôle 3 (Contrôleur) :**
- ✅ Peut contrôler (2→4, 12→13)
- ✅ Peut approuver (4→7, 13→14)
- ❌ Ne peut pas valider définitivement (7→8, 14→15)

**Rôle 4 (Superviseur) :**
- ✅ Peut contrôler (2→4, 12→13)
- ✅ Peut approuver (4→7, 13→14)
- ❌ Ne peut pas valider définitivement (7→8, 14→15)

**Rôle 6 (Président) :**
- ✅ Peut valider définitivement CO (7→8)
- ✅ Peut valider définitivement Formule A (14→15)
- ✅ Peut rejeter à tout moment
- ⚠️ Doit appartenir à la même organisation que le certificat

### Validations Obligatoires

1. **Vérification du mot de passe :**
   - Toutes les transitions de statut nécessitent la vérification du mot de passe
   - Protection contre les actions non autorisées

2. **Vérification du rôle :**
   - Chaque transition vérifie que l'utilisateur a le rôle approprié
   - Validation côté serveur stricte

3. **Vérification de l'organisation :**
   - Pour la validation finale (statut 7→8, 14→15)
   - Le Président doit appartenir à la même organisation que le certificat

4. **Commentaire obligatoire pour rejet :**
   - Tout rejet (→ statut 5) nécessite un commentaire explicatif
   - Enregistré dans la table `commentaires`

---

## 📝 GÉNÉRATION DE NUMÉROS

### Format Commun

**Structure :** `CO{Numéro}{Date}{CodeDépartement}`

**Exemple Pointe-Noire :** `CO100000241031224PNR`
- CO : Préfixe
- 100000 : Numéro séquentiel
- 241031 : Date (24/10/31 = 24 octobre 2031)
- PNR : Code département Pointe-Noire

**Exemple Ouesso :** `CO100000241031224OUE`
- CO : Préfixe
- 100000 : Numéro séquentiel
- 241031 : Date
- OUE : Code département Ouesso

### Algorithme de Génération

1. Récupération du partenaire (partner_id)
2. Récupération du département du partenaire
3. Récupération du code département
4. Formatage de la date (ddmmyy)
5. Recherche du dernier numéro pour ce partenaire et cette date
6. Extraction du numéro séquentiel
7. Incrémentation
8. Construction du numéro final

---

## 🔄 TRANSITIONS DE STATUT DÉTAILLÉES

### Certificat d'Origine (CO)

```
Statut 1 : Élaboré
    ↓ Exportateur soumet
Statut 2 : Soumis
    ↓ Contrôleur/Superviseur (rôles 3 ou 4)
Statut 4 : Contrôlé
    ↓ Contrôleur/Superviseur (rôles 3 ou 4)
Statut 7 : Approuvé
    ↓ Président SEULEMENT (rôle 6, même organisation)
Statut 8 : Validé ✅ → PDF générable

Rejets possibles :
- 2 → 5 (Rejeté)
- 4 → 5 (Rejeté)
- 7 → 5 (Rejeté)

Modification :
- 8 → 10 (Modification) → 7 (Approuvé) → 8 (Validé)
```

### Formule A (Ouesso uniquement)

```
Statut 12 : Formule A soumise
    ↓ Contrôleur/Superviseur (rôles 3 ou 4)
Statut 13 : Formule A contrôlée
    ↓ Contrôleur/Superviseur (rôles 3 ou 4)
Statut 14 : Formule A approuvée
    ↓ Président SEULEMENT (rôle 6, même organisation)
Statut 15 : Formule A validée ✅ → PDF générable

Rejets possibles :
- 12 → 5 (Rejetée)
- 13 → 5 (Rejetée)
- 14 → 5 (Rejetée)
```

---

## 📧 NOTIFICATIONS ET EMAILS

### Types de Notifications

1. **Soumission :**
   - Notification à la chambre de commerce
   - Email de confirmation à l'exportateur

2. **Contrôle :**
   - Notification à l'exportateur (si approuvé)
   - Email de rejet avec commentaire (si rejeté)

3. **Approbation :**
   - Notification au Président
   - Email de confirmation à l'exportateur

4. **Validation finale :**
   - Notification à l'exportateur
   - Email de validation avec lien de génération PDF

5. **Formule A :**
   - Notifications similaires à chaque étape
   - Emails spécifiques pour les Formules A

---

## 🎯 POINTS CLÉS POUR LA MIGRATION .NET CORE

### Services à Implémenter

1. **CertificateWorkflowService :**
   - Gestion des transitions de statut
   - Validation des règles métier
   - Vérification des rôles et permissions

2. **FormuleAService :**
   - Création de Formule A depuis CO validé
   - Validation des prérequis (Ouesso uniquement)
   - Workflow spécifique Formule A

3. **PDFGenerationService :**
   - Génération PDF CO standard
   - Génération PDF Ouesso
   - Génération PDF EUR.1
   - Génération PDF ALC
   - Génération PDF Formule A

4. **NotificationService :**
   - Envoi d'emails selon les transitions
   - Notifications en temps réel

### Validations Métier à Implémenter

1. **Validation des transitions :**
   - Vérification de la validité de la transition
   - Vérification du rôle utilisateur
   - Vérification de l'organisation
   - Vérification du mot de passe

2. **Validation Formule A :**
   - Vérification que le CO est validé
   - Vérification que le CO appartient à Ouesso
   - Vérification des autorisations exportateur

3. **Validation des commentaires :**
   - Commentaire obligatoire pour rejet
   - Validation de la longueur et du contenu

### DTOs et Modèles

- **CertificateWorkflowDTO** : Transfert des données de workflow
- **FormuleACreationDTO** : Création de Formule A
- **ValidationDTO** : Validation avec commentaire
- **StatusTransitionDTO** : Transition de statut

---

## 📝 CONCLUSION

Ce document détaille les workflows complets pour les deux chambres de commerce du système GECO :

1. **Pointe-Noire** : Workflow standard CO avec possibilité de Formule A cargo commun
2. **Ouesso** : Workflow CO standard + types spéciaux (EUR-1, ALC) + Formule A complète

Les principales différences :
- **Types de certificats** : Plus de variété pour Ouesso
- **Formule A** : Uniquement disponible pour Ouesso
- **Templates PDF** : Spécifiques à chaque chambre
- **Codes département** : PNR vs OUE

Pour la migration .NET Core, il sera essentiel de :
- Implémenter les workflows avec les mêmes règles métier
- Respecter les restrictions par chambre de commerce
- Maintenir la compatibilité des formats PDF
- Assurer la sécurité des validations

---

**Document généré le :** 2025-01-XX  
**Version :** 1.0  
**Projet :** GECO - Workflows Chambres de Commerce
