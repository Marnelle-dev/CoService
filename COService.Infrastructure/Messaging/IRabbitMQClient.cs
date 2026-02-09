namespace COService.Infrastructure.Messaging;

/// <summary>
/// Client RabbitMQ pour publier des événements
/// </summary>
public interface IRabbitMQClient : IDisposable
{
    /// <summary>
    /// Publie un événement sur l'exchange
    /// </summary>
    Task PublishAsync(string routingKey, object message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Vérifie la connexion à RabbitMQ
    /// </summary>
    bool IsConnected { get; }
}
