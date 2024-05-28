using BaseLib;

namespace Services;

public class DocumentoAnexoOutput : BaseOutput
{
    public long DocumentoId { get; set; }

    public string DocumentoNome { get; set; }

    public byte[] Arquivo { get; set; }

    public string MimeType { get; set; }
}
