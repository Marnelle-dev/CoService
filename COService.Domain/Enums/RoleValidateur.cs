namespace COService.Domain.Enums;

/// <summary>
/// Rôle du validateur dans le workflow
/// </summary>
public enum RoleValidateur
{
    /// <summary>
    /// Contrôleur (CCI)
    /// </summary>
    Controleur = 0,

    /// <summary>
    /// Superviseur (CCI)
    /// </summary>
    Superviseur = 1,

    /// <summary>
    /// Signataire (CCI)
    /// </summary>
    Signataire = 2
}

