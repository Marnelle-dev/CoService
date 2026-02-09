namespace COService.Application.Services;

/// <summary>
/// Service pour la génération des numéros de certificats, abonnements, etc.
/// </summary>
public interface INumeroGenerationService
{
    /// <summary>
    /// Génère un numéro de certificat selon le format : CO{Numéro}{Date}{CodeDépartement}
    /// Exemple : CO100000241031224PNR
    /// </summary>
    Task<string> GenererNumeroCertificatAsync(Guid partenaireId, Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un numéro d'abonnement
    /// Format : {Année}{Mois}{Jour}{Heure}{Minute}{Seconde}{Lettre}
    /// </summary>
    Task<string> GenererNumeroAbonnementAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un numéro de facture
    /// </summary>
    Task<string> GenererNumeroFactureAsync(Guid partenaireId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère le code département d'un partenaire
    /// </summary>
    Task<string?> GetCodeDepartementPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère le dernier numéro séquentiel pour une date et un partenaire donnés
    /// </summary>
    Task<int> GetDernierNumeroSequencielAsync(Guid partenaireId, DateTime date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Extrait le numéro séquentiel d'un numéro de certificat
    /// Exemple : "CO100000241031224PNR" → 100000
    /// </summary>
    int ExtraireNumeroSequenciel(string numeroCertificat);

    /// <summary>
    /// Formate une date pour l'inclure dans un numéro de certificat
    /// Format : ddmmyy (ex: 241031 pour 24/10/2024)
    /// </summary>
    string FormaterDatePourNumero(DateTime date);
}
