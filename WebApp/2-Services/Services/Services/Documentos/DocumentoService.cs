#pragma warning disable IDE0290

using Core;
using Domain;
using FluentValidation;

namespace Services;

public class DocumentoService : CrudService<DocumentoInput, DocumentoOutput, Documento, IDocumentoRepository>
{
    public DocumentoService(AppSettings appSettings, 
        ResponseObject responseObject, 
        IValidator<DocumentoInput> validator, 
        IDocumentoRepository repository) : base(appSettings, responseObject, validator, repository)
    {
    }
}
