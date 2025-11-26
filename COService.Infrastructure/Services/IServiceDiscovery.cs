namespace COService.Infrastructure.Services;

/// <summary>
/// Interface pour la découverte de services via Consul
/// </summary>
public interface IServiceDiscovery
{
    /// <summary>
    /// Découvre l'adresse d'un service par son nom
    /// </summary>
    /// <param name="serviceName">Nom du service à découvrir</param>
    /// <returns>L'URL du service ou null si non trouvé</returns>
    Task<string?> DiscoverServiceAsync(string serviceName);

    /// <summary>
    /// Découvre toutes les instances d'un service
    /// </summary>
    /// <param name="serviceName">Nom du service</param>
    /// <returns>Liste des URLs des instances du service</returns>
    Task<List<string>> DiscoverServiceInstancesAsync(string serviceName);
}

