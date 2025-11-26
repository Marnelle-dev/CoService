namespace COService.Domain.Enums;

/// <summary>
/// Statut d'un certificat d'origine
/// Les valeurs numériques (0, 1, 2, 3, 4) sont utilisées en interne par C#
/// Elles sont converties en string pour la base de données selon le dictionnaire de données
/// </summary>
public enum StatutCertificat
{
    /// <summary>
    /// Certificat en cours d'élaboration
    /// Valeur en base : "Élaboré"
    /// </summary>
    Elabore = 0,

    /// <summary>
    /// Certificat soumis pour validation
    /// Valeur en base : "Soumis"
    /// </summary>
    Soumis = 1,

    /// <summary>
    /// Certificat contrôlé
    /// Valeur en base : "Contrôlé"
    /// </summary>
    Controle = 2,

    /// <summary>
    /// Certificat approuvé
    /// Valeur en base : "Approuvé"
    /// </summary>
    Approuve = 3,

    /// <summary>
    /// Certificat validé définitivement
    /// Valeur en base : "Validé"
    /// </summary>
    Valide = 4
}

