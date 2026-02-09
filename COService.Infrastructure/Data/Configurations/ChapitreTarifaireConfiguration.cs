using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class ChapitreTarifaireConfiguration : IEntityTypeConfiguration<ChapitreTarifaire>
{
    public void Configure(EntityTypeBuilder<ChapitreTarifaire> builder)
    {
        builder.ToTable("ChapitresTariffaires");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(ct => ct.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(ct => ct.Code)
            .IsUnique()
            .HasDatabaseName("IX_ChapitresTariffaires_Code");

        builder.Property(ct => ct.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(ct => ct.SectionTarifaireId)
            .HasColumnName("SectionTarifaireId");

        builder.Property(ct => ct.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(ct => ct.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(ct => ct.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(ct => ct.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(ct => ct.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(ct => ct.SectionTarifaire)
            .WithMany(st => st.ChapitresTariffaires)
            .HasForeignKey(ct => ct.SectionTarifaireId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(ct => ct.DivisionsTariffaires)
            .WithOne(dt => dt.Chapitre)
            .HasForeignKey(dt => dt.ChapitreId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
