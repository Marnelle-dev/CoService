using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CommentaireConfiguration : IEntityTypeConfiguration<Commentaire>
{
    public void Configure(EntityTypeBuilder<Commentaire> builder)
    {
        builder.ToTable("commentaires");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(c => c.CertificateId)
            .HasColumnName("certificate_id")
            .IsRequired();

        builder.Property(c => c.CommentaireText)
            .HasColumnName("Commentaire")
            .HasMaxLength(255);

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
    }
}

