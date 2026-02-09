using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class IncotermConfiguration : IEntityTypeConfiguration<Incoterm>
{
    public void Configure(EntityTypeBuilder<Incoterm> builder)
    {
        builder.ToTable("Incoterms");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(i => i.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(i => i.Code)
            .IsUnique()
            .HasDatabaseName("IX_Incoterms_Code");

        builder.Property(i => i.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.ModuleId)
            .HasColumnName("ModuleId");

        builder.Property(i => i.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(i => i.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(i => i.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(i => i.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(i => i.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(i => i.Module)
            .WithMany(m => m.Incoterms)
            .HasForeignKey(i => i.ModuleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
