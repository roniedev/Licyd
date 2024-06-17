using Core;

namespace Services;

public class DocumentoOutput : BaseStatusOutput
{
    public string Nome { get; set; }

    public string Observacao { get; set; }

    public List<DocumentoAnexoOutput> Anexos { get; set; }
}
