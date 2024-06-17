using Core;
using Domain;

namespace Services;

public interface IDocumentoService : ICrudService<DocumentoInput, DocumentoOutput, Documento, IDocumentoRepository>
{
}
