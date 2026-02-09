namespace COService.Shared.Events;

/// <summary>
/// Événement publié lorsqu'un certificat change de statut
/// </summary>
public class CertificatStatutChangeEvent
{
    public Guid CertificatId { get; set; }
    public string AncienStatut { get; set; } = string.Empty;
    public string NouveauStatut { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
