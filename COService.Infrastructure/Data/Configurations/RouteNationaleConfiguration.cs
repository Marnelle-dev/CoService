using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class RouteNationaleConfiguration : IEntityTypeConfiguration<RouteNationale>
{
    public void Configure(EntityTypeBuilder<RouteNationale> builder)
    {
        builder.ToTable("RoutesNationales");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(r => r.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(r => r.Code)
            .IsUnique()
            .HasDatabaseName("IX_RoutesNationales_Code");

        builder.Property(r => r.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(r => r.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(r => r.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(r => r.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(r => r.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(r => r.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(r => r.Troncons)
            .WithOne(t => t.Route)
            .HasForeignKey(t => t.RouteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
