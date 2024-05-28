#pragma warning disable IDE0290

using BaseLib;
using Domain;

namespace Data
{
    public class DocumentoRepository : CrudEfRepository<Documento>, IDocumentoRepository
    {
        public DocumentoRepository(BaseDbContext context, AppSettings appSettings) : base(context, appSettings)
        {
        }
    }
}
