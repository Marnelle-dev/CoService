namespace COService.Application.Repositories;

/// <summary>
/// Interface pour l'unité de travail (Unit of Work pattern)
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Sauvegarde tous les changements dans le contexte
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sauvegarde tous les changements dans le contexte (synchrone)
    /// </summary>
    int SaveChanges();
}

