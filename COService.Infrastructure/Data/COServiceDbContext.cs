using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Data;

/// <summary>
/// DbContext pour le microservice COService
/// </summary>
public class COServiceDbContext : DbContext
{
    public COServiceDbContext(DbContextOptions<COServiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<CertificatOrigine> CertificatsOrigine { get; set; }
    public DbSet<CertificateLine> CertificateLines { get; set; }
    public DbSet<CertificateValidation> CertificateValidations { get; set; }
    public DbSet<Commentaire> Commentaires { get; set; }
    public DbSet<Abonnement> Abonnements { get; set; }
    public DbSet<CertificateType> CertificateTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Appliquer toutes les configurations depuis l'assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(COServiceDbContext).Assembly);
    }
}

