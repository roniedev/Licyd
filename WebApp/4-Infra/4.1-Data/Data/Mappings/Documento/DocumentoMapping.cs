using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class DocumentoMapping : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Observacao).HasMaxLength(512);

        builder.HasMany(x => x.Anexos)
            .WithOne(x => x.Documento)
            .HasForeignKey(x => x.DocumentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
