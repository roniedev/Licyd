using Dapper;
using System.Data.Odbc;

namespace BaseLib
{
    public class CrudOdbcRepository<T>(AppSettings appSettings, ResponseObject responseObject,
        SqlHelper sqlHelper, IAuthUser usuarioAutenticado) : BaseRepository(appSettings, responseObject),
        ICrudOdbcRepository<T> where T : IBaseAuditEntity
    {
        protected readonly SqlHelper _sqlHelper = sqlHelper;
        protected readonly IAuthUser _usuarioAutenticado = usuarioAutenticado;
        protected readonly string _tableName = SqlHelper.GetTableName<T>();

        public virtual void Activate(long id)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Active, true);
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Execute(sql, param);
        }

        public void Activate(long id, OdbcConnection conn, OdbcTransaction transaction)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Active, true);
            conn.Execute(sql, param, transaction);
        }

        public virtual void Inactivate(long id, string reasonInactivation)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Inactive, true, reasonInactivation);
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Execute(sql, param);
        }

        public void Inactivate(long id, string reasonInactivation, OdbcConnection conn, OdbcTransaction transaction)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateStatusCommand<T>(id, StatusEnum.Inactive, true, reasonInactivation);
            conn.Execute(sql, param, transaction);
        }

        public TResult BeginTransaction<TResult>(Func<OdbcConnection, OdbcTransaction, TResult> func)
        {
            var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Open();

            var transaction = conn.BeginTransaction();

            try
            {
                TResult result = func(conn, transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void BeginTransaction(Action<OdbcConnection, OdbcTransaction> action)
        {
            var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Open();

            var transaction = conn.BeginTransaction();

            try
            {
                action(conn, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task<TResult> BeginTransactionAsync<TResult>(Func<OdbcConnection, OdbcTransaction, Task<TResult>> func)
        {
            var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Open();

            var transaction = conn.BeginTransaction();

            try
            {
                TResult result = await func(conn, transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task BeginTransactionAsync(Action<OdbcConnection, OdbcTransaction> action)
        {
            var conn = new OdbcConnection(_appSettings.Connection.DB2);
            await conn.OpenAsync();

            var transaction = (OdbcTransaction)await conn.BeginTransactionAsync();

            try
            {
                action(conn, transaction);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public virtual T Insert(T entity)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetInsertCommand(entity, true);

            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Execute(sql, param);

            var command = "VALUES IDENTITY_VAL_LOCAL()";
            var id = conn.Query<long>(command).FirstOrDefault();

            return GetById(id);
        }

        public T Insert(T entity, OdbcConnection conn, OdbcTransaction transaction, bool getEntity = false)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetInsertCommand(entity, true);

            conn.Execute(sql, param, transaction);

            if (getEntity)
            {
                var command = "VALUES IDENTITY_VAL_LOCAL()";
                var id = conn.Query<long>(command).FirstOrDefault();
                return GetById(id);
            }

            return default;
        }

        public virtual void Delete(long id)
        {
            (string sql, DynamicParameters param) = SqlHelper.GetDeleteCommand<T>(id, true);

            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.ExecuteAsync(sql, param);
        }

        public virtual T Update(T entity)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateCommand(entity, true);

            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            conn.Execute(sql, param);

            return GetById(entity.Id);
        }

        public T Update(T entity, OdbcConnection conn, OdbcTransaction transaction)
        {
            (string sql, DynamicParameters param) = _sqlHelper.GetUpdateCommand(entity, true);
            conn.Execute(sql, param, transaction);
            return GetById(entity.Id);
        }

        public virtual T GetById(long id)
        {
            var property = typeof(T).GetProperty(nameof(IBaseEntity.Id));
            var columnName = SqlHelper.GetColumnName<T>(property);

            var sql = GetBaseSql() + $" WHERE {columnName} = ?";
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            return conn.Query<T>(sql, new { id }).FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll(List<Filter> filters)
        {
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);

            var sql = GetBaseSql();
            (string whereSql, DynamicParameters param) = SqlHelper.GetFilters<T>(filters, true);

            sql += $" {whereSql}";

            var sqlPaginated = $@"SELECT * FROM ({sql}) Tabela OFFSET ? ROWS FETCH FIRST ? ROWS ONLY";

            var query = conn.Query<T>(sqlPaginated, param);

            return query.AsQueryable();
        }

        public virtual (IQueryable<T> rows, long totalRows) GetAllPaginated(GetAllInput viewModel)
        {
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);

            var sql = GetBaseSql();
            (string whereSql, DynamicParameters param) = SqlHelper.GetFilters<T>(
                viewModel.Filters,
                true,
                viewModel.OrderBy,
                viewModel.IsAscending);

            sql += $" {whereSql}";

            var sqlTotalRows = $"SELECT COUNT(*) FROM ({sql}) TotalRegistros";
            var totalRows = conn.Query<int>(sqlTotalRows, param);

            param.Add("Skip", viewModel.Skip);
            param.Add("Take", viewModel.Take);

            var sqlPaginated = $@"SELECT * FROM ({sql} OFFSET ? ROWS FETCH FIRST ? ROWS ONLY) Tabela";

            var query = conn.Query<T>(sqlPaginated, param);

            return (query.AsQueryable(), totalRows.FirstOrDefault());
        }

        public virtual IQueryable<T> GetAll(string filter)
        {
            var sql = GetBaseSql();
            using var conn = new OdbcConnection(_appSettings.Connection.DB2);
            return conn.Query<T>(sql).AsQueryable();
        }

        protected virtual string GetBaseSql()
        {
            var sql = @$"SELECT * FROM (SELECT A.*,
            ROW_NUMBER() OVER(ORDER BY 1 DESC) AS ROWID 
            FROM {_tableName} A) Tabela";

            return sql;
        }
    }
}
