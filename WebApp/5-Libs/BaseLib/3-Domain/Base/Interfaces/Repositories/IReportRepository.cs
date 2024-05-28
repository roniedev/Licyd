namespace BaseLib;

public interface IReportRepository
{
    Task<long> Insert(BaseReport relatorio);
}
