using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class SectionTarifaireConfiguration : IEntityTypeConfiguration<SectionTarifaire>
{
    public void Configure(EntityTypeBuilder<SectionTarifaire> builder)
    {
        builder.ToTable("SectionsTariffaires");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(st => st.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(st => st.Code)
            .IsUnique()
            .HasDatabaseName("IX_SectionsTariffaires_Code");

        builder.Property(st => st.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(st => st.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(st => st.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(st => st.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(st => st.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(st => st.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(st => st.ChapitresTariffaires)
            .WithOne(ct => ct.SectionTarifaire)
            .HasForeignKey(ct => ct.SectionTarifaireId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
