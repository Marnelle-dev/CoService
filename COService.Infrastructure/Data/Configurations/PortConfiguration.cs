using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class PortConfiguration : IEntityTypeConfiguration<Port>
{
    public void Configure(EntityTypeBuilder<Port> builder)
    {
        builder.ToTable("Ports");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(p => p.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(p => p.Code)
            .IsUnique()
            .HasDatabaseName("IX_Ports_Code");

        builder.Property(p => p.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.PaysId)
            .HasColumnName("PaysId");

        builder.Property(p => p.Type)
            .HasColumnName("Type")
            .HasMaxLength(20); // Maritime, Fluvial

        builder.Property(p => p.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(p => p.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(p => p.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(p => p.Pays)
            .WithMany(pays => pays.Ports)
            .HasForeignKey(p => p.PaysId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
