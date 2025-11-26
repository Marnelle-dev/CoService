namespace COService.Domain.Entities;

/// <summary>
/// Commentaire sur un certificat d'origine
/// </summary>
public class Commentaire
{
    /// <summary>
    /// Identifiant unique du commentaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Référence au certificat
    /// </summary>
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Texte du commentaire
    /// </summary>
    public string? CommentaireText { get; set; }

    // Champs d'audit
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime? CreeLe { get; set; }

    /// <summary>
    /// Auteur du commentaire
    /// </summary>
    public string? CreePar { get; set; }

    /// <summary>
    /// Date de dernière modification
    /// </summary>
    public DateTime? ModifierLe { get; set; }

    /// <summary>
    /// Utilisateur ayant modifié
    /// </summary>
    public string? ModifiePar { get; set; }

    // Navigation property
    /// <summary>
    /// Certificat parent
    /// </summary>
    public CertificatOrigine CertificatOrigine { get; set; } = null!;
}

