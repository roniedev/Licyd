#pragma warning disable IDE0290

using BaseLib;
using Domain;
using Services;

namespace WebAPI.Controllers.Documentos
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentoController : CrudStatusController<DocumentoInput, DocumentoOutput, Documento, IDocumentoService, IDocumentoRepository>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="responseObject"></param>
        /// <param name="service"></param>
        public DocumentoController(AppSettings appSettings, 
            ResponseObject responseObject, 
            IDocumentoService service) : base(appSettings, responseObject, service)
        {
        }
    }
}
