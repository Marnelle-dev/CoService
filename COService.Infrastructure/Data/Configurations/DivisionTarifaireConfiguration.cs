using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class DivisionTarifaireConfiguration : IEntityTypeConfiguration<DivisionTarifaire>
{
    public void Configure(EntityTypeBuilder<DivisionTarifaire> builder)
    {
        builder.ToTable("DivisionsTariffaires");

        builder.HasKey(dt => dt.Id);

        builder.Property(dt => dt.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(dt => dt.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(dt => dt.Code)
            .IsUnique()
            .HasDatabaseName("IX_DivisionsTariffaires_Code");

        builder.Property(dt => dt.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(dt => dt.ChapitreId)
            .HasColumnName("ChapitreId");

        builder.Property(dt => dt.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(dt => dt.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(dt => dt.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(dt => dt.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(dt => dt.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(dt => dt.Chapitre)
            .WithMany(ct => ct.DivisionsTariffaires)
            .HasForeignKey(dt => dt.ChapitreId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(dt => dt.CategoriesTariffaires)
            .WithOne(cat => cat.Division)
            .HasForeignKey(cat => cat.DivisionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
