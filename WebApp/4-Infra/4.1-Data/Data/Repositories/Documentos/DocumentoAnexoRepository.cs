#pragma warning disable IDE0290

using Core;
using Domain;

namespace Data;

public class DocumentoAnexoRepository : CrudEfRepository<DocumentoAnexo>
{
    public DocumentoAnexoRepository(BaseDbContext context, AppSettings appSettings) : base(context, appSettings)
    {
    }
}