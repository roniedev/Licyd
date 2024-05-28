using BaseLib;
using Domain;

namespace Services;

public interface IDocumentoAnexoService : ICrudService<DocumentoAnexoInput, DocumentoAnexoOutput, DocumentoAnexo, IDocumentoAnexoRepository>
{
}
