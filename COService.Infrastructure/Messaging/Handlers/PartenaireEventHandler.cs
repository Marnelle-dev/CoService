using COService.Application.Repositories;
using COService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace COService.Infrastructure.Messaging.Handlers;

/// <summary>
/// Handler pour les événements de partenaires (Chambres de Commerce)
/// Consomme les événements : partenaire.creé, partenaire.modifié, partenaire.supprimé
/// </summary>
public class PartenaireEventHandler
{
    private readonly IPartenaireRepository _partenaireRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PartenaireEventHandler> _logger;

    public PartenaireEventHandler(
        IPartenaireRepository partenaireRepository,
        IUnitOfWork unitOfWork,
        ILogger<PartenaireEventHandler> logger)
    {
        _partenaireRepository = partenaireRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Traite l'événement de création/modification d'un partenaire
    /// </summary>
    public async Task HandlePartenaireCreeOuModifieAsync(string messageBody, CancellationToken cancellationToken = default)
    {
        try
        {
            var eventData = JsonSerializer.Deserialize<PartenaireEventData>(messageBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (eventData == null)
            {
                _logger.LogWarning("Impossible de désérialiser l'événement partenaire : {MessageBody}", messageBody);
                return;
            }

            // Vérifier si le partenaire existe déjà
            var partenaireExistant = await _partenaireRepository.GetByIdAsync(eventData.Id, cancellationToken);

            if (partenaireExistant == null)
            {
                // Créer le partenaire
                var nouveauPartenaire = new Partenaire
                {
                    Id = eventData.Id,
                    CodePartenaire = eventData.CodePartenaire,
                    Nom = eventData.Nom,
                    Adresse = eventData.Adresse,
                    Telephone = eventData.Telephone,
                    Email = eventData.Email,
                    TypePartenaireId = eventData.TypePartenaireId,
                    DepartementId = eventData.DepartementId,
                    Actif = eventData.Actif,
                    CreeLe = eventData.Timestamp ?? DateTime.UtcNow,
                    CreePar = "SYSTEM",
                    DerniereSynchronisation = DateTime.UtcNow
                };

                await _partenaireRepository.AddAsync(nouveauPartenaire, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Partenaire créé depuis événement RabbitMQ : {PartenaireId} ({CodePartenaire})",
                    eventData.Id, eventData.CodePartenaire);
            }
            else
            {
                // Mettre à jour le partenaire
                partenaireExistant.CodePartenaire = eventData.CodePartenaire;
                partenaireExistant.Nom = eventData.Nom;
                partenaireExistant.Adresse = eventData.Adresse;
                partenaireExistant.Telephone = eventData.Telephone;
                partenaireExistant.Email = eventData.Email;
                partenaireExistant.TypePartenaireId = eventData.TypePartenaireId;
                partenaireExistant.DepartementId = eventData.DepartementId;
                partenaireExistant.Actif = eventData.Actif;
                partenaireExistant.ModifierLe = DateTime.UtcNow;
                partenaireExistant.ModifiePar = "SYSTEM";
                partenaireExistant.DerniereSynchronisation = DateTime.UtcNow;

                _partenaireRepository.Update(partenaireExistant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Partenaire mis à jour depuis événement RabbitMQ : {PartenaireId} ({CodePartenaire})",
                    eventData.Id, eventData.CodePartenaire);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement partenaire.creé/modifié : {MessageBody}", messageBody);
            throw; // Re-throw pour que RabbitMQ puisse gérer le retry
        }
    }

    /// <summary>
    /// Traite l'événement de suppression d'un partenaire
    /// </summary>
    public async Task HandlePartenaireSupprimeAsync(string messageBody, CancellationToken cancellationToken = default)
    {
        try
        {
            var eventData = JsonSerializer.Deserialize<PartenaireSupprimeEventData>(messageBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (eventData == null)
            {
                _logger.LogWarning("Impossible de désérialiser l'événement partenaire.supprimé : {MessageBody}", messageBody);
                return;
            }

            var partenaire = await _partenaireRepository.GetByIdAsync(eventData.Id, cancellationToken);
            if (partenaire != null)
            {
                // Marquer comme inactif plutôt que supprimer (soft delete)
                partenaire.Actif = false;
                partenaire.ModifierLe = DateTime.UtcNow;
                partenaire.ModifiePar = "SYSTEM";
                partenaire.DerniereSynchronisation = DateTime.UtcNow;

                _partenaireRepository.Update(partenaire);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Partenaire marqué comme inactif depuis événement RabbitMQ : {PartenaireId}", eventData.Id);
            }
            else
            {
                _logger.LogWarning("Partenaire {PartenaireId} introuvable pour suppression", eventData.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement partenaire.supprimé : {MessageBody}", messageBody);
            throw;
        }
    }

    // DTOs pour les événements
    private class PartenaireEventData
    {
        public Guid Id { get; set; }
        public string CodePartenaire { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public Guid? TypePartenaireId { get; set; }
        public Guid? DepartementId { get; set; }
        public bool Actif { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    private class PartenaireSupprimeEventData
    {
        public Guid Id { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
