using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class TronconConfiguration : IEntityTypeConfiguration<Troncon>
{
    public void Configure(EntityTypeBuilder<Troncon> builder)
    {
        builder.ToTable("Troncons");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(t => t.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(t => t.Code)
            .IsUnique()
            .HasDatabaseName("IX_Troncons_Code");

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.CorridorId)
            .HasColumnName("CorridorId");

        builder.Property(t => t.RouteId)
            .HasColumnName("RouteId");

        builder.Property(t => t.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(t => t.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(t => t.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(t => t.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(t => t.Corridor)
            .WithMany(c => c.Troncons)
            .HasForeignKey(t => t.CorridorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(t => t.Route)
            .WithMany(r => r.Troncons)
            .HasForeignKey(t => t.RouteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
