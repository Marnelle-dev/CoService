using COService.Application.Repositories;
using COService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace COService.Infrastructure.Messaging.Handlers;

/// <summary>
/// Handler pour les événements d'exportateurs
/// Consomme les événements : exportateur.creé, exportateur.modifié, exportateur.supprimé
/// </summary>
public class ExportateurEventHandler
{
    private readonly IExportateurRepository _exportateurRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ExportateurEventHandler> _logger;

    public ExportateurEventHandler(
        IExportateurRepository exportateurRepository,
        IUnitOfWork unitOfWork,
        ILogger<ExportateurEventHandler> logger)
    {
        _exportateurRepository = exportateurRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Traite l'événement de création/modification d'un exportateur
    /// </summary>
    public async Task HandleExportateurCreeOuModifieAsync(string messageBody, CancellationToken cancellationToken = default)
    {
        try
        {
            var eventData = JsonSerializer.Deserialize<ExportateurEventData>(messageBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (eventData == null)
            {
                _logger.LogWarning("Impossible de désérialiser l'événement exportateur : {MessageBody}", messageBody);
                return;
            }

            // Vérifier si l'exportateur existe déjà
            var exportateurExistant = await _exportateurRepository.GetByIdAsync(eventData.Id, cancellationToken);

            if (exportateurExistant == null)
            {
                // Créer l'exportateur
                var nouvelExportateur = new Exportateur
                {
                    Id = eventData.Id,
                    CodeExportateur = eventData.CodeExportateur,
                    Nom = eventData.Nom,
                    RaisonSociale = eventData.RaisonSociale,
                    NIU = eventData.Niu,
                    RCCM = eventData.Rccm,
                    CodeActivite = eventData.CodeActivite,
                    Adresse = eventData.Adresse,
                    Telephone = eventData.Telephone,
                    Email = eventData.Email,
                    PartenaireId = eventData.PartenaireId,
                    DepartementId = eventData.DepartementId,
                    TypeExportateur = eventData.TypeExportateur,
                    Actif = eventData.Actif,
                    CreeLe = eventData.Timestamp ?? DateTime.UtcNow,
                    CreePar = "SYSTEM",
                    DerniereSynchronisation = DateTime.UtcNow
                };

                await _exportateurRepository.AddAsync(nouvelExportateur, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Exportateur créé depuis événement RabbitMQ : {ExportateurId} ({CodeExportateur})",
                    eventData.Id, eventData.CodeExportateur);
            }
            else
            {
                // Mettre à jour l'exportateur
                exportateurExistant.CodeExportateur = eventData.CodeExportateur;
                exportateurExistant.Nom = eventData.Nom;
                exportateurExistant.RaisonSociale = eventData.RaisonSociale;
                exportateurExistant.NIU = eventData.Niu;
                exportateurExistant.RCCM = eventData.Rccm;
                exportateurExistant.CodeActivite = eventData.CodeActivite;
                exportateurExistant.Adresse = eventData.Adresse;
                exportateurExistant.Telephone = eventData.Telephone;
                exportateurExistant.Email = eventData.Email;
                exportateurExistant.PartenaireId = eventData.PartenaireId;
                exportateurExistant.DepartementId = eventData.DepartementId;
                exportateurExistant.TypeExportateur = eventData.TypeExportateur;
                exportateurExistant.Actif = eventData.Actif;
                exportateurExistant.ModifierLe = DateTime.UtcNow;
                exportateurExistant.ModifiePar = "SYSTEM";
                exportateurExistant.DerniereSynchronisation = DateTime.UtcNow;

                _exportateurRepository.Update(exportateurExistant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Exportateur mis à jour depuis événement RabbitMQ : {ExportateurId} ({CodeExportateur})",
                    eventData.Id, eventData.CodeExportateur);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement exportateur.creé/modifié : {MessageBody}", messageBody);
            throw;
        }
    }

    /// <summary>
    /// Traite l'événement de suppression d'un exportateur
    /// </summary>
    public async Task HandleExportateurSupprimeAsync(string messageBody, CancellationToken cancellationToken = default)
    {
        try
        {
            var eventData = JsonSerializer.Deserialize<ExportateurSupprimeEventData>(messageBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (eventData == null)
            {
                _logger.LogWarning("Impossible de désérialiser l'événement exportateur.supprimé : {MessageBody}", messageBody);
                return;
            }

            var exportateur = await _exportateurRepository.GetByIdAsync(eventData.Id, cancellationToken);
            if (exportateur != null)
            {
                // Marquer comme inactif plutôt que supprimer (soft delete)
                exportateur.Actif = false;
                exportateur.ModifierLe = DateTime.UtcNow;
                exportateur.ModifiePar = "SYSTEM";
                exportateur.DerniereSynchronisation = DateTime.UtcNow;

                _exportateurRepository.Update(exportateur);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Exportateur marqué comme inactif depuis événement RabbitMQ : {ExportateurId}", eventData.Id);
            }
            else
            {
                _logger.LogWarning("Exportateur {ExportateurId} introuvable pour suppression", eventData.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de l'événement exportateur.supprimé : {MessageBody}", messageBody);
            throw;
        }
    }

    // DTOs pour les événements
    private class ExportateurEventData
    {
        public Guid Id { get; set; }
        public string CodeExportateur { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string? RaisonSociale { get; set; }
        public string? Niu { get; set; }
        public string? Rccm { get; set; }
        public string? CodeActivite { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public Guid? PartenaireId { get; set; }
        public Guid? DepartementId { get; set; }
        public int? TypeExportateur { get; set; }
        public bool Actif { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    private class ExportateurSupprimeEventData
    {
        public Guid Id { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
