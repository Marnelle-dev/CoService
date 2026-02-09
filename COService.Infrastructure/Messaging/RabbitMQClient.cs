using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace COService.Infrastructure.Messaging;

/// <summary>
/// Client RabbitMQ pour publier des événements
/// </summary>
public class RabbitMQClient : IRabbitMQClient
{
    private readonly RabbitMQOptions _options;
    private readonly ILogger<RabbitMQClient> _logger;
    private IConnection? _connection;
    private IModel? _channel;
    private readonly object _lock = new object();

    public bool IsConnected => _connection?.IsOpen == true && _channel?.IsOpen == true;

    public RabbitMQClient(IOptions<RabbitMQOptions> options, ILogger<RabbitMQClient> logger)
    {
        _options = options.Value;
        _logger = logger;
        
        // Ne pas se connecter si RabbitMQ est désactivé
        if (_options.Enabled)
        {
            Connect();
        }
        else
        {
            _logger.LogInformation("RabbitMQ est désactivé. Les événements ne seront pas publiés.");
        }
    }

    private void Connect()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Déclarer l'exchange (topic, durable)
            _channel.ExchangeDeclare(
                exchange: _options.Exchange,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);

            _logger.LogInformation(
                "Connexion RabbitMQ établie : {HostName}:{Port}, Exchange: {Exchange}",
                _options.HostName, _options.Port, _options.Exchange);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Impossible de se connecter à RabbitMQ. L'application continuera de fonctionner mais les événements ne seront pas publiés.");
            _connection = null;
            _channel = null;
        }
    }

    public async Task PublishAsync(string routingKey, object message, CancellationToken cancellationToken = default)
    {
        // Si RabbitMQ est désactivé, ne rien faire
        if (!_options.Enabled)
        {
            _logger.LogDebug("RabbitMQ désactivé, événement non publié : {RoutingKey}", routingKey);
            await Task.CompletedTask;
            return;
        }

        if (!IsConnected)
        {
            _logger.LogWarning("RabbitMQ non connecté, tentative de reconnexion...");
            Connect();
        }

        if (!IsConnected)
        {
            _logger.LogWarning("Impossible de publier l'événement {RoutingKey}, RabbitMQ non connecté. L'application continue de fonctionner.", routingKey);
            await Task.CompletedTask;
            return;
        }

        try
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = _channel!.CreateBasicProperties();
            properties.Persistent = true; // Message persistant
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.MessageId = Guid.NewGuid().ToString();

            _channel.BasicPublish(
                exchange: _options.Exchange,
                routingKey: routingKey,
                basicProperties: properties,
                body: body);

            _logger.LogInformation(
                "Événement publié : RoutingKey={RoutingKey}, Exchange={Exchange}",
                routingKey, _options.Exchange);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erreur lors de la publication de l'événement {RoutingKey}. L'application continue de fonctionner.", routingKey);
            // Ne pas lever l'exception pour permettre à l'application de continuer
        }

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}
