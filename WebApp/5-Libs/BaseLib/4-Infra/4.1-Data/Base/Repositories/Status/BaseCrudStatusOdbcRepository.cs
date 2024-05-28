//using Dapper;
//using System.Data.Odbc;

//namespace BaseLib;

//public class BaseCrudStatusOdbcRepository<T>(AppSettings appSettings, ResponseObject responseObject, 
//    SqlHelper sqlHelper, IAuthUser usuarioAutenticado) : BaseCrudOdbcRepository<T>(appSettings, responseObject, sqlHelper, usuarioAutenticado), 
//    IBaseCrudStatusOdbcRepository<T> where T : class, IBaseStatusEntity
//{
//    public virtual void Activate(long id)
//    {
//        (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Active, true);
//        using var conn = new OdbcConnection(_appSettings.Connection.DB2);
//        conn.Execute(sql, param);
//    }

//    public void Activate(long id, OdbcConnection conn, OdbcTransaction transaction)
//    {
//        (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Active, true);
//        conn.Execute(sql, param, transaction);
//    }

//    public virtual void Inactivate(long id, string reasonInactivation)
//    {
//        (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Inactive, true, reasonInactivation);
//        using var conn = new OdbcConnection(_appSettings.Connection.DB2);
//        conn.Execute(sql, param);
//    }

//    public void Inactivate(long id, string reasonInactivation, OdbcConnection conn, OdbcTransaction transaction)
//    {
//        (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Inactive, true, reasonInactivation);
//        conn.Execute(sql, param, transaction);
//    }
//}
