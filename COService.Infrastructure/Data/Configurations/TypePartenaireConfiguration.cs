using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class TypePartenaireConfiguration : IEntityTypeConfiguration<TypePartenaire>
{
    public void Configure(EntityTypeBuilder<TypePartenaire> builder)
    {
        builder.ToTable("TypesPartenaires");

        builder.HasKey(tp => tp.Id);

        builder.Property(tp => tp.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(tp => tp.Code)
            .HasColumnName("Code")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(tp => tp.Code)
            .IsUnique()
            .HasDatabaseName("IX_TypesPartenaires_Code");

        builder.Property(tp => tp.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(tp => tp.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);

        builder.Property(tp => tp.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(tp => tp.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(tp => tp.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(tp => tp.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(tp => tp.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(tp => tp.Partenaires)
            .WithOne(p => p.TypePartenaire)
            .HasForeignKey(p => p.TypePartenaireId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
