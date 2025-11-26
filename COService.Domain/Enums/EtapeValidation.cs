namespace COService.Domain.Enums;

/// <summary>
/// Étape de validation dans le workflow
/// </summary>
public enum EtapeValidation
{
    /// <summary>
    /// Étape de contrôle
    /// </summary>
    Controle = 0,

    /// <summary>
    /// Étape d'approbation
    /// </summary>
    Approbation = 1,

    /// <summary>
    /// Étape de validation finale
    /// </summary>
    Validation = 2
}

