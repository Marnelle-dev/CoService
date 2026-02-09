namespace COService.Shared.Events;

/// <summary>
/// Événement publié lorsqu'un certificat est créé
/// </summary>
public class CertificatCreeEvent
{
    public Guid CertificatId { get; set; }
    public string CertificateNo { get; set; } = string.Empty;
    public Guid? ExportateurId { get; set; }
    public Guid? PartenaireId { get; set; }
    public string Statut { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
