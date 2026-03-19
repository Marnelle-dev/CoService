namespace COService.Shared.Events;

/// <summary>
/// Événement publié lorsqu'un certificat d'origine (CO) est validé définitivement.
/// Destiné à informer les autres microservices.
/// </summary>
public class EvenementCOValide
{
    public Guid IdentifiantCO { get; set; }
    public string NumeroCO { get; set; } = string.Empty;
    public Guid? IdentifiantExportateur { get; set; }
    public Guid? IdentifiantPartenaire { get; set; }
    public DateTime DateValidationUtc { get; set; } = DateTime.UtcNow;
}

