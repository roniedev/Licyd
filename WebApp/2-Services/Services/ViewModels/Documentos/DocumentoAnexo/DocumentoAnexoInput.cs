using Core;

namespace Services;

public class DocumentoAnexoInput : BaseInput
{
    public long DocumentoId { get; set; }

    public byte[] Arquivo { get; set; }

    public string MimeType { get; set; }
}
