using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using COService.Infrastructure.Messaging.Handlers;

namespace COService.Infrastructure.Messaging;

/// <summary>
/// Service hébergé pour consommer les événements RabbitMQ
/// </summary>
public class RabbitMQConsumerService : BackgroundService
{
    private readonly RabbitMQOptions _options;
    private readonly ILogger<RabbitMQConsumerService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMQConsumerService(
        IOptions<RabbitMQOptions> options,
        ILogger<RabbitMQConsumerService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _options = options.Value;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Vérifier si RabbitMQ est activé
        if (!_options.Enabled)
        {
            _logger.LogInformation("RabbitMQ est désactivé. Le service de consommation ne démarrera pas.");
            return;
        }

        await ConnectAsync(stoppingToken);

        if (_connection == null || _channel == null)
        {
            _logger.LogWarning("Impossible de se connecter à RabbitMQ. Le service de consommation ne démarrera pas. L'application continuera de fonctionner sans synchronisation via RabbitMQ.");
            return;
        }

        // Configurer les queues et consommer les messages
        await SetupQueuesAndConsumersAsync(stoppingToken);

        // Attendre jusqu'à l'annulation
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task ConnectAsync(CancellationToken cancellationToken)
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

            // Déclarer l'exchange
            _channel.ExchangeDeclare(
                exchange: _options.Exchange,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);

            _logger.LogInformation(
                "Connexion RabbitMQ établie pour la consommation : {HostName}:{Port}, Exchange: {Exchange}",
                _options.HostName, _options.Port, _options.Exchange);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la connexion à RabbitMQ pour la consommation. L'application continuera de fonctionner sans synchronisation via RabbitMQ.");
            // Ne pas lever l'exception pour permettre à l'application de démarrer même sans RabbitMQ
            _connection = null;
            _channel = null;
        }

        await Task.CompletedTask;
    }

    private async Task SetupQueuesAndConsumersAsync(CancellationToken cancellationToken)
    {
        if (_channel == null) return;

        // Queue pour les partenaires
        var partenaireQueue = "coservice.partenaires";
        _channel.QueueDeclare(partenaireQueue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(partenaireQueue, _options.Exchange, "partenaire.*");

        var partenaireConsumer = new EventingBasicConsumer(_channel);
        partenaireConsumer.Received += async (model, ea) =>
        {
            await HandlePartenaireEventAsync(ea, cancellationToken);
        };
        _channel.BasicConsume(partenaireQueue, autoAck: false, consumer: partenaireConsumer);
        _logger.LogInformation("Consumer configuré pour la queue : {Queue}", partenaireQueue);

        // Queue pour les exportateurs
        var exportateurQueue = "coservice.exportateurs";
        _channel.QueueDeclare(exportateurQueue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(exportateurQueue, _options.Exchange, "exportateur.*");

        var exportateurConsumer = new EventingBasicConsumer(_channel);
        exportateurConsumer.Received += async (model, ea) =>
        {
            await HandleExportateurEventAsync(ea, cancellationToken);
        };
        _channel.BasicConsume(exportateurQueue, autoAck: false, consumer: exportateurConsumer);
        _logger.LogInformation("Consumer configuré pour la queue : {Queue}", exportateurQueue);

        // Queue pour les référentiels
        var referentielQueue = "coservice.referentiels";
        _channel.QueueDeclare(referentielQueue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(referentielQueue, _options.Exchange, "referentiel.*");

        var referentielConsumer = new EventingBasicConsumer(_channel);
        referentielConsumer.Received += async (model, ea) =>
        {
            await HandleReferentielEventAsync(ea, cancellationToken);
        };
        _channel.BasicConsume(referentielQueue, autoAck: false, consumer: referentielConsumer);
        _logger.LogInformation("Consumer configuré pour la queue : {Queue}", referentielQueue);

        await Task.CompletedTask;
    }

    private async Task HandlePartenaireEventAsync(BasicDeliverEventArgs ea, CancellationToken cancellationToken)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;

        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<PartenaireEventHandler>();

        try
        {
            if (routingKey == "partenaire.creé" || routingKey == "partenaire.modifié")
            {
                await handler.HandlePartenaireCreeOuModifieAsync(message, cancellationToken);
            }
            else if (routingKey == "partenaire.supprimé")
            {
                await handler.HandlePartenaireSupprimeAsync(message, cancellationToken);
            }

            // Acknowledger le message après traitement réussi
            _channel?.BasicAck(ea.DeliveryTag, false);
            _logger.LogDebug("Événement partenaire traité avec succès : {RoutingKey}", routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement partenaire : {RoutingKey}", routingKey);
            // Rejeter le message et le renvoyer à la queue (ou DLQ)
            _channel?.BasicNack(ea.DeliveryTag, false, true);
        }
    }

    private async Task HandleExportateurEventAsync(BasicDeliverEventArgs ea, CancellationToken cancellationToken)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;

        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ExportateurEventHandler>();

        try
        {
            if (routingKey == "exportateur.creé" || routingKey == "exportateur.modifié")
            {
                await handler.HandleExportateurCreeOuModifieAsync(message, cancellationToken);
            }
            else if (routingKey == "exportateur.supprimé")
            {
                await handler.HandleExportateurSupprimeAsync(message, cancellationToken);
            }

            _channel?.BasicAck(ea.DeliveryTag, false);
            _logger.LogDebug("Événement exportateur traité avec succès : {RoutingKey}", routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement exportateur : {RoutingKey}", routingKey);
            _channel?.BasicNack(ea.DeliveryTag, false, true);
        }
    }

    private async Task HandleReferentielEventAsync(BasicDeliverEventArgs ea, CancellationToken cancellationToken)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;

        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ReferentielEventHandler>();

        try
        {
            await handler.HandleReferentielEventAsync(routingKey, message, cancellationToken);
            _channel?.BasicAck(ea.DeliveryTag, false);
            _logger.LogDebug("Événement référentiel traité avec succès : {RoutingKey}", routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement référentiel : {RoutingKey}", routingKey);
            _channel?.BasicNack(ea.DeliveryTag, false, true);
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
        base.Dispose();
    }
}
