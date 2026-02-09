# 📊 RÉFÉRENTIELS RÉELLEMENT UTILISÉS DANS LE PROCESSUS CO

## Analyse basée sur le document d'étude

D'après l'analyse du document `ETUDE_COMPLETE_FONCTIONNALITES_PROJET.md` et `WORKFLOWS_COMPLETS_CHAMBRES_COMMERCE.md`, voici les référentiels **réellement utilisés** dans le processus de création et gestion des certificats.

---

## ✅ RÉFÉRENTIELS UTILISÉS (À CRÉER)

### 1. Pays ✅
- **Utilisation** : `country_id` dans `certificates` (pays de destination)
- **Champ dans certificat** : `PaysDestination`
- **Priorité** : **HAUTE** - Utilisé directement

### 2. Ports ✅
- **Utilisation** : 
  - `port_id` dans `certificates` (port de destination)
  - `portcongo_id` dans `certificates` (port du Congo)
- **Champs dans certificat** : `PortSortie`, `PortCongo`
- **Priorité** : **HAUTE** - Utilisé directement

### 3. Aéroports ✅
- **Utilisation** : Sélection pour transport aérien
- **Selon le workflow** : "Port/Aéroport/Fleuve/Corridor selon le module"
- **Priorité** : **MOYENNE** - Utilisé si transport aérien

### 4. Fleuves ❌
- **Note** : Non nécessaire - Les ports fluviaux sont gérés via la table `Ports` avec le champ `Type` (Maritime, Fluvial)
- **Pas de table séparée** : La table `Fleuves` n'est pas nécessaire

### 5. Corridors ✅
- **Utilisation** : Sélection pour transport
- **Selon le workflow** : "Port/Aéroport/Fleuve/Corridor selon le module"
- **Priorité** : **MOYENNE** - Utilisé selon le mode de transport

### 6. Modules (Modes de transport) ✅
- **Utilisation** : "Module de transport (Aérien, Maritime, Fluvial, Routier)"
- **Priorité** : **HAUTE** - Utilisé directement pour déterminer quel référentiel utiliser

### 7. Devises ✅
- **Utilisation** : `Currency` dans `certificate_lines` (lignes de certificat)
- **Priorité** : **HAUTE** - Utilisé dans les lignes de certificat

### 8. BureauxDedouanements ✅
- **Utilisation** : `customs_office` dans `certificates` (bureau de douane)
- **Priorité** : **MOYENNE** - Utilisé dans les certificats

### 9. Incoterms ❓
- **Utilisation** : Non clairement mentionné dans le document
- **Priorité** : **BASSE** - À vérifier si utilisé

---

## ❓ RÉFÉRENTIELS À VÉRIFIER

### 10. RoutesNationales ❓
- **Utilisation** : Mentionné dans le document mais pas clairement utilisé
- **Relation** : Peut être utilisé via Tronçons
- **Priorité** : **BASSE** - À vérifier

### 11. Tronçons ❓
- **Utilisation** : Mentionné dans le document mais pas clairement utilisé
- **Relation** : Peut être utilisé pour transport routier
- **Priorité** : **BASSE** - À vérifier

### 12. Sections ❓
- **Utilisation** : Mentionné dans le document mais pas clairement utilisé
- **Priorité** : **BASSE** - À vérifier

### 13. TauxDeChanges ❓
- **Utilisation** : Peut être utilisé pour calculs financiers
- **Priorité** : **BASSE** - À vérifier si nécessaire pour conversions

### 14. Classification Tarifaire ❓
- **SectionsTariffaires, ChapitresTariffaires, DivisionsTariffaires, CategoriesTariffaires, PositionsTariffaires**
- **Utilisation** : Peut être utilisé dans les lignes de certificat pour classification des produits
- **Champ** : `HSCode` dans `certificate_lines` (peut être un code tarifaire)
- **Priorité** : **BASSE** - À vérifier si utilisé

### 15. TypeTransports ❓
- **Utilisation** : Non clairement mentionné
- **Priorité** : **BASSE** - À vérifier

### 16. UniteDeChargements ❓
- **Utilisation** : Peut être utilisé dans les lignes de certificat
- **Priorité** : **BASSE** - À vérifier

### 17. UniteStatistiques ❓
- **Utilisation** : `Unity` dans `certificate_lines` (unité)
- **Priorité** : **MOYENNE** - Probablement utilisé

---

## 📋 RÉSUMÉ PAR PRIORITÉ

### 🔴 PRIORITÉ HAUTE (À créer en premier)
1. ✅ **Pays** - Utilisé directement (`country_id`)
2. ✅ **Ports** - Utilisé directement (`port_id`, `portcongo_id`)
3. ✅ **Modules** - Utilisé directement (mode de transport)
4. ✅ **Devises** - Utilisé directement (`Currency` dans lignes)

### 🟡 PRIORITÉ MOYENNE (À créer ensuite)
5. ✅ **Aéroports** - Utilisé si transport aérien
6. ✅ **Corridors** - Utilisé selon mode de transport
8. ✅ **BureauxDedouanements** - Utilisé (`customs_office`)
9. ❓ **UniteStatistiques** - Probablement utilisé (`Unity`)

### 🟢 PRIORITÉ BASSE (À créer plus tard ou à vérifier)
10. ❓ **RoutesNationales** - À vérifier
11. ❓ **Tronçons** - À vérifier
12. ❓ **Sections** - À vérifier
13. ❓ **TauxDeChanges** - À vérifier si nécessaire
14. ❓ **Classification Tarifaire** (5 tables) - À vérifier si utilisé
15. ❓ **TypeTransports** - À vérifier
16. ❓ **UniteDeChargements** - À vérifier
17. ❓ **Incoterms** - À vérifier

---

## 💡 RECOMMANDATION

### Phase 1 : Créer les référentiels prioritaires (HAUTE)
- Pays
- Ports
- Modules
- Devises

### Phase 2 : Créer les référentiels conditionnels (MOYENNE)
- Aéroports
- Fleuves
- Corridors
- BureauxDedouanements
- UniteStatistiques

### Phase 3 : Vérifier et créer les autres si nécessaire (BASSE)
- Vérifier dans le code existant ou demander au métier
- Créer seulement si réellement utilisés

---

## 🎯 CONCLUSION

**Référentiels réellement utilisés** : Environ **9-10 tables** sur les 22+ proposées.

**Approche recommandée** :
1. Créer d'abord les 4 référentiels prioritaires (Pays, Ports, Modules, Devises)
2. Créer ensuite les 5 référentiels conditionnels
3. Vérifier les autres avec le métier avant de les créer

Cela permet de :
- ✅ Aller plus vite
- ✅ Ne créer que ce qui est nécessaire
- ✅ Ajouter les autres plus tard si besoin

---

**Document créé le** : 2025-02-02  
**Version** : 1.0  
**Statut** : Analyse basée sur le document d'étude
