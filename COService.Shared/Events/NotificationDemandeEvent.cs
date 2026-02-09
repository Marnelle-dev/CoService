namespace COService.Shared.Events;

/// <summary>
/// Événement publié pour demander l'envoi d'une notification
/// Consommé par le microservice Notification
/// </summary>
public class NotificationDemandeEvent
{
    public string Type { get; set; } = string.Empty; // "email", "sms", "push"
    public string Destinataire { get; set; } = string.Empty;
    public string Sujet { get; set; } = string.Empty;
    public string Corps { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
