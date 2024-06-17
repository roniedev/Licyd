using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Core;

public class ReportRepository(AppSettings appSettings, IAuthUser authUser) : IReportRepository
{
    private readonly string _usuarioRede = authUser.UserName;
    private readonly AppSettings _appSettings = appSettings;

    public async Task<long> Insert(BaseReport relatorio)
    {
        if (string.IsNullOrWhiteSpace(_appSettings.Connection.InfoteutoWeb))
            throw new InvalidOperationException("Relatorio não sera gerado, necessario informar a conexão com o InfoTeuto.");

        using var conn = new SqlConnection(_appSettings.Connection.InfoteutoWeb);

        var sql = $@"INSERT INTO Infoteuto_Relatorio 
            (JsonParametros, TipoRelatorio, CreateBy, CreateDate, UpdateBy, UpdateDate)
            OUTPUT INSERTED.Id VALUES (@JsonParametros, @TipoRelatorio, @CreateBy, 
            @CreateDate, @UpdateBy, @UpdateDate)";

        var param = new DynamicParameters();

        var dataAtual = DateTime.Now;
        var parametros = JsonConvert.SerializeObject(relatorio);

        param.Add("@JsonParametros", parametros);
        param.Add("@TipoRelatorio", 0);
        param.Add("@CreateBy", _usuarioRede);
        param.Add("@CreateDate", dataAtual);
        param.Add("@UpdateBy", _usuarioRede);
        param.Add("@UpdateDate", dataAtual);

        return await conn.QueryFirstOrDefaultAsync<long>(sql, param);
    }
}
