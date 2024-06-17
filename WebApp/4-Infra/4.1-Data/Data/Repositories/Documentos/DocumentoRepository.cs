#pragma warning disable IDE0290

using Core;
using Domain;

namespace Data;

public class DocumentoRepository : CrudEfRepository<Documento>
{
    public DocumentoRepository(BaseDbContext context, AppSettings appSettings) : base(context, appSettings)
    {
    }
}