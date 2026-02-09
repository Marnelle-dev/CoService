using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class TauxDeChangeConfiguration : IEntityTypeConfiguration<TauxDeChange>
{
    public void Configure(EntityTypeBuilder<TauxDeChange> builder)
    {
        builder.ToTable("TauxDeChanges");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(t => t.DeviseId)
            .HasColumnName("DeviseId")
            .IsRequired();

        builder.Property(t => t.Source)
            .HasColumnName("Source")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Taux)
            .HasColumnName("Taux")
            .HasColumnType("decimal(20, 5)")
            .IsRequired();

        builder.Property(t => t.ValideDe)
            .HasColumnName("ValideDe")
            .HasMaxLength(50);

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
        builder.HasOne(t => t.Devise)
            .WithMany(d => d.TauxDeChanges)
            .HasForeignKey(t => t.DeviseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
