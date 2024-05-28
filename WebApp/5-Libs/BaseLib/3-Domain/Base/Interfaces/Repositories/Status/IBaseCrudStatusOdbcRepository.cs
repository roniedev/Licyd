//using System.Data.Odbc;

//namespace BaseLib;

//public interface IBaseCrudStatusOdbcRepository<T> : IBaseCrudStatusRepository<T> where T : class, IBaseStatusEntity
//{
//    /// <summary>
//    /// Ativa um registro via transação
//    /// </summary>
//    /// <param name="id"></param>
//    void Activate(long id, OdbcConnection conn, OdbcTransaction transaction);

//    /// <summary>
//    /// Inativa um registro com o motivo via transação
//    /// </summary>
//    /// <param name="id"></param>
//    /// <param name="reasonInactivation"></param>
//    void Inactivate(long id, string reasonInactivation, OdbcConnection conn, OdbcTransaction transaction);
//}
