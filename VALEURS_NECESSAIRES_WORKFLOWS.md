# 📋 VALEURS NÉCESSAIRES POUR IMPLÉMENTER LES WORKFLOWS

## ✅ Valeurs Déjà Connues

### Codes Chambres de Commerce
- **Pointe-Noire** : `CodePartenaire = "CCIAM-PNR"`
- **Ouesso** : `CodePartenaire = "CCIAM-OUE"`

---

## ❓ Valeurs à Confirmer

### 1. Codes Département pour Génération Numéros

**Question** : Pour la génération des numéros de certificats, utilise-t-on :
- Le code complet `"CCIAM-PNR"` / `"CCIAM-OUE"` ?
- OU seulement le suffixe `"PNR"` / `"OUE"` ?

**Format actuel dans le document** : `CO{Numéro}{Date}{CodeDépartement}`
- Exemple Pointe-Noire : `CO100000241031224PNR` (utilise "PNR")
- Exemple Ouesso : `CO100000241031224OUE` (utilise "OUE")

**Proposition** : Utiliser `"PNR"` et `"OUE"` pour la génération des numéros (suffixe du CodePartenaire)

---

### 2. Codes Rôles Utilisateurs

**Question** : Comment identifier les rôles dans le système d'authentification externe ?

**Rôles nécessaires selon les workflows** :
- **Rôle 3** : Contrôleur
- **Rôle 4** : Superviseur  
- **Rôle 6** : Président
- **Rôle 84** : Exportateur (pour création de certificats)

**Options possibles** :
- A) Codes numériques : `"3"`, `"4"`, `"6"`, `"84"`
- B) Codes string : `"CONTROLEUR"`, `"SUPERVISEUR"`, `"PRESIDENT"`, `"EXPORTATEUR"`
- C) Autre format ?

**À confirmer** : Quel format utilise le microservice d'authentification pour identifier les rôles ?

---

### 3. Codes Types de Certificats / Formules

**Question** : Quels sont les codes exacts pour les types de certificats ?

**Types mentionnés dans les workflows** :

#### Pointe-Noire :
- **CO Simple** : `formule = "CO"` ?
- **CO + Formule A Cargo Commun** : `formule = "B"` ?

#### Ouesso :
- **Certificat d'Origine** : `formule = "CO"` ?
- **CO + EUR-1** : `formule = "EUR-1"` ?
- **CO + ALC** : `formule = "CO+ALC"` ?
- **Formule A** : `is_formule_a = true` ?

**À confirmer** : 
- Les codes exacts pour chaque type
- Comment distinguer un CO simple d'un CO+EUR-1 d'un CO+ALC ?
- Y a-t-il un champ `TypeCertificat` ou `Formule` dans `CertificatOrigine` ?

---

### 4. Statuts de Certificats

**Question** : L'enum `StatutCertificat` actuel correspond-il aux statuts du workflow ?

**Statuts nécessaires selon les workflows** :
- **1** : Élaboré
- **2** : Soumis
- **4** : Contrôlé
- **5** : Rejeté
- **7** : Approuvé
- **8** : Validé
- **10** : Modification
- **12** : Formule A soumise
- **13** : Formule A contrôlée
- **14** : Formule A approuvée
- **15** : Formule A validée

**Enum actuel** :
```csharp
public enum StatutCertificat
{
    Elabore = 0,    // Devrait être 1 ?
    Soumis = 1,     // Devrait être 2 ?
    Controle = 2,   // Devrait être 4 ?
    Approuve = 3,   // Devrait être 7 ?
    Valide = 4      // Devrait être 8 ?
}
```

**À confirmer** :
- Les valeurs numériques de l'enum doivent-elles correspondre aux statuts du workflow (1, 2, 4, 5, 7, 8, etc.) ?
- Ou l'enum peut-il garder des valeurs séquentielles (0, 1, 2, 3, 4) et on fait un mapping ?
- Comment gérer les statuts manquants (5, 10, 12, 13, 14, 15) ?

---

### 5. Vérification Mot de Passe

**Question** : Comment vérifier le mot de passe utilisateur ?

**Options** :
- A) Appel API au microservice d'authentification pour vérifier le mot de passe
- B) Le mot de passe est envoyé dans le DTO et vérifié via Auth Service
- C) Autre mécanisme ?

**À confirmer** : 
- Le microservice Auth expose-t-il un endpoint pour vérifier un mot de passe ?
- Ou le mot de passe est-il vérifié côté client et on reçoit juste un token ?

---

### 6. Vérification Organisation Utilisateur

**Question** : Comment vérifier qu'un utilisateur appartient à la même organisation qu'un certificat ?

**Cas d'usage** :
- Le Président (rôle 6) doit appartenir à la même chambre de commerce que le certificat
- Exemple : Un Président de Pointe-Noire ne peut pas valider un certificat d'Ouesso

**À confirmer** :
- Le microservice Auth fournit-il l'`organisation_id` ou `CodePartenaire` de l'utilisateur ?
- Comment récupérer cette information depuis le token JWT ou l'API Auth ?

---

## 📝 Résumé des Questions

1. ✅ **Codes Chambres** : `"CCIAM-PNR"` et `"CCIAM-OUE"` (confirmé)
2. ❓ **Codes Département** : `"PNR"` / `"OUE"` pour génération numéros ?
3. ❓ **Codes Rôles** : Format exact (numérique, string, autre) ?
4. ❓ **Codes Formules** : Codes exacts pour chaque type de certificat ?
5. ❓ **Statuts** : Mapping entre enum et valeurs workflow ?
6. ❓ **Mot de passe** : Mécanisme de vérification ?
7. ❓ **Organisation utilisateur** : Comment récupérer depuis Auth Service ?

---

**Document créé le** : 2025-02-04  
**Version** : 1.0  
**Statut** : ⚠️ En attente de confirmation des valeurs
