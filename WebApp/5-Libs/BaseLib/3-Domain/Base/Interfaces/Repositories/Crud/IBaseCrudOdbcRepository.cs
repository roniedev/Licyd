//using System.Data.Odbc;

//namespace BaseLib;

//public interface IBaseCrudOdbcRepository<T> : ICrudRepository<T> where T : IBaseAuditEntity
//{
//    /// <summary>
//    /// Inicia uma transação
//    /// </summary>
//    /// <typeparam name="TResult">Tipo do resultado do método</typeparam>
//    /// <param name="func">Função que será executada</param>
//    /// <returns></returns>
//    TResult BeginTransaction<TResult>(Func<OdbcConnection, OdbcTransaction, TResult> func);

//    /// <summary>
//    /// Inicia uma transação
//    /// </summary>
//    /// <param name="action"></param>
//    void BeginTransaction(Action<OdbcConnection, OdbcTransaction> action);

//    /// <summary>
//    /// Inicia uma transação assíncrona
//    /// </summary>
//    /// <typeparam name="TResult"></typeparam>
//    /// <param name="func"></param>
//    /// <returns></returns>
//    Task<TResult> BeginTransactionAsync<TResult>(Func<OdbcConnection, OdbcTransaction, Task<TResult>> func);

//    /// <summary>
//    /// Inicia uma transação assíncrona
//    /// </summary>
//    /// <param name="action"></param>
//    /// <returns></returns>
//    Task BeginTransactionAsync(Action<OdbcConnection, OdbcTransaction> action);

//    /// <summary>
//    /// Lista um registro por Id
//    /// </summary>
//    /// <param name="id"></param>
//    /// <param name="asNoTracking"></param>
//    /// <returns></returns>
//    T GetById(long id);

//    /// <summary>
//    /// Insere um registro via transação
//    /// </summary>
//    /// <param name="entity">Entidade do tipo IBaseEntity</param>
//    /// <param name="conn">Conexão aberta para realizar as operações</param>
//    /// <param name="transaction">Transação aberta para realizar as operações</param>
//    /// <param name="getEntity">Retornar o registro inserido</param>
//    /// <returns></returns>
//    T Insert(T entity, OdbcConnection conn, OdbcTransaction transaction, bool getEntity = false);

//    /// <summary>
//    /// Atualiza um registro via transação
//    /// </summary>
//    /// <param name="entity">Entidade do tipo IBaseEntity</param>
//    /// <param name="conn">Conexão aberta para realizar as operações</param>
//    /// <param name="transaction">Transação aberta para realizar as operações</param>
//    /// <returns></returns>
//    T Update(T entity, OdbcConnection conn, OdbcTransaction transaction);
//}
