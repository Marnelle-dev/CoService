using System.Linq.Expressions;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Interface générique pour les repositories
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Récupère une entité par son ID
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère toutes les entités
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère les entités selon un prédicat
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère la première entité correspondant au prédicat
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ajoute une nouvelle entité
    /// </summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ajoute plusieurs entités
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Met à jour une entité
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Supprime une entité
    /// </summary>
    void Remove(T entity);

    /// <summary>
    /// Supprime plusieurs entités
    /// </summary>
    void RemoveRange(IEnumerable<T> entities);

    /// <summary>
    /// Vérifie si une entité existe selon un prédicat
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Compte les entités selon un prédicat
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}

