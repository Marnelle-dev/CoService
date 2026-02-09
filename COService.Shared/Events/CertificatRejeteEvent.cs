namespace COService.Shared.Events;

/// <summary>
/// Événement publié lorsqu'un certificat est rejeté
/// </summary>
public class CertificatRejeteEvent
{
    public Guid CertificatId { get; set; }
    public string CertificateNo { get; set; } = string.Empty;
    public string Raison { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
