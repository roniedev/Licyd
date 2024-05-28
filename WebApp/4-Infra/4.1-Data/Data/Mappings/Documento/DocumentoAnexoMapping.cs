using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class DocumentoAnexoMapping : IEntityTypeConfiguration<DocumentoAnexo>
{
    public void Configure(EntityTypeBuilder<DocumentoAnexo> builder)
    {
        builder.Property(x => x.MimeType).IsRequired().HasMaxLength(128);
    }
}
