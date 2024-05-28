#pragma warning disable IDE0290

using BaseLib;
using Domain;

namespace Data
{
    public class DocumentoAnexoRepository : CrudEfRepository<DocumentoAnexo>, IDocumentoAnexoRepository
    {
        public DocumentoAnexoRepository(BaseDbContext context, AppSettings appSettings) : base(context, appSettings)
        {
        }
    }
}
