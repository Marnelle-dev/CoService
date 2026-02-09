using COService.Application.Repositories;
using COService.Shared.Constants;
using Microsoft.Extensions.Logging;

namespace COService.Application.Services;

/// <summary>
/// Service pour la génération des numéros de certificats, abonnements, etc.
/// </summary>
public class NumeroGenerationService : INumeroGenerationService
{
    private readonly IPartenaireRepository _partenaireRepository;
    private readonly ICertificatOrigineRepository _certificatRepository;
    private readonly ILogger<NumeroGenerationService> _logger;

    public NumeroGenerationService(
        IPartenaireRepository partenaireRepository,
        ICertificatOrigineRepository certificatRepository,
        ILogger<NumeroGenerationService> logger)
    {
        _partenaireRepository = partenaireRepository;
        _certificatRepository = certificatRepository;
        _logger = logger;
    }

    public async Task<string> GenererNumeroCertificatAsync(Guid partenaireId, Guid certificatId, CancellationToken cancellationToken = default)
    {
        // 1. Récupérer le partenaire
        var partenaire = await _partenaireRepository.GetByIdAsync(partenaireId, cancellationToken)
            ?? throw new KeyNotFoundException($"Partenaire {partenaireId} introuvable");

        // 2. Récupérer le code département
        var codeDepartement = await GetCodeDepartementPartenaireAsync(partenaireId, cancellationToken)
            ?? throw new InvalidOperationException($"Impossible de déterminer le code département pour le partenaire {partenaireId}");

        // 3. Formater la date actuelle
        var dateFormatee = FormaterDatePourNumero(DateTime.UtcNow);

        // 4. Récupérer le dernier numéro séquentiel pour cette date et ce partenaire
        var dernierNumero = await GetDernierNumeroSequencielAsync(partenaireId, DateTime.UtcNow.Date, cancellationToken);

        // 5. Incrémenter
        var nouveauNumero = dernierNumero + 1;

        // 6. Construire le numéro : CO{Numéro}{Date}{CodeDépartement}
        var numeroCertificat = $"CO{nouveauNumero:D6}{dateFormatee}{codeDepartement}";

        _logger.LogInformation(
            "Numéro de certificat généré : {Numero} pour le partenaire {PartenaireId}",
            numeroCertificat, partenaireId);

        return numeroCertificat;
    }

    public Task<string> GenererNumeroAbonnementAsync(CancellationToken cancellationToken = default)
    {
        var maintenant = DateTime.UtcNow;
        var numero = $"{maintenant:yyyyMMddHHmmss}{GetLettreAleatoire()}";
        
        _logger.LogInformation("Numéro d'abonnement généré : {Numero}", numero);
        return Task.FromResult(numero);
    }

    public async Task<string> GenererNumeroFactureAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        var maintenant = DateTime.UtcNow;
        var codePartenaire = await GetCodeDepartementPartenaireAsync(partenaireId, cancellationToken) ?? "XXX";
        var numero = $"FACT{maintenant:yyyyMMdd}{codePartenaire}{maintenant:HHmmss}";
        
        _logger.LogInformation("Numéro de facture généré : {Numero} pour le partenaire {PartenaireId}", numero, partenaireId);
        return numero;
    }

    public async Task<string?> GetCodeDepartementPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        var partenaire = await _partenaireRepository.GetByIdAsync(partenaireId, cancellationToken);
        
        if (partenaire?.Departement == null)
        {
            _logger.LogWarning("Partenaire {PartenaireId} n'a pas de département associé", partenaireId);
            return null;
        }

        return partenaire.Departement.Code;
    }

    public async Task<int> GetDernierNumeroSequencielAsync(Guid partenaireId, DateTime date, CancellationToken cancellationToken = default)
    {
        // Récupérer tous les certificats du partenaire
        var certificats = await _certificatRepository.GetAllAsync(cancellationToken);
        
        var certificatsPartenaire = certificats
            .Where(c => c.PartenaireId == partenaireId && c.CertificateNo.StartsWith("CO"))
            .ToList();

        var dateFormatee = FormaterDatePourNumero(date);
        var codeDepartement = await GetCodeDepartementPartenaireAsync(partenaireId, cancellationToken);

        if (string.IsNullOrEmpty(codeDepartement))
        {
            return 0;
        }

        // Filtrer les certificats qui correspondent à la date et au code département
        var certificatsDate = certificatsPartenaire
            .Where(c => c.CertificateNo.EndsWith($"{dateFormatee}{codeDepartement}"))
            .ToList();

        if (!certificatsDate.Any())
        {
            return 0;
        }

        // Extraire les numéros séquentiels et trouver le maximum
        var numeros = certificatsDate
            .Select(c => ExtraireNumeroSequenciel(c.CertificateNo))
            .Where(n => n > 0)
            .ToList();

        return numeros.Any() ? numeros.Max() : 0;
    }

    public int ExtraireNumeroSequenciel(string numeroCertificat)
    {
        // Format attendu : CO{Numéro}{Date}{CodeDépartement}
        // Exemple : CO100000241031224PNR
        // Le numéro séquentiel est entre "CO" et la date (6 chiffres)

        if (string.IsNullOrWhiteSpace(numeroCertificat) || !numeroCertificat.StartsWith("CO"))
        {
            return 0;
        }

        try
        {
            // Enlever "CO" au début
            var sansPrefixe = numeroCertificat.Substring(2);
            
            // Le numéro séquentiel fait 6 chiffres (format :D6)
            if (sansPrefixe.Length < 6)
            {
                return 0;
            }

            var numeroStr = sansPrefixe.Substring(0, 6);
            if (int.TryParse(numeroStr, out var numero))
            {
                return numero;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erreur lors de l'extraction du numéro séquentiel de {Numero}", numeroCertificat);
        }

        return 0;
    }

    public string FormaterDatePourNumero(DateTime date)
    {
        // Format : ddmmyy
        // Exemple : 24/10/2024 → 241024
        return date.ToString("ddMMyy");
    }

    private char GetLettreAleatoire()
    {
        var random = new Random();
        return (char)('A' + random.Next(0, 26));
    }
}
