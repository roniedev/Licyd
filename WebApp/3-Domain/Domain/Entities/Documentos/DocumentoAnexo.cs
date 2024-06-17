using Core;
using System.Text.Json.Serialization;

namespace Domain;

public class DocumentoAnexo : BaseAuditEntity
{
    public long DocumentoId { get; set; }

    public byte[] Arquivo { get; set; }

    public string MimeType { get; set; }

    [JsonIgnore]
    public Documento Documento { get; set; }
}
