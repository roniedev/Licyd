#pragma warning disable IDE0290

using Core;
using Domain;
using Services;

namespace WebApi;

/// <summary>
/// Cadastro de anexos dos documentos
/// </summary>
public class DocumentoAnexoController : CrudController<DocumentoAnexoInput, 
    DocumentoAnexoOutput, DocumentoAnexo, IDocumentoAnexoService, IDocumentoAnexoRepository>
{
    /// <summary>
    /// Método construtor
    /// </summary>
    /// <param name="appSettings"></param>
    /// <param name="responseObject"></param>
    /// <param name="service"></param>
    public DocumentoAnexoController(AppSettings appSettings, 
        ResponseObject responseObject, 
        IDocumentoAnexoService service) : base(appSettings, responseObject, service)
    {
    }
}
