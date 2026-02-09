using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;

namespace COService.Infrastructure.ExternalServices;

/// <summary>
/// Wrapper pour le client Auth Service via l'API Gateway (Apache APISIX)
/// </summary>
public class AuthServiceClientWrapper : IAuthServiceClient
{
    private readonly ILogger<AuthServiceClientWrapper> _logger;
    private readonly IAuthServiceClient _client;

    public AuthServiceClientWrapper(
        ILogger<AuthServiceClientWrapper> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        
        // Utiliser Apache APISIX API Gateway
        var apiGatewayUrl = configuration.GetValue<string>("ApiGateway:BaseUrl") 
            ?? throw new InvalidOperationException("ApiGateway:BaseUrl non configuré");
        
        var authConfig = configuration.GetSection("ExternalServices:AuthService");
        var authPath = authConfig.GetValue<string>("Path") ?? "/api/auth";
        var timeout = authConfig.GetValue<int>("Timeout", 30);
        
        var baseAddress = $"{apiGatewayUrl.TrimEnd('/')}{authPath}";
        
        _client = RestService.For<IAuthServiceClient>(
            new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(timeout)
            });

        _logger.LogInformation("Client Auth Service configuré via API Gateway: {BaseAddress}", baseAddress);
    }

    public async Task<UserInfoDto> GetUserInfoAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _client.GetUserInfoAsync(userId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des informations utilisateur {UserId}", userId);
            throw;
        }
    }

    public async Task<bool> VerifierRoleAsync(string userId, string role, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _client.VerifierRoleAsync(userId, role, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la vérification du rôle {Role} pour l'utilisateur {UserId}", role, userId);
            return false;
        }
    }

    public async Task<List<string>> GetRolesAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _client.GetRolesAsync(userId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des rôles pour l'utilisateur {UserId}", userId);
            return new List<string>();
        }
    }

    public async Task<bool> VerifierMotDePasseAsync(string userId, VerifyPasswordRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _client.VerifierMotDePasseAsync(userId, request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la vérification du mot de passe pour l'utilisateur {UserId}", userId);
            return false;
        }
    }

    public async Task<bool> VerifierOrganisationAsync(string userId, Guid organisationId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _client.VerifierOrganisationAsync(userId, organisationId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la vérification de l'organisation {OrganisationId} pour l'utilisateur {UserId}", organisationId, userId);
            return false;
        }
    }
}
