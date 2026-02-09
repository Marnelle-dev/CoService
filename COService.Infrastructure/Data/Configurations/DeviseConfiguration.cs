using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class DeviseConfiguration : IEntityTypeConfiguration<Devise>
{
    public void Configure(EntityTypeBuilder<Devise> builder)
    {
        builder.ToTable("Devises");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(d => d.Code)
            .HasColumnName("Code")
            .HasMaxLength(3)
            .IsRequired();

        builder.HasIndex(d => d.Code)
            .IsUnique()
            .HasDatabaseName("IX_Devises_Code");

        builder.Property(d => d.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(d => d.Symbole)
            .HasColumnName("Symbole")
            .HasMaxLength(10);

        builder.Property(d => d.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(d => d.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(d => d.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(d => d.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(d => d.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(d => d.TauxDeChanges)
            .WithOne(t => t.Devise)
            .HasForeignKey(t => t.DeviseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
