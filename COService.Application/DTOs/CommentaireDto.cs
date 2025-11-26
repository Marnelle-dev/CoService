namespace COService.Application.DTOs;

/// <summary>
/// DTO pour un commentaire
/// </summary>
public class CommentaireDto
{
    public Guid Id { get; set; }
    public Guid CertificateId { get; set; }
    public string? CommentaireText { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}

/// <summary>
/// DTO pour créer un commentaire
/// </summary>
public class CreerCommentaireDto
{
    public string? CommentaireText { get; set; }
}

/// <summary>
/// DTO pour modifier un commentaire
/// </summary>
public class ModifierCommentaireDto
{
    public string? CommentaireText { get; set; }
}

