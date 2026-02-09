using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("Modules");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(m => m.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(m => m.Code)
            .IsUnique()
            .HasDatabaseName("IX_Modules_Code");

        builder.Property(m => m.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(m => m.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);

        builder.Property(m => m.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(m => m.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(m => m.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(m => m.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(m => m.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(m => m.Incoterms)
            .WithOne(i => i.Module)
            .HasForeignKey(i => i.ModuleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
