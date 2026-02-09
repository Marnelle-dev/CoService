namespace COService.Infrastructure.Messaging;

/// <summary>
/// Options de configuration pour RabbitMQ
/// </summary>
public class RabbitMQOptions
{
    public const string SectionName = "RabbitMQ";

    /// <summary>
    /// Active ou désactive RabbitMQ. Si false, le service de consommation ne démarrera pas.
    /// </summary>
    public bool Enabled { get; set; } = true;

    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public string Exchange { get; set; } = "coservice";
}
