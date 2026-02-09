using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CorridorConfiguration : IEntityTypeConfiguration<Corridor>
{
    public void Configure(EntityTypeBuilder<Corridor> builder)
    {
        builder.ToTable("Corridors");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(c => c.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(c => c.Code)
            .IsUnique()
            .HasDatabaseName("IX_Corridors_Code");

        builder.Property(c => c.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(c => c.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(c => c.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(c => c.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(c => c.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(c => c.Troncons)
            .WithOne(t => t.Corridor)
            .HasForeignKey(t => t.CorridorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
