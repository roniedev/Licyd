using Core;

namespace Domain;

public class Documento : BaseStatusEntity
{
    public string Nome { get; set; }

    public string Observacao { get; set; }

    public List<DocumentoAnexo> Anexos { get; set; }
}