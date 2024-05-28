#pragma warning disable IDE0290

using BaseLib;
using Domain;
using Services;

namespace WebAPI;

/// <summary>
/// 
/// </summary>
public class DocumentoAnexoController : CrudController<DocumentoAnexoInput, DocumentoAnexoOutput, 
    DocumentoAnexo, IDocumentoAnexoService, IDocumentoAnexoRepository>
{
    /// <summary>
    /// 
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
