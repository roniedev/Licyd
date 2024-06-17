using Core;
using Microsoft.AspNetCore.Http;

namespace Services;

public class DocumentoInput : BaseInput
{
    public string Nome { get; set; }

    public string Observacao { get; set; }

    public List<IFormFile> Anexos { get; set; } = [];
}
