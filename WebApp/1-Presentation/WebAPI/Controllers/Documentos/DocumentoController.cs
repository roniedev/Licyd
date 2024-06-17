#pragma warning disable IDE0290

using Core;
using Domain;
using Services;

namespace WebApi.Controllers.Documentos
{
    /// <summary>
    /// Cadastro de documentos
    /// </summary>
    public class DocumentoController : CrudStatusController<DocumentoInput, DocumentoOutput, Documento, IDocumentoService, IDocumentoRepository>
    {
        /// <summary>
        /// Método construtor
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
