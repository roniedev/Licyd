#pragma warning disable IDE0290

using BaseLib;
using Domain;
using FluentValidation;

namespace Services;

public class DocumentoAnexoService : CrudService<DocumentoAnexoInput, DocumentoAnexoOutput, DocumentoAnexo, IDocumentoAnexoRepository>, IDocumentoAnexoService
{
    public DocumentoAnexoService(AppSettings appSettings,
        ResponseObject responseObject,
        IValidator<DocumentoAnexoInput> validator,
        IDocumentoAnexoRepository repository) :
        base(appSettings, responseObject, validator, repository)
    {
    }
}
