namespace Core;

public interface IReportRepository
{
    Task<long> Insert(BaseReport relatorio);
}
