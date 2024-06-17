using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class DocumentoMapping : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Observacao)
            .HasMaxLength(256);
    }
}
