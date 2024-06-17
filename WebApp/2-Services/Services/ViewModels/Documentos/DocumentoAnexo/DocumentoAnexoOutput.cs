using Core;

namespace Services;

public class DocumentoAnexoOutput : BaseOutput
{
    public long DocumentoId { get; set; }

    public byte[] Arquivo { get; set; }

    public string MimeType { get; set; }
}
